using MVC_Presentation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Presentation.Models {
	public class CodeFirstEFRepository : ITaskRepository {

		private class TaskContext : DbContext {

			public TaskContext() : base("MVC_Presentation") { }

			protected override void OnModelCreating(DbModelBuilder modelBuilder) {
				base.OnModelCreating(modelBuilder);
			}

			public DbSet<Task> Tasks { get; set; }

		}

		ITaskRepositoryUpdateConsumer updateconsumer;

		public CodeFirstEFRepository(ITaskRepositoryUpdateConsumer updateconsumer) {
			this.updateconsumer = updateconsumer;
		}

		protected void onTasksUpdated() {
			System.Threading.Tasks.Task.Factory.StartNew(() => updateconsumer.TasksUpdated());
		}

		public IEnumerable<Task> GetAll() {
			using (var context = new TaskContext()) {
				return context.Tasks.ToList();
			}
		}

		public Task Get(int id) {
			using (var context = new TaskContext()) {
				var task = from t in context.Tasks
							  where t.id == id
							  select t;

				return task.FirstOrDefault();
			}
		}

		public Task Add(Task t) {
			using (var context = new TaskContext()) {
				context.Tasks.Add(t);
				context.SaveChanges();
				onTasksUpdated();
				return t;
			}
		}

		public void Delete(int id) {
			using (var context = new TaskContext()) {
				Task task = (from t in context.Tasks
								 where t.id == id
								 select t).FirstOrDefault();

				if (task != null)
					context.Tasks.Remove(task);

				context.SaveChanges();
				onTasksUpdated();
			}
		}

		public bool Update(Task t) {
			using (var context = new TaskContext()) {

				context.Tasks.Attach(t);
				context.Entry(t).State = System.Data.EntityState.Modified;
				context.SaveChanges();

				onTasksUpdated();

				return true;
			}
		}
	}
}