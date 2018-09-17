using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;

using MyDocuments.DAL.Entities;
using MyDocuments.BLL.Map;

namespace MyDocuments.BLL.Facades
{
    public class FacadeDocument: BaseFacade
    {
        public FacadeDocument(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<DocumentDTO>> GetDocumentsAsync()
        {
            var documents = await UoW.Documents.GetAll();

            return MapService.ToListDto(documents);

        }
        public async Task<PagedListDocumentDTO> GetDocumentsInPagedListAsync(int pageNumber, int pageSize)
        {
            var documents = await UoW.Documents.GetPagedList();
            int count = documents.Count();
            var pagedListDocumentDto = new PagedListDocumentDTO();
            pagedListDocumentDto.PageSize = (pageSize > 50 || pageSize < 5) ? 20 : pageSize;
            int TotalPages = (int)Math.Ceiling(count / (double)pagedListDocumentDto.PageSize);
            pagedListDocumentDto.PageNumber = (pageNumber > TotalPages || pageNumber < 1)? 1 : pageNumber;

            pagedListDocumentDto.NumberOfPages = TotalPages;

            pagedListDocumentDto.Items = MapService.ToListDto( documents.Skip((pagedListDocumentDto.PageNumber - 1) * pagedListDocumentDto.PageSize).Take(pagedListDocumentDto.PageSize).ToList());


            return pagedListDocumentDto;

        }

        public async Task<DocumentDTO> GetDocumentByIdAsync(int id)
        {
            var document = await UoW.Documents.Get(id);

            return MapService.ToDto(document);
        }

        public async Task RemoveDocumentById(int id)
        {
            var document = await UoW.Documents.Get(id);
            if (document == null)
            {
                throw new Exception($"There isn't document with id = {id}");
            }

            UoW.Documents.Remove(document);
            await UoW.Documents.SaveAsync();
        }
        public async Task AddDocumentAsync(DocumentDTO documentDTO)
        {
            documentDTO.CreateDate = DateTime.UtcNow;
            UoW.Documents.Add(MapService.ToEntity(documentDTO));
            await UoW.Documents.SaveAsync();
        }
        public async Task UpdateDocumentAsync(int id, DocumentDTO documentDTO)
        {
            var document = await UoW.Documents.Get(id);
            if (document == null)
            {
                throw new Exception($"There isn't document with id = {id}");
            }
            documentDTO.ModifiedDate = DateTime.UtcNow;

            UoW.Documents.Update(MapService.ToEntityForUpdate(documentDTO, document));
            await UoW.Documents.SaveAsync();
        }



    }
}
