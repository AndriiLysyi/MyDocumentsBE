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

        public static  List<Document> ToListEntity(List<DocumentDTO> documentsDTO)
        {
            return Mapper.Map< List<DocumentDTO>, List<Document>>(documentsDTO);
        }

        public static DocumentDTO ToDto(Document documents)
        {
            return Mapper.Map<Document, DocumentDTO>(documents);
        }
        public static Document ToEntity(DocumentDTO documentsDTO)
        {
            return Mapper.Map< DocumentDTO, Document >(documentsDTO);
        }
        public static Document ToEntityForUpdate(DocumentDTO documentDTO, Document document)
        {
            return Mapper.Map<DocumentDTO, Document>(documentDTO, document);
        }

    }
}
