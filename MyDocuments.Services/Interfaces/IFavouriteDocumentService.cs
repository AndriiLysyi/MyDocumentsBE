using MyDocuments.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.Services.Interfaces
{
   public interface IFavouriteDocumentService
    {
        Task<List<DocumentDTO>> GetFavouriteDocumentByUserId(int id);
        Task DeleteDocumentFromFavourites(int documentId, int userId);
        Task<bool> AddDocument(FavouriteDocumentDTO favouriteDocumentDTO);
    }
}
