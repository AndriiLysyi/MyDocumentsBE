using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Map;
using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.Facades
{
   public class FacadeFavouriteDocument: BaseFacade
    {
        public FacadeFavouriteDocument(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<FavouriteDocumentDTO>> GetFavouriteDocumentsByUserIdAsync(int id)
        {
            var favouriteDocuments = await UoW.FavouriteDocuments.GetList(id);
            var result = favouriteDocuments.ToList();
            return MapService.ToListFavouriteDocumentDto(result);
        }

    }
}
