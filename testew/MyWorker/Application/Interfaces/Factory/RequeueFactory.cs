using Application.Interfaces.Brokers;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Factory
{
	public interface IRequeueFactory
	{
		IRequeueBase Create(QueueType queueType);
	}
}
