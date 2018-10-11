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
using MyDocuments.Services.Services;
using MyDocuments.Services.Interfaces;
using MyDocuments.BLL.Facades;

namespace MyDocuments.PL.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterType<DocumentContext>().AsSelf().InstancePerRequest().WithParameter("connectionString", "DocumentContext");
            builder.RegisterType<DocumentService>().As<IDocumentService>().InstancePerRequest();
            builder.RegisterType<HistoryService>().As<IHistoryService>().InstancePerRequest();
           // builder.RegisterType<BaseService>().As<IBaseService>().InstancePerRequest();
            builder.RegisterType<FacadeDocument>().AsSelf().InstancePerRequest();
            builder.RegisterType<FacadeHistory>().AsSelf().InstancePerRequest();
            builder.Register(c => new UnitOfWork(c.Resolve<DocumentContext>())).AsImplementedInterfaces().InstancePerRequest();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
        
    }
}