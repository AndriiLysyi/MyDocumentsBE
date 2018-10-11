using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.Services.Interfaces;
using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Facades;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.DAL.Entities;
using AutoMapper;

namespace MyDocuments.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FacadeDocument facadeDocument;
        public DocumentService(FacadeDocument facade1)
        {
            this.facadeDocument = facade1;
        }

        public async Task<List<DocumentDTO>> GetAllDocuments()
        {
            var documents = await facadeDocument.GetDocumentsAsync();
            return documents;
        }

        public async Task<PagedListDocumentDTO> GetDocumentsByParameters(DocumentsParameters documentsParameters)
        {

            if (documentsParameters.pageSize < 0) throw new Exception("Page size should more than 0");

            var documents = await facadeDocument.GetDocumentsByParameters(documentsParameters);
            return documents;
        }
        public async Task<PagedListDocumentWithMessageDTO> GetDocumentsByStrangeParameters(DocumentsParameters documentsParameters)
        {
            if (documentsParameters.pageSize < 0) throw new Exception("Page size should more than 0");
            PagedListDocumentWithMessageDTO documents;
            if (string.IsNullOrEmpty(documentsParameters.searchValue.Trim()))
            {
                documents = await facadeDocument.GetDocumentsByStrangeParameters(documentsParameters, null);
            }
            else
            {
                SearchPredicateService predicateService = new SearchPredicateService();
                var expression = predicateService.getExpression(documentsParameters.searchValue.Trim(), out string message);
                documents = await facadeDocument.GetDocumentsByStrangeParameters(documentsParameters, expression);
                if (!string.IsNullOrEmpty(message))
                {
                    documents.Message = message;
                }
            }
            return documents;
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            if (id <= 0) throw new Exception("Id should be more than 0");
            var document = await facadeDocument.GetDocumentByIdAsync(id);
            return document;
        }

        public async Task RemoveDocumentById(int[] documentId)
        {
            await facadeDocument.RemoveDocumentById(documentId);
        }

        public async Task<DocumentDTO> AddDocument(DocumentDTO documentDTO)
        {
            return await facadeDocument.AddDocumentAsync(documentDTO);
        }

        public async Task<DocumentDTO> UpdateDocumentById(int id, DocumentDTO documentDTO)
        {
            if (id <= 0) throw new Exception("Id should be more than 0");
            return await facadeDocument.UpdateDocumentAsync(id, documentDTO);
        }
    }
}
