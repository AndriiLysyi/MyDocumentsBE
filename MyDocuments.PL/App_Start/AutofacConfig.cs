using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using MyDocuments.DAL.EF;
using MyDocuments.DAL.Repositories;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.BLL.Services;
using MyDocuments.BLL.Interfaces;


namespace MyDocuments.PL.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<DocumentContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<DocumentService>().As<IDocumentService>().InstancePerRequest();
           // builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter(new InjectionConstructor("context", DocumentContext)) .InstancePerRequest();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
        
    }
}