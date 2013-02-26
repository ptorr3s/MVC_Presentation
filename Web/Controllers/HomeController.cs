using MVC_Presentation.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers {
	public class HomeController : Controller {

		//
		// GET: /Home/
		public ActionResult Index() {
			return View(
				new List<Task>() {
                new Task() { id = 1, isComplete = true, title = "Do Groceries" },
                new Task() { id = 2, isComplete = true, title = "Pick up dry cleaning" },
                new Task() { id = 3, isComplete = false, title = "Fix Jeff Z's code" }
            });
		}
	}
}
