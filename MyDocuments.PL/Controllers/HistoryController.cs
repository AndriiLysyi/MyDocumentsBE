using MyDocuments.PL.Handlers;
using MyDocuments.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyDocuments.PL.Controllers
{
    [RoutePrefix("api/history")]
    public class HistoryController : ApiController
    {
        private readonly IHistoryService historyService;
        public HistoryController( IHistoryService serv1)
        {
            
            this.historyService = serv1;
        }
        [HttpGet]

        public async Task<HttpResponseMessage> Get()
        {
            var userId = Request.Properties[HistoryHandler.userId];
            if (int.TryParse(userId.ToString(), out int id))
            {
                var history = await historyService.GetHistoryByUserId(id);
                if ( history.Count()!=0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, history);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There aren`t search histories for user with id = {id}");
        }
    }
}
