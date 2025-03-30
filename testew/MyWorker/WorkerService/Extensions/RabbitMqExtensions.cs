using Application.Interfaces.Brokers;
using Infrastructure.Brokers.Configuration;
using Infrastructure.Brokers.Connectors;
using Infrastructure.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{
	public static class RabbitMqExtensions
	{
		public static void AddRabbit(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<RabbitConfiguration>(configuration.GetSection("ConnectionStrings:RabbitMQCluster"));
			services.AddSingleton<IRabbitRequeue, RabbitRequeue>();
			services.AddSingleton<IKafkaRequeue, KafkaRequeue>();
			services.AddSingleton<RabbitConnector>();
		}
	}
}
