using MyDocuments.BLL.DTO;
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
    [RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        private readonly IDocumentService documentService;
        public DocumentController(IDocumentService serv)
        {
            this.documentService = serv;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            var documents = await documentService.GetAllDocuments();

            if (documents.Count() != 0)
            {
                return Request.CreateResponse<IEnumerable<DocumentDTO>>(HttpStatusCode.OK, documents);
            }
            const string message = "No documents in database.";
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
        }


        [HttpGet]
        [Route("{pageNumber}/{pageSize}")]
        public async Task<HttpResponseMessage> Get(int pageNumber, int pageSize)
        {
            var documents = await documentService.GetDocumentsInPagedList(pageNumber, pageSize);

            if (documents!= null)
            {
                return Request.CreateResponse<PagedListDocumentDTO>(HttpStatusCode.OK, documents);
            }
            const string message = "No documents in database.";
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var document = await documentService.GetDocumentById(id);
            if (document != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, document);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There isn't document with id = {id}");
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]DocumentDTO document)
        {
            await documentService.AddDocument(document);

            var okMessage = $"Succesfully created document: {document.Name}";
            return Request.CreateResponse(HttpStatusCode.OK, okMessage);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]DocumentDTO document)
        {
            await documentService.UpdateDocumentById(id, document);

            var message = $"Succesfully updated document with id = {id} ";
            return Request.CreateResponse(HttpStatusCode.OK, "Succesfully updated document.");
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            await documentService.RemoveDocumentById(id);

            return Request.CreateResponse(HttpStatusCode.OK, $"Succesfully deleted document id: {id}.");
        }
    }
}
