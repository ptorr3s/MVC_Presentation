using MVC_Presentation.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers {
	public class HomeController : Controller {

		private ITaskRepository repository;

		public HomeController(ITaskRepository repository) {
			this.repository = repository;
		}

		//
		// GET: /Home/
		public ActionResult Index() {
			return View(repository.GetAll());
		}
	}
}
