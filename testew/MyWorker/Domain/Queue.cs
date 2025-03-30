using Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Domain
{
	[ExcludeFromCodeCoverage]
	public class Queue
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string DestinationName { get; private set; }
		public int BatchSize { get; private set; }
		public QueueType Type { get; private set; }
		public QueueContext Context { get; private set; }
		public QueueStatus Status { get; private set; }
		public DateTime CreateAt { get; private set; }
		public DateTime? UpdateAt { get; private set; }

        public Queue()
        {
            
        }
        public Queue(int id, string name, string destinationName, int batchSize, QueueType type, QueueContext context, QueueStatus status, DateTime createAt, DateTime? updateAt)
		{
			Id = id;
			Name = name;
			DestinationName = destinationName;
			BatchSize = batchSize;
			Type = type;
			Context = context;
			Status = status;
			CreateAt = createAt;
			UpdateAt = updateAt;
		}

		public void BatchSizeDecrement()
		{
			BatchSize--;
		}
	}
}
