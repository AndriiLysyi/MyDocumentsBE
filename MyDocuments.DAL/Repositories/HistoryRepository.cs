using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using MyDocuments.DAL.EF;
using MyDocuments.DAL.Repositories.Interfaces;

namespace MyDocuments.DAL.Repositories
{
    public class HistoryRepository : Repository<History>, IHistoryRepository
    {
        public HistoryRepository(DocumentContext context) : base(context)
        {
        }
        public async Task<IQueryable<History>> GetIQueryable(int id)
        {
            return DbSet.Where(i => i.UserId == id).OrderByDescending(i => i.CreateDate).AsQueryable();
        }
    }
}
