using Infrastructure.Database.Entities;
using Infrastructure.Database.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
	[ExcludeFromCodeCoverage]
	public class Context : DbContext
	{
		public DbSet<Queue> Queues { get; set; }

		public Context(DbContextOptions<Context> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new QueueMap());
		}
	}
}
