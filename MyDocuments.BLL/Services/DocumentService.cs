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
            var AllDocuments = await UoW.Documents.GetAll();
            return Mapper.Map<List<Document>, List<DocumentDTO>>(AllDocuments);
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            var Document = await UoW.Documents.Get(id);
            return Mapper.Map<Document, DocumentDTO>(Document);
        }
        public async Task<bool> RemoveDocumentById(int id)
        {
            var Document = await UoW.Documents.Get(id);
            if (Document != null)
            {
                UoW.Documents.Remove(Document);
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
        public async Task<bool> UpdateDocumentById(int id, DocumentDTO document)
        {
            var Document = await UoW.Documents.Get(id);
            // TODO: add validator dto  
            if (Document != null)
            {
                Document.Author = document.Author;
                Document.Description = document.Description;
                Document.Name = document.Name;
                Document.Type = document.Type;
                Document.ModifiedDate = DateTime.UtcNow; // TODO : add trigger
                UoW.Documents.Update(Document);
                await UoW.Documents.SaveAsync();
                return true;
            }
            return false;
        }


    }
}
