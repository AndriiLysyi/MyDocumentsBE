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
        public async Task<IEnumerable<Document>> GetListFavouriteDocuments(int id)
        {            
            return await Context.FavouriteDocuments.Where(doc => doc.UserId == id).Select(i => i.Document).ToListAsync();        
        }

        public async Task<FavouriteDocument> GetFavouriteDocumentEntity(int idDocument, int idUser)
        {
            return await Context.FavouriteDocuments.Where(doc => doc.DocumentId== idDocument && doc.UserId==idUser).FirstOrDefaultAsync();
        }
    }
}
