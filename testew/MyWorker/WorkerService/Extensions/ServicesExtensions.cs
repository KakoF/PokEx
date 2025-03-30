using Application.Factory;
using Application.Interfaces.Factory;
using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;
using Application.UseCases.Reprocess;
using Confluent.Kafka;
using Infrastructure.Database.Repositories;
using Infrastructure.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{
	public static class ServicesExtension
	{
		public static void AddServices(this IServiceCollection services)
		{
			/*var consumerConfig = new ConsumerConfig
			{
				BootstrapServers = "localhost:9092",
				GroupId = "dql-workflow-reprocess",
				AutoOffsetReset = AutoOffsetReset.Earliest,
				EnableAutoCommit = false
			};

			services.AddSingleton(consumerConfig);*/
			services.AddSingleton<IConsumerFactory, ConsumerFactory>();
			services.AddScoped<IQueueRepositoryReadOnly, QueueRepositoryReadOnly>();
			services.AddScoped<IReprocessUseCase, ReprocessUseCase>();
			services.AddScoped<IRequeueFactory, RequeueFactory>();
		}
	}
}
