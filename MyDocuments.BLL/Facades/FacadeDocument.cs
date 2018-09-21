using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;
using MyDocuments.DAL.Entities;
using MyDocuments.BLL.Map;
using System.Runtime.Serialization;
using MyDocuments.PL.Filters;

namespace MyDocuments.BLL.Facades
{
    public class FacadeDocument : BaseFacade
    {
        public enum Direction
        {
            asc,
            desc
        }
        public FacadeDocument(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<DocumentDTO>> GetDocumentsAsync()
        {
            var documents = await UoW.Documents.GetAll();

            return MapService.ToListDto(documents);

        }
        public async Task<PagedListDocumentDTO> GetDocumentsInPagedListAsync(int pageNumber, int pageSize, string criterion, string direction, string searchValue)
        {
            if (direction != Direction.desc.ToString())
            {
                direction = "asc";
            }

            var pagedListDocumentDto = new PagedListDocumentDTO();
            var documents = await UoW.Documents.GetPagedList(criterion, direction);

            List<Document> result = new List<Document>();

            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchList = searchValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                documents = documents.Where(p => !searchList.Any(val =>  p.Description.Contains(val))).Where(p => searchList.Any(val => p.Name.Contains(val) && p.Author.Contains(val) ));


            }

            pagedListDocumentDto.PageSize = pageSize;
            pagedListDocumentDto.TotalCount = documents.Count();
            pagedListDocumentDto.NumberOfPages = (int)Math.Ceiling(pagedListDocumentDto.TotalCount / (double)pagedListDocumentDto.PageSize);

            if (pageNumber > pagedListDocumentDto.NumberOfPages || pageNumber < 0)
            {
                if (pagedListDocumentDto.TotalCount == 0)
                {
                    throw new NoDocumentsException("Page number should be 0 with PageSize = '{0}'", pagedListDocumentDto.PageSize);
                }
                else
                    throw new NoDocumentsException("Page number should be between 0 and '{0}' with PageSize = '{1}'", pagedListDocumentDto.NumberOfPages, pagedListDocumentDto.PageSize);
            }

            pagedListDocumentDto.PageNumber = pageNumber;
            var pagedListEntities = documents.Skip((pagedListDocumentDto.PageNumber) * pagedListDocumentDto.PageSize).Take(pagedListDocumentDto.PageSize).ToList();

            pagedListDocumentDto = MapService.ToPagedListDto(pagedListDocumentDto, pagedListEntities);
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
