﻿
using Domain;

namespace Application.Interfaces.Repositories
{
	public interface IQueueRepositoryReadOnly
	{
		Task<List<Queue>> GetActive();
	}
}
