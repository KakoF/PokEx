using Application.Interfaces.Brokers;
using Application.Interfaces.Factory;
using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;
using Domain;
using Domain.Enums;

namespace Application.UseCases.Reprocess
{
	public class ReprocessUseCase : IReprocessUseCase
	{
		private readonly IQueueRepositoryReadOnly queueRepositoryReadOnly;
		private readonly IRequeueFactory _factory;

		public ReprocessUseCase(IQueueRepositoryReadOnly queueRepositoryReadOnly, IRequeueFactory factory)
		{
			this.queueRepositoryReadOnly = queueRepositoryReadOnly;
			_factory = factory;
		}

		public async Task Execute()
		{
			var queues = await queueRepositoryReadOnly.GetActive();
			if (!queues.Any())
				return;

			var tasks = new List<Task>();

			foreach (var queue in queues)
				tasks.Add(Execute(queue, _factory.Create(queue.Type), new CancellationTokenSource().Token));

			await Task.WhenAll(tasks);
		}
		private async Task Execute(Queue queue, IRequeueBase requeuer, CancellationToken cancellationToken)
		{
			await requeuer.RequeueMessages(queue, cancellationToken);
		}
	}
}
