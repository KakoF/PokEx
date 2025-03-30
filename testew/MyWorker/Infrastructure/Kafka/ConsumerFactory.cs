using Confluent.Kafka;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Kafka
{
	[ExcludeFromCodeCoverage]
	public class ConsumerFactory : IConsumerFactory
	{
		private readonly ConsumerConfig config;

		public ConsumerFactory(ConsumerConfig config)
		{
			this.config = config;
		}

		public IConsumer<string, string> CreateConsumer()
		{
			return new ConsumerBuilder<string, string>(config).Build();
		}
	}
}
