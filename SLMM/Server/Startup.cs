﻿using System.Reflection;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SLMM.Communication;
using SLMM.Core;
using SLMM.Server.Helpers;

namespace SLMM.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MessageHandlers.Add(new AddCorrelationIdToResponseHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            appBuilder.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
            appBuilder.UseWebApi(config);
        }

        public static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<ILownManager>().To<LownManager>().InSingletonScope();

            //Created initial machine
            kernel.Get<ILownManager>().Init(new LawnMowingMachine(10, 10, 0, 0));

            return kernel;
        }
    }
}
