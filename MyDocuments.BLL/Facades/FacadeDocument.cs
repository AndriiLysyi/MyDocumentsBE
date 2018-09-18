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
            var pagedListDocumentDto = new PagedListDocumentDTO();
            pagedListDocumentDto.PageSize = pageSize;
            pagedListDocumentDto.TotalCount = documents.Count();
            pagedListDocumentDto.NumberOfPages = (int)Math.Ceiling(pagedListDocumentDto.TotalCount / (double)pagedListDocumentDto.PageSize) - 1;
            if (pageNumber > pagedListDocumentDto.NumberOfPages || pageNumber < 0 )
            {
                return pagedListDocumentDto;
            }
            pagedListDocumentDto.PageNumber = pageNumber;
            var pagedListEntities =  documents.Skip((pagedListDocumentDto.PageNumber ) * pagedListDocumentDto.PageSize).Take(pagedListDocumentDto.PageSize).ToList();
            pagedListDocumentDto = MapService.ToPagedListDto(pagedListDocumentDto , pagedListEntities);

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
        public async Task<DocumentDTO> AddDocumentAsync(DocumentDTO documentDTO)
        {
            documentDTO.CreateDate = DateTime.UtcNow;
            var document = MapService.ToEntity(documentDTO);
            UoW.Documents.Add(document);
            await UoW.Documents.SaveAsync();
            return MapService.ToDto(document); 
        }
        public async Task<DocumentDTO> UpdateDocumentAsync(int id, DocumentDTO documentDTO)
        {
            var document = await UoW.Documents.Get(id);
            if (document == null)
            {
                throw new Exception($"There isn't document with id = {id}");
            }
            documentDTO.ModifiedDate = DateTime.UtcNow;

            UoW.Documents.Update(MapService.ToEntityForUpdate(documentDTO, document));
            await UoW.Documents.SaveAsync();
            return MapService.ToDto(document);
        }



    }
}
