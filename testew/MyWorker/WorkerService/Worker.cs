using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;


		public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var queueRepository = scope.ServiceProvider.GetRequiredService<IReprocessUseCase>();
				await queueRepository.Execute();
			}

			while (!stoppingToken.IsCancellationRequested)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				}
				await Task.Delay(1000, stoppingToken);
			}
		}
	}
}
