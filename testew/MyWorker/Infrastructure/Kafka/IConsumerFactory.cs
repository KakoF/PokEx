using Confluent.Kafka;

namespace Infrastructure.Kafka
{
	public interface IConsumerFactory
	{
		IConsumer<string, string> CreateConsumer();
	}
}