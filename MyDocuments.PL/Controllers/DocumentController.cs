using MyDocuments.BLL.DTO;
using MyDocuments.PL.Models;
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

            if (documents.Count != 0)
            {
                return Request.CreateResponse<IEnumerable<DocumentDTO>>(HttpStatusCode.OK, documents);
            }
            const string message = "No documents in database.";
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, message);
        }


        [HttpPost]
        [Route("getDocuments")]
        public async Task<HttpResponseMessage> GetDocumentsByParameters( [FromBody] DocumentsParameters documentsParameters)
        {
            var documents = await documentService.GetDocumentsByParameters( documentsParameters);

            return Request.CreateResponse(HttpStatusCode.OK, documents);
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
        public async Task<HttpResponseMessage> Post([FromBody]DocumentDTO documentDTO)
        {
           var document = await documentService.AddDocument(documentDTO);
            if (document != null)
            {               
                return Request.CreateResponse(HttpStatusCode.Created, document);
            }
            var message = $"Can`t create document ";
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadFile([FromBody]DocumentDTO documentDTO)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var document = await documentService.AddDocument(documentDTO);
                    if (document != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, document);
                    }
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Succesfully uploaded file");
            }
            string message = $"Can`t create document ";
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]DocumentDTO documentDTO)
        {
            var document = await documentService.UpdateDocumentById(id, documentDTO);
            if (document != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, document);
            }
            var message = $"Can`t update document with id = {id} ";
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
        }


        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromBody] int[] documentId)
        {
            await documentService.RemoveDocumentById(documentId);

            return Request.CreateResponse(HttpStatusCode.OK, $"Succesfully deleted array of documents .");
        }
    }
}
