using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.Interfaces;
using MyDocuments.BLL.DTO;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.DAL.Entities;
using AutoMapper;

namespace MyDocuments.BLL.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        public DocumentService(IUnitOfWork db) : base(db)
        {
        }

        public async Task<List<DocumentDTO>> GetAllDocuments()
        {
            throw new NotImplementedException();
            //throw new NotImplementedException()
            var documents = await UoW.Documents.GetAll();
            return Mapper.Map<List<Document>, List<DocumentDTO>>(documents);
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            var document = await UoW.Documents.Get(id);
            return Mapper.Map<Document, DocumentDTO>(document);
        }
        public async Task<bool> RemoveDocumentById(int id)
        {
            var document = await UoW.Documents.Get(id);
            if (document != null)
            {
                UoW.Documents.Remove(document);
                await UoW.Documents.SaveAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> AddDocument(DocumentDTO documentDTO)
        {
            var document = Mapper.Map<DocumentDTO, Document>(documentDTO);
            UoW.Documents.Add(document);
            await UoW.Documents.SaveAsync();
            return true;
        }
        public async Task<bool> UpdateDocumentById(int id, DocumentDTO documentDTO)
        {
            var document = await UoW.Documents.Get(id);
            if (document != null)
            {
                documentDTO.ModifiedDate = DateTime.UtcNow; // TODO : add trigger
                document = Mapper.Map<DocumentDTO, Document>(documentDTO, document);
                await UoW.Documents.SaveAsync();
                return true;
            }
            return false;

        }


    }
}
