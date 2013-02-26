using Microsoft.AspNet.SignalR;
using MVC_Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Presentation.Hubs {

	public class HubTaskUpdateConsumer : ITaskRepositoryUpdateConsumer {

		void ITaskRepositoryUpdateConsumer.TasksUpdated() {
			Tasks.TasksUpdated();
		}

	}

	public class Tasks : Hub {

		ITaskRepository repository;

		public Tasks(ITaskRepository repository) {
			this.repository = repository;
		}

		public static void TasksUpdated() {
			GlobalHost.ConnectionManager.GetHubContext<Tasks>()
				.Clients.All.updated();
		}
	}
}