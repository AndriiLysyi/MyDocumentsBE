using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IDocumentRepository Documents { get; }
        IHistoryRepository Histories { get; }
        IFavouriteDocumentRepository FavouriteDocuments { get; }

        Task SaveChangesAsync();
       
    }
}
