using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Map;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.PL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.Facades
{
    public class FacadeFavouriteDocument : BaseFacade
    {
        public FacadeFavouriteDocument(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<DocumentDTO>> GetFavouriteDocumentsByUserIdAsync(int id)
        {
            var favouriteDocuments = await UoW.FavouriteDocuments.GetListFavouriteDocuments(id);

            return MapService.ToListDto(favouriteDocuments.ToList());

        }

        public async Task<bool> AddDocumentToFavouriteAsync(FavouriteDocumentDTO favouriteDocumentDTO)
        {
            var document = MapService.FavouriteDocumentToEntity(favouriteDocumentDTO);
            var checkDocument = await UoW.FavouriteDocuments.GetFavouriteDocumentEntity(favouriteDocumentDTO.DocumentId, favouriteDocumentDTO.UserId);
            if (checkDocument == null)
            {
                UoW.FavouriteDocuments.Add(document);
            }
            else
                // throw new Exception("Document  already is a favourite document.");     
                return false;
            

            await UoW.FavouriteDocuments.SaveAsync();
            return true;
        }

        public async Task DeleteDocumentFromFavourites(int documentId, int userId)
        {
            var documentForDeleting = await UoW.FavouriteDocuments.GetFavouriteDocumentEntity(documentId, userId);

            if (documentForDeleting == null)
            {
                throw new NotFoundDocumentException("There isn't document with id = {0}", documentId);
            }

            UoW.FavouriteDocuments.Remove(documentForDeleting);

            await UoW.FavouriteDocuments.SaveAsync();
        }

    }
}
