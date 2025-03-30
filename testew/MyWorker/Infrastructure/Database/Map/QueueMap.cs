using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Map
{
	[ExcludeFromCodeCoverage]
	public class QueueMap : IEntityTypeConfiguration<Queue>
	{
		public void Configure(EntityTypeBuilder<Queue> builder)
		{
			builder.ToTable("Queues", "dbo");

			builder.HasKey(q => q.Id);

			builder.Property(q => q.Id)
				.ValueGeneratedOnAdd();

			builder.Property(q => q.Name)
				.IsRequired()
				.HasMaxLength(100)
				.HasColumnType("VARCHAR(100)");

			builder.Property(q => q.DestinationName)
				.IsRequired()
				.HasMaxLength(100)
				.HasColumnType("VARCHAR(100)");

			builder.Property(q => q.BatchSize)
				.IsRequired()
				.HasColumnType("INT");

			builder.Property(q => q.Type)
				.IsRequired()
				.HasConversion<string>()
				.HasColumnType("VARCHAR(50)");

			builder.Property(q => q.Context)
				.IsRequired()
				.HasConversion<string>()
				.HasColumnType("VARCHAR(50)");

			builder.Property(q => q.Status)
				.IsRequired()
				.HasConversion<string>()
				.HasColumnType("VARCHAR(50)");

			builder.Property(q => q.CreateAt)
				.IsRequired()
				.HasColumnType("DATETIME");

			builder.Property(q => q.UpdateAt)
				.HasColumnType("DATETIME")
				.IsRequired(false);
		}
	}
}