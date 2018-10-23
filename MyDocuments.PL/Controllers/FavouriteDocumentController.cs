using MyDocuments.BLL.DTO;
using MyDocuments.PL.Handlers;
using MyDocuments.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyDocuments.PL.Controllers
{

    [RoutePrefix("api/favourite")]
    public class FavouriteDocumentController : ApiController
    {      
            private readonly IFavouriteDocumentService favouriteDocumentService;
            public FavouriteDocumentController(IFavouriteDocumentService favouriteDocService)
            {

                this.favouriteDocumentService = favouriteDocService;
            }

            [HttpGet]
            public async Task<HttpResponseMessage> Get()
            {
            var userId = int.TryParse(Request.Properties[HistoryHandler.userId].ToString(), out int id);
            if (userId)
            {
                var favouriteDocuments = await favouriteDocumentService.GetFavouriteDocumentByUserId(id);
                if (favouriteDocuments.Count != 0)
                {
                    return Request.CreateResponse<IEnumerable<FavouriteDocumentDTO>>(HttpStatusCode.OK, favouriteDocuments);
                }
            }        
                           
            const string message = "No favourite documents in database for this user.";
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
        }
        
    }
}