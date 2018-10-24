using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyDocuments.BLL.DTO;
using MyDocuments.DAL.Entities;

namespace MyDocuments.BLL.Map
{
    public static class MapService
    {
        public static List<DocumentDTO> ToListDto(List<Document> documents)
        {
            return Mapper.Map<List<Document>, List<DocumentDTO>>(documents);
        }

        public static List<Document> ToListEntity(List<DocumentDTO> documentsDTO)
        {
            return Mapper.Map<List<DocumentDTO>, List<Document>>(documentsDTO);
        }

        public static DocumentDTO ToDto(Document documents)
        {
            return Mapper.Map<Document, DocumentDTO>(documents);
        }
        public static Document ToEntity(DocumentDTO documentsDTO)
        {
            return Mapper.Map<DocumentDTO, Document>(documentsDTO);
        }

        public static FavouriteDocumentDTO FavouriteDocumentToDto(FavouriteDocument favouriteDocument)
        {
            return Mapper.Map<FavouriteDocument, FavouriteDocumentDTO>(favouriteDocument);
        }
        public static FavouriteDocument FavouriteDocumentToEntity(FavouriteDocumentDTO favouriteDocumentDTO)
        {
            return Mapper.Map<FavouriteDocumentDTO, FavouriteDocument>(favouriteDocumentDTO);
        }

        public static Document ToEntityForUpdate(DocumentDTO documentDTO, Document document)
        {
            documentDTO.Id = document.Id;
            documentDTO.Type = document.Type;
            documentDTO.CreateDate = document.CreateDate;
            return Mapper.Map<DocumentDTO, Document>(documentDTO, document);
        }
        public static PagedListDocumentDTO ToPagedListDto(PagedListDocumentDTO pagedListDocumentDTO, List<Document> documents)
        {
            if (pagedListDocumentDTO.PageNumber > 0)
            {
                pagedListDocumentDTO.HasPrevious = true;
            }
            if (pagedListDocumentDTO.PageNumber < pagedListDocumentDTO.NumberOfPages)
            {
                pagedListDocumentDTO.HasNext = true;
            }
            pagedListDocumentDTO.Items = ToListDto(documents);
            return pagedListDocumentDTO;
        }

        public static PagedListDocumentWithMessageDTO ToPagedListDocumentWithMessageDto(PagedListDocumentWithMessageDTO pagedListDocumentDTO, List<Document> documents)
        {
            if (pagedListDocumentDTO.PageNumber > 0)
            {
                pagedListDocumentDTO.HasPrevious = true;
            }
            if (pagedListDocumentDTO.PageNumber < pagedListDocumentDTO.NumberOfPages)
            {
                pagedListDocumentDTO.HasNext = true;
            }
            pagedListDocumentDTO.Items = ToListDto(documents);
            return pagedListDocumentDTO;
        }

        public static List<HistoryDTO> ToListHistoryDto(List<History> histories)
        {
            return Mapper.Map<List<History>, List<HistoryDTO>>(histories);
        }
        public static History ToHistoryEntity(HistoryDTO dto)
        {
            return Mapper.Map<HistoryDTO, History>(dto);
        }
        public static History ToHistoryEntityForUpdate(HistoryDTO dto, History history)
        {
            dto.Id = history.Id;
            dto.CreateDate = DateTime.UtcNow;
            return Mapper.Map<HistoryDTO, History>(dto, history);
        }
        public static List<FavouriteDocumentDTO> ToListFavouriteDocumentDto(List<FavouriteDocument> favouriteDocuments)
        {
            return Mapper.Map<List<FavouriteDocument>, List<FavouriteDocumentDTO>>(favouriteDocuments);
        }

    }
}
