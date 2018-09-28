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

        public async Task<PagedListDocumentDTO> GetDocumentsByParameters(DocumentsParameters documentsParameters)
        {
            if (documentsParameters.direction != Direction.desc.ToString())
            {
                documentsParameters.direction = "asc";
            }

            var pagedListDocumentDto = new PagedListDocumentDTO();
            var documents = await UoW.Documents.GetPagedList(documentsParameters.criterion, documentsParameters.direction);

            List<Document> result = new List<Document>();

            //if (!string.IsNullOrEmpty(documentsParameters.searchValue))
            //{
            //    string  [] separator = { " " };
            //    List<string> elementsNot = new List<string>();
            //    List<string> elementsAnd = new List<string>();

            //    string [] searchList = documentsParameters.searchValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            //    for (int i = 0; i < searchList.Length; i++)
            //    {
            //        if (searchList[i] == "not")
            //        {
            //            elementsNot.Add(searchList[i+1]);
            //        }

            //        if (searchList[i] == "and")
            //        {
            //            elementsAnd.Add(searchList[i - 1]);
            //            elementsAnd.Add(searchList[i + 1]);
            //        }
            //    }
            //    documents = documents.Where(p => !elementsNot.Any(val => p.Name.Contains(val) || p.Description.Contains(val) || p.Author.Contains(val)))
            //        .Where(p => !elementsAnd.Any(val => p.Name.Contains(val) && p.Description.Contains(val) && p.Author.Contains(val)));
            //}

            pagedListDocumentDto.PageSize = documentsParameters.pageSize;
            pagedListDocumentDto.TotalCount = documents.Count();
            pagedListDocumentDto.NumberOfPages = (int)Math.Ceiling(pagedListDocumentDto.TotalCount / (double)pagedListDocumentDto.PageSize);

            if (documentsParameters.pageNumber > pagedListDocumentDto.NumberOfPages || documentsParameters.pageNumber < 0)
            {
                if (pagedListDocumentDto.TotalCount == 0)
                {
                    throw new NoDocumentsException("Page number should be 0 with PageSize = '{0}'", pagedListDocumentDto.PageSize);
                }
                else
                    throw new NoDocumentsException("Page number should be between 0 and '{0}' with PageSize = '{1}'", pagedListDocumentDto.NumberOfPages, pagedListDocumentDto.PageSize);
            }

            pagedListDocumentDto.PageNumber = documentsParameters.pageNumber;
            var pagedListEntities = documents.Skip((pagedListDocumentDto.PageNumber) * pagedListDocumentDto.PageSize).Take(pagedListDocumentDto.PageSize).ToList();

            pagedListDocumentDto = MapService.ToPagedListDto(pagedListDocumentDto, pagedListEntities);
            return pagedListDocumentDto;
        }

        public async Task<DocumentDTO> GetDocumentByIdAsync(int id)
        {
            var document = await UoW.Documents.Get(id);

            return MapService.ToDto(document);
        }

        public async Task RemoveDocumentById(int [] documentId)
        {
            foreach (var id in documentId)
            {
                var document = await UoW.Documents.Get(id);
                if (document == null)
                {
                    throw new NotFoundDocumentException("There isn't document with id = {0}",id);                        
                }
                UoW.Documents.Remove(document);
            }       
            
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
                throw new NotFoundDocumentException("There isn't document with id = {0}", id);
            }
            documentDTO.ModifiedDate = DateTime.UtcNow;

            UoW.Documents.Update(MapService.ToEntityForUpdate(documentDTO, document));
            await UoW.Documents.SaveAsync();
            return MapService.ToDto(document);
        }

    }
}
