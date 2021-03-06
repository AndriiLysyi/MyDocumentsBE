﻿using MyDocuments.PL.Filters;
using MyDocuments.PL.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace MyDocuments.PL
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            string url = ConfigurationManager.AppSettings["ApiUrl"];
            config.EnableCors(new EnableCorsAttribute(url, headers: "*", methods: "*"));

            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ExceptionHandlingAttribute());
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{id2}",
                defaults: new { id = RouteParameter.Optional , id2 = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
