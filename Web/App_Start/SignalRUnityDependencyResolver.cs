using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Unity.SignalR {
	/// <summary>
	/// A SiganlR DependencyResolver that uses Microsoft Unity.
	/// </summary>
	public class UnityDependencyResolver : DefaultDependencyResolver {
		private readonly IUnityContainer container;

		public UnityDependencyResolver(IUnityContainer container) {
			if (container == null)
				throw new ArgumentNullException("container");

			this.container = container;
		}

		[DebuggerStepThrough] // If Unity cannot resolve a type, it will throw an exception. We don't want this when "Break on exceptions" is activated.
		public override object GetService(Type serviceType) {
			try {
				return container.Resolve(serviceType);
			} catch (Exception) {
				return base.GetService(serviceType);
			}
		}

		[DebuggerStepThrough] // If Unity cannot resolve a type, it will throw an exception. We don't want this when "Break on exceptions" is activated.
		public override IEnumerable<object> GetServices(Type serviceType) {
			List<object> result = new List<object>();

			try {
				result.AddRange(container.ResolveAll(serviceType));
			}
			finally {
				result.AddRange(base.GetServices(serviceType));
			}

			return result;
		}
	}
}