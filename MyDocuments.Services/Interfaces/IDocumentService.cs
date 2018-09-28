using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;
using MyDocuments.BLL;

namespace MyDocuments.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDTO>> GetAllDocuments();
        Task<DocumentDTO> GetDocumentById(int id);
        Task RemoveDocumentById(int [] documentId);
        Task<DocumentDTO> AddDocument(DocumentDTO documentDTO);
        Task<DocumentDTO> UpdateDocumentById(int id, DocumentDTO dto);
        Task<PagedListDocumentDTO> GetDocumentsByParameters(DocumentsParameters documentsParameters);
           // int pageNumber, int pageSize, string criterion, string direction, string searchValue);
    }
}
