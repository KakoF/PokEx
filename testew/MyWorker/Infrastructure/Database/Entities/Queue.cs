using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Entities
{
	[ExcludeFromCodeCoverage]
	public class Queue
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DestinationName { get; set; }
		public int BatchSize { get; set; }
		public QueueType Type { get; set; }
		public QueueContext Context { get; set; }
		public QueueStatus Status { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }

    }
}
