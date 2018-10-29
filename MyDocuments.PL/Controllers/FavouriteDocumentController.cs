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

    [RoutePrefix("api/favouriteDocument")]
    public class FavouriteDocumentController : ApiController
    {      
            private readonly IFavouriteDocumentService favouriteDocumentService;
            public FavouriteDocumentController(IFavouriteDocumentService favouriteDocService)
            {

                this.favouriteDocumentService = favouriteDocService;
            }

        [HttpGet]
        public async Task<HttpResponseMessage> GetFavDoc()
        {
            var userId = int.TryParse(Request.Properties[HistoryHandler.userId].ToString(), out int id);
            if (userId)
            {
                var favouriteDocuments = await favouriteDocumentService.GetFavouriteDocumentByUserId(id);
                if (favouriteDocuments.Count != 0)
                {
                    return Request.CreateResponse<IEnumerable<DocumentDTO>>(HttpStatusCode.OK, favouriteDocuments);
                }
            }

            const string message = "No favourite documents in database for this user.";
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
        }

        [HttpPost]
        
        public async Task<HttpResponseMessage> Post([FromBody]FavouriteDocumentDTO favouriteDocumentDTO)
        {
           
            bool success= await favouriteDocumentService.AddDocument(favouriteDocumentDTO);
            if (success)
            {
                var message = $"Document added to favourite";
                return Request.CreateResponse(HttpStatusCode.Created,message);
            }
            else
            {
                var message = $"Document already is a favourite document ";
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteDocumentFromFavourites(FavouriteDocumentDTO favouriteDocumentDTO )
        {
            await favouriteDocumentService.DeleteDocumentFromFavourites( favouriteDocumentDTO.DocumentId, favouriteDocumentDTO.UserId);

            return Request.CreateResponse(HttpStatusCode.OK, $"Succesfully deleted document from favourites .");
        }
    }
}