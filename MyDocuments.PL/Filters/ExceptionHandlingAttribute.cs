using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace MyDocuments.PL.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(context.Exception.Message)
                };
            }
            else
            {
                if (context.Exception is NoDocumentsException)
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent(context.Exception.Message)
                    };
                }
                else
                    if (context.Exception is Exception)
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(context.Exception.Message)
                    };
                }
                else
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("An error occurred, please try again or contact the administrator."),
                        ReasonPhrase = "Critical Exception"
                    };
                }
            }
            


        }
    }
}