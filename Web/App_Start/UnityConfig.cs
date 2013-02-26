using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC_Presentation.Models;
using System.Web.Http;

namespace Web {
	public static class UnityConfig {

		public static IUnityContainer Initialise() {
			var container = BuildUnityContainer();

			DependencyResolver.SetResolver(new Unity.Mvc4.UnityDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

			return container;
		}

		private static IUnityContainer BuildUnityContainer() {
			var container = new UnityContainer();

			container.RegisterType<ITaskRepository, MemoryTaskRepository>(new ContainerControlledLifetimeManager());

			return container;
		}
	}
}