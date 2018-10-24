using MyDocuments.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.DAL.Repositories.Interfaces
{
    public interface IFavouriteDocumentRepository : IRepository<FavouriteDocument>
    {
        Task<IEnumerable<Document>> GetListFavouriteDocuments(int id);
        Task<FavouriteDocument> GetFavouriteDocumentEntity(int idDocument, int idUser);
    }
}
