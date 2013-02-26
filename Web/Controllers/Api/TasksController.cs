using MVC_Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC_Presentation.Controllers {
	public class TasksController : ApiController {

		private ITaskRepository repository;

		public TasksController(ITaskRepository repository) {
			this.repository = repository;
		}

		public IEnumerable<Task> GetAllTasks() {
			return repository.GetAll();
		}

		public Task GetTask(int id) {
			return repository.Get(id);
		}

		public Task PostTask(Task t) {
			return repository.Add(t);
		}

		public bool PutTask(Task t) {
			return repository.Update(t);
		}

		public void DeleteTask(int id) {
			repository.Delete(id);
		}
	}
}
