using Application.Interfaces.Repositories;
using AutoMapper;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
	public class QueueRepositoryReadOnly : IQueueRepositoryReadOnly
	{
		private readonly Context context;
		private readonly IMapper mapper;

		public QueueRepositoryReadOnly(Context context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<List<Queue>> GetActive()
		{
			var queues = await context.Queues.Where(w => w.Status.Equals(QueueStatus.Active)).ToListAsync();

			return queues.Select(x => new Queue(x.Id, x.Name, x.DestinationName, x.BatchSize, x.Type, x.Context, x.Status, x.CreateAt, x.UpdateAt)).ToList();
		}

		
	}
}
