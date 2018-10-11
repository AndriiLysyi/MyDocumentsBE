using MyDocuments.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;
using System.Web;

namespace MyDocuments.PL.Handlers
{
    public class HistoryHandler : DelegatingHandler
    {
        

        public static string userId = "uid";
        async protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {


            int id =-1;

            var userid = request.Headers.GetCookies(userId).FirstOrDefault();
            if (userid == null)
            {
                id = 1;
            }
            else
            {
                if (int.TryParse(userid[userId].Value, out id))
                { }
                else
                    id = 1;
            }

           

            // Store the session ID in the request property bag.
            request.Properties[userId] = (object) id ;

            // Continue processing the HTTP request.
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            // Set the session ID as a cookie in the response message.
            response.Headers.AddCookies(new CookieHeaderValue[] {
            new CookieHeaderValue(userId, id.ToString())
        });

            return response;
        }
    }
}