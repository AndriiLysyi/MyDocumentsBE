using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;

namespace MyDocuments.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDTO>> GetAllDocuments();
        Task<DocumentDTO> GetDocumentById(int id);
        Task RemoveDocumentById(int id);
        Task<DocumentDTO> AddDocument(DocumentDTO documentDTO);
        Task<DocumentDTO> UpdateDocumentById(int id, DocumentDTO dto);
        Task<PagedListDocumentDTO> GetDocumentsInPagedList(int pageNumber, int pageSize);
    }
}
