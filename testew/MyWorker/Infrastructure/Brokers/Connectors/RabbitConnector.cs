using Infrastructure.Brokers.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Threading.Channels;
using IModel = RabbitMQ.Client.IModel;

namespace Infrastructure.Brokers.Connectors
{
	public class RabbitConnector
	{
		private readonly IConnectionFactory _factory;
		private readonly RabbitConfiguration _conf;
		private IConnection _connection;
		private IModel _channel;
		public RabbitConnector(IOptions<RabbitConfiguration> options)
		{
			_conf = options.Value;
			_factory = new ConnectionFactory()
			{
				UserName = _conf.UserName,
				Password = _conf.Password,
			};
		}

		private void Connect()
		{
			_connection = _factory.CreateConnection(_conf.Hosts);
		}

		public IModel OpenChannel(int? bathSize = null)
		{
			Connect();
			_channel = _connection.CreateModel();
			if (bathSize.HasValue)
				_channel.BasicQos(0, (ushort)bathSize, false);

			return _channel;
		}

		public IModel? OpenChannelBasic(string queueName)
		{
			Connect();
			_channel = _connection.CreateModel();
			try
			{
				_channel.QueueDeclarePassive(queueName);
			}
			catch (RabbitMQ.Client.Exceptions.OperationInterruptedException ex)
			{
				return null;
			}

			return _channel;
		}
	}
}
