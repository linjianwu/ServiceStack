using System;
using System.Collections.Generic;
using ServiceStack.Configuration;
using ServiceStack.Host;
using ServiceStack.Html;
using ServiceStack.IO;
using ServiceStack.Server;

namespace ServiceStack
{
	/// <summary>
	/// ASP.NET or HttpListener ServiceStack host
	/// </summary>
	public interface IAppHost : IResolver
	{
		/// <summary>
		/// Register dependency in AppHost IOC on Startup
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance"></param>
		void Register<T>(T instance);
		
		/// <summary>
		/// AutoWired Registration of an interface with a concrete type in AppHost IOC on Startup.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TAs"></typeparam>
		void RegisterAs<T, TAs>() where T : TAs;
		
		/// <summary>
		/// Allows the clean up for executed autowired services and filters.
		/// Calls directly after services and filters are executed.
		/// </summary>
		/// <param name="instance"></param>
		void Release(object instance);

        /// <summary>
        /// Called at the end of each request. Enables Request Scope.
        /// </summary>
	    void OnEndRequest();

        /// <summary>
        /// Register user-defined custom routes.
        /// </summary>
        IServiceRoutes Routes { get; }
		
		/// <summary>
		/// Register custom ContentType serializers
		/// </summary>
		IContentTypes ContentTypeses { get; }
		
        /// <summary>
        /// Add Request Filters, to be applied before the dto is deserialized
        /// </summary>
        List<Action<IHttpRequest, IHttpResponse>> PreRequestFilters { get; }

		/// <summary>
		/// Add Request Filters
		/// </summary>
		List<Action<IHttpRequest, IHttpResponse, object>> GlobalRequestFilters { get; }
		
		/// <summary>
		/// Add Response Filters
		/// </summary>
		List<Action<IHttpRequest, IHttpResponse, object>> GlobalResponseFilters { get; }

		/// <summary>
		/// Add alternative HTML View Engines
		/// </summary>
		List<IViewEngine> ViewEngines { get; }

        /// <summary>
        /// Provide an exception handler for un-caught exceptions
        /// </summary>
        HandleUncaughtExceptionDelegate ExceptionHandler { get; set; }

        /// <summary>
        /// Provide an exception handler for unhandled exceptions
        /// </summary>
        HandleServiceExceptionDelegate ServiceExceptionHandler { get; set; }

		/// <summary>
		/// Provide a catch-all handler that doesn't match any routes
		/// </summary>
		List<HttpHandlerResolverDelegate> CatchAllHandlers { get; }

		/// <summary>
		/// Provide a custom model minder for a specific Request DTO
		/// </summary>
		Dictionary<Type, Func<IHttpRequest, object>> RequestBinders { get; }

		/// <summary>
		/// The AppHost config
		/// </summary>
		AppHostConfig Config { get; }

		/// <summary>
		/// Register an Adhoc web service on Startup
		/// </summary>
		/// <param name="serviceType"></param>
		/// <param name="atRestPaths"></param>
		void RegisterService(Type serviceType, params string[] atRestPaths);

	    /// <summary>
	    /// List of pre-registered and user-defined plugins to be enabled in this AppHost
	    /// </summary>
        List<IPlugin> Plugins { get;  }

		/// <summary>
		/// Apply plugins to this AppHost
		/// </summary>
		/// <param name="plugins"></param>
		void LoadPlugin(params IPlugin[] plugins);

        /// <summary>
        /// Virtual access to file resources
        /// </summary>
		IVirtualPathProvider VirtualPathProvider { get; set; }

        /// <summary>
        /// Create a service runner for IService actions
        /// </summary>
	    IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext);

        /// <summary>
        /// Resolve the absolute url for this request
        /// </summary>
        string ResolveAbsoluteUrl(string virtualPath, IHttpRequest httpReq);
	}

	public interface IHasAppHost
	{
		IAppHost AppHost { get; }
	}
}