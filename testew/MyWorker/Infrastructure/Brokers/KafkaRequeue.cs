using Application.Interfaces.Brokers;
using Confluent.Kafka;
using Domain;
using Infrastructure.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Brokers
{
	public class KafkaRequeue : IKafkaRequeue
	{
		private readonly IProducer<string, string> producer;
		private readonly IConsumer<string, string> consumer;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly ILogger<RabbitRequeue> _logger;


		public KafkaRequeue(IProducer<string, string> producer, IConsumerFactory consumerFactory)
		{
			this.producer = producer;
			this.consumer = consumerFactory.CreateConsumer();
		}

		public async Task RequeueMessages(Queue queue, CancellationToken cancellationToken)
		{
			try
			{
				consumer.Subscribe(queue.Name);

				var offsets = new List<TopicPartitionOffset>();
				var consumedMessages = 0;

				while (consumedMessages < queue.BatchSize && !cancellationToken.IsCancellationRequested)
				{
					try
					{
						var consumeResult = consumer.Consume(cancellationToken);

						if (consumeResult is null || consumeResult.IsPartitionEOF)
							break;

						await ProduceMessage(queue.DestinationName, consumeResult);

						offsets.Add(consumeResult.TopicPartitionOffset);
					}
					catch (Exception ex)
					{
						//Log here
					}

					consumedMessages++;
				}

				if (offsets.Count > 0)
					consumer.Commit(offsets);
			}
			catch (Exception ex)
			{
				//log here
			}
			finally
			{
				consumer.Close();
			}
		}

		private ConsumeResult<string, string> ConsumeMessage(CancellationToken cancellationToken)
			=> consumer.Consume(cancellationToken);

		private async Task ProduceMessage(string destinationName, ConsumeResult<string, string> consumeResult)
		{
			await producer.ProduceAsync(destinationName, new Message<string, string>
			{
				Key = consumeResult.Key,
				Value = consumeResult.Value
			});
		}
	}
}
