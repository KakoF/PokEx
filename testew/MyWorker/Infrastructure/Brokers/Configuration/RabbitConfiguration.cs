using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Brokers.Configuration
{
	public class RabbitConfiguration
	{
		public string Cluster { get; set; } = null!;
		public string[] Hosts { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}
