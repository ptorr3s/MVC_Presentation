using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Presentation.Models {
	public class Task {
		public int id { get; set; }
		public bool isComplete { get; set; }
		[Required]
		public string title { get; set; }
	}
}