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
        Task<IQueryable<FavouriteDocument>> GetList(int id);
    }
}
