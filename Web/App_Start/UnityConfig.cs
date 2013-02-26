using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC_Presentation.Models;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using MVC_Presentation.Hubs;

namespace Web {
	public static class UnityConfig {

		public static IUnityContainer Initialise() {
			var container = BuildUnityContainer();

			DependencyResolver.SetResolver(new Unity.Mvc4.UnityDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
			GlobalHost.DependencyResolver = new Unity.SignalR.UnityDependencyResolver(container);

			return container;
		}

		private static IUnityContainer BuildUnityContainer() {
			var container = new UnityContainer();

			container.RegisterType<ITaskRepository, CodeFirstEFRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<ITaskRepositoryUpdateConsumer, HubTaskUpdateConsumer>(new ContainerControlledLifetimeManager());

			return container;
		}
	}
}