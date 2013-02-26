using MVC_Presentation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace MVC_Presentation.Models {
	public class DapperRepository : ITaskRepository {

		ITaskRepositoryUpdateConsumer updateconsumer;

		public DapperRepository(ITaskRepositoryUpdateConsumer updateconsumer) {
			this.updateconsumer = updateconsumer;
		}

		protected void onTasksUpdated() {
			System.Threading.Tasks.Task.Factory.StartNew(() => updateconsumer.TasksUpdated());
		}

		protected IDbConnection connection() {
			SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlExpress"].ConnectionString);
			c.Open();
			return c;
		}

		public IEnumerable<Task> GetAll() {
			using (var c = connection()) {
				return c.Query<Task>("SELECT * FROM Tasks");
			}
		}

		public Task Get(int id) {
			using (var c = connection()) {
				return c.Query<Task>("SELECT * FROM Tasks WHERE id = @id",
					new { id = id }).FirstOrDefault();
			}
		}

		public Task Add(Task t) {
			using (var c = connection()) {
				c.Execute("INSERT INTO Tasks (isComplete, title) VALUES (@isComplete, @title)", t);
				onTasksUpdated();
				return t;
			}
		}

		public void Delete(int id) {
			using (var c = connection()) {
				c.Execute("DELETE FROM Tasks WHERE id = @id", new { id = id });
				onTasksUpdated();
			}
		}

		public bool Update(Task t) {
			using (var c = connection()) {
				c.Execute("UPDATE Tasks SET isComplete = @isComplete, title = @title WHERE id = @id", t);
				onTasksUpdated();
				return true;
			}
		}
	}
}