using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.Services.Interfaces;
using MyDocuments.BLL.DTO;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.DAL.Entities;
using AutoMapper;

namespace MyDocuments.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IBaseService baseService;
        public DocumentService(IBaseService baseService) 
        {
            this.baseService = baseService;
        }

        public async Task<List<DocumentDTO>> GetAllDocuments()
        {
            var documents = await baseService.GetAllAsync();
            return MapService.ToListDto(documents);
        }

        public async Task<DocumentDTO> GetDocumentById(int id)
        {
            
            var document = await baseService.GetAsync(id);
            return MapService.ToDto(document);
        }
        public async Task<bool> RemoveDocumentById(int id)
        {
            throw new NotImplementedException();

        }
        public async Task<bool> AddDocument(DocumentDTO documentDTO)
        {
            documentDTO.CreateDate = DateTime.UtcNow;
            await baseService.AddAsync(MapService.ToEntity(documentDTO));
            
            return true;
        }
        public async Task<bool> UpdateDocumentById(int id, DocumentDTO documentDTO)
        {

            var document = await baseService.GetAsync(id);
            if (document != null)
            {
                documentDTO.ModifiedDate = DateTime.UtcNow; // TODO : add trigger
                await baseService.UpdateAsync(MapService.ToEntityForUpdate(documentDTO,document));
                return true;
            }
            return false;

        }


    }
}
