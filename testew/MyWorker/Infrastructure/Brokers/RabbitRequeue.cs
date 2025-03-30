using Application.Interfaces.Brokers;
using Domain;
using Infrastructure.Brokers.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Brokers
{
	public class RabbitRequeue : IRabbitRequeue
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly ILogger<RabbitRequeue> _logger;


		public RabbitRequeue(IServiceScopeFactory serviceScopeFactory, ILogger<RabbitRequeue> logger)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_logger = logger;

		}

		public async Task RequeueMessages(Queue queue, CancellationToken cancellationToken)
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var service = scope.ServiceProvider.GetRequiredService<RabbitConnector>();
				var channel = service.OpenChannelBasic(queue.Name);
				if(channel == null)
					return;

				while (queue.BatchSize > 0)
				{
					channel!.QueueDeclarePassive(queue.Name);


					var result = channel.BasicGet(queue.Name, false);
					try
					{
						if (result is null)
							return;

						queue.BatchSizeDecrement();
						var message = PrepareMessage(result.Body.ToArray(), result.BasicProperties.Headers);
						await PublishMessageAsync(queue.DestinationName, message.Item1, message.Item2);
						channel.BasicAck(deliveryTag: result.DeliveryTag, multiple: false);

					}
					catch (Exception ex)
					{
						_logger.LogError(ex, ex.Message);
						channel.BasicNack(deliveryTag: result.DeliveryTag, multiple: false, requeue: true);
					}
				}
			}

		}


		private (object?, IDictionary<string, object>?) PrepareMessage(byte[] bytes, IDictionary<string, object>? headers = null)
		{
			var body = bytes.ToArray();
			var message = JsonSerializer.Deserialize<object>(Encoding.UTF8.GetString(body));
			return (message, headers);
		}


		private async Task PublishMessageAsync<T>(string queue, T message, IDictionary<string, object>? headers = null)
		{

			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var service = scope.ServiceProvider.GetRequiredService<RabbitConnector>();
				var channel = service.OpenChannel();
				var properties = channel.CreateBasicProperties();
				properties.Headers = headers;

				var messageJson = JsonSerializer.Serialize(message);
				var body = Encoding.UTF8.GetBytes(messageJson);

				channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

				await Task.Run(() =>
				{
					channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
				});
			}
		}

	}
}