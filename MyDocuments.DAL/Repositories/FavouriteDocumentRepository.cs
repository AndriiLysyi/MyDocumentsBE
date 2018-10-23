using MyDocuments.DAL.EF;
using MyDocuments.DAL.Entities;
using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.DAL.Repositories
{
   public class FavouriteDocumentRepository : Repository<FavouriteDocument>, IFavouriteDocumentRepository
    {
        public FavouriteDocumentRepository(DocumentContext context) : base(context)
        {
        }
        public async Task<IQueryable<FavouriteDocument>> GetList(int id)
        {
            return DbSet.Where(i => i.UserId == id).AsQueryable();
        }
    }
}
