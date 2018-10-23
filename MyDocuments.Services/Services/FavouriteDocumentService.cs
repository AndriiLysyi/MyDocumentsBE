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
    class FavouriteDocumentService : IFavouriteDocumentService
    {
      
            private readonly FacadeFavouriteDocument facadeFavouriteDocument;
            public FavouriteDocumentService(FacadeFavouriteDocument facade)
            {
                this.facadeFavouriteDocument = facade;
            }
            public async Task<List<FavouriteDocumentDTO>> GetFavouriteDocumentByUserId(int id)
            {
                if (id <= 0) throw new Exception("Id should be more than 0");
               var favouriteDocument = await facadeFavouriteDocument.GetFavouriteDocumentsByUserIdAsync(id);
              
                return favouriteDocument;
            }
          
        
    }
}
