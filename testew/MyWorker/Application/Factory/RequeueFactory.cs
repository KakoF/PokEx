using Application.Interfaces.Brokers;
using Application.Interfaces.Factory;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factory
{
	public class RequeueFactory : IRequeueFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public RequeueFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IRequeueBase Create(QueueType queueType)
		{
			switch (queueType)
			{
				case QueueType.RabbitMQ:
					return _serviceProvider.GetRequiredService<IRabbitRequeue>();
				case QueueType.Kafka:
					return _serviceProvider.GetRequiredService<IKafkaRequeue>();
				default:
					throw new ArgumentException("Tipo de fila não suportado", nameof(queueType));
			}

		}
	}
}