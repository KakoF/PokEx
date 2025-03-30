using Infrastructure.Database;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using WorkerService;
using WorkerService.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.AddKafkaConsumer();
builder.Services.AddServices();
builder.Services.AddRabbit(builder.Configuration);

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReprocessingQueues")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
