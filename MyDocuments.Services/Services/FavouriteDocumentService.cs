using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Facades;
using MyDocuments.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.Services.Services
{
    public class FavouriteDocumentService : IFavouriteDocumentService
    {
        
        private readonly FacadeFavouriteDocument facadeFavouriteDocument;
        public FavouriteDocumentService(FacadeFavouriteDocument facadeFav)
        {
            this.facadeFavouriteDocument = facadeFav;
        }
        public async Task<List<DocumentDTO>> GetFavouriteDocumentByUserId(int id)
        {
            if (id <= 0) throw new Exception("Id should be more than 0");
            var favouriteDocument = await facadeFavouriteDocument.GetFavouriteDocumentsByUserIdAsync(id);

            return favouriteDocument;
        }
        public async Task<bool> AddDocument(FavouriteDocumentDTO favouriteDocumentDTO)
        {
            return await facadeFavouriteDocument.AddDocumentToFavouriteAsync(favouriteDocumentDTO);
          
        }

        public async Task DeleteDocumentFromFavourites(int documentId, int userId)
        {
            await facadeFavouriteDocument.DeleteDocumentFromFavourites(documentId,userId);
        }

    }
}
