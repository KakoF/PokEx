using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class KafkaExtension
	{
		public static void AddKafkaConsumer(this HostApplicationBuilder builder)
		{
			var kafka = new KafkaConfig();

			builder.Configuration.GetSection("Kafka").Bind(kafka);
			builder.Services.AddOptions<KafkaConfig>().BindConfiguration("Kafka");

			RegisterGenericConsumer(builder, kafka);
			RegisterProducer(builder, kafka);
		}

		
		private static void RegisterGenericConsumer(HostApplicationBuilder builder, KafkaConfig kafka)
		{
			var consumerConfig = new ConsumerConfig
			{
				BootstrapServers = kafka.BootstrapServers,
				GroupId = "dql-workflow-reprocess",
				AutoOffsetReset = AutoOffsetReset.Earliest,
				EnableAutoCommit = false
			};

			builder.Services.AddSingleton(consumerConfig);
		}

		private static void RegisterProducer(HostApplicationBuilder builder, KafkaConfig kafka)
		{
			var producerConfig = new ProducerConfig
			{
				BootstrapServers = kafka.BootstrapServers,
				SecurityProtocol = SecurityProtocol.Plaintext
			};

			builder.Services.AddSingleton(producerConfig);
			builder.Services.AddSingleton<IProducer<string, string>>(sp =>
			{
				var config = sp.GetRequiredService<ProducerConfig>();

				return new ProducerBuilder<string, string>(config).Build();
			});
		}
	}

	public class KafkaConfig
	{
		public string BootstrapServers { get; set; }
		public string SecurityProtocol { get; set; }
		public string Topic { get; set; }
		public string Consumer { get; set; }
	}
}