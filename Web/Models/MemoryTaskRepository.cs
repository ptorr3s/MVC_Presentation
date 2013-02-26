using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Presentation.Models {
	public class MemoryTaskRepository : ITaskRepository {

		private IList<Task> tasks { get; set; }
		private ITaskRepositoryUpdateConsumer updateconsumer;

		public MemoryTaskRepository(ITaskRepositoryUpdateConsumer updateconsumer) {
			this.updateconsumer = updateconsumer;

			tasks = new List<Task>() {
                new Task() { id = 1, isComplete = true, title = "Do Groceries" },
                new Task() { id = 2, isComplete = true, title = "Pick up dry cleaning" },
                new Task() { id = 3, isComplete = false, title = "Fix Jeff Z's code" }
            };
		}

		protected void onTasksUpdated() {
			System.Threading.Tasks.Task.Factory.StartNew(() => updateconsumer.TasksUpdated());
		}

		public IEnumerable<Task> GetAll() {
			return tasks;
		}

		public Task Get(int id) {
			return tasks.Where(t => t.id == id).FirstOrDefault();
		}

		public Task Add(Task t) {
			tasks.Add(t);
			onTasksUpdated();
			return t;
		}

		public void Delete(int id) {
			tasks.Where(t => t.id == id).ToList().ForEach(t => tasks.Remove(t));
			onTasksUpdated();
		}

		public bool Update(Task t) {
			Task update = tasks.Where(t2 => t2.id == t.id).FirstOrDefault();

			if (update == null)
				return false;

			tasks.Insert(tasks.IndexOf(update) + 1, t);
			tasks.Remove(update);
			onTasksUpdated();

			return true;
		}

	}
}