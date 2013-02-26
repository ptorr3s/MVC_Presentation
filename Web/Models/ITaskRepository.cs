using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC_Presentation.Models {

	public interface ITaskRepositoryUpdateConsumer {
		void TasksUpdated();
	}

	public interface ITaskRepository {

		IEnumerable<Task> GetAll();

		Task Get(int id);
		Task Add(Task t);
		void Delete(int id);
		bool Update(Task t);
	}
}
