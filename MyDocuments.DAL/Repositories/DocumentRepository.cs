using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using System.Data.Entity;
using MyDocuments.DAL.EF;
using MyDocuments.DAL.Repositories.Interfaces;

namespace MyDocuments.DAL.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DocumentContext context) : base(context)
        {
        }
        public async Task<IQueryable<Document>> GetPagedList()
        {
           return  DbSet.OrderBy(a=> a.Id).AsQueryable();
        }
    }
}
