using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using System.Data.Entity;
using MyDocuments.DAL.EF;
using MyDocuments.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocuments.DAL.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
       
        public DocumentRepository(DocumentContext context) : base(context)
        {
        }
        
        public async Task<IQueryable<Document>> GetPagedList( string criterion, string direction )
        {
            var nodeForExpressionTree = Expression.Parameter(typeof(Document), "n");
            var prop = Expression.Property(nodeForExpressionTree, criterion);

            var expession = Expression.Lambda(prop, nodeForExpressionTree);

            string method = direction == "asc" ? "OrderBy" : "OrderByDescending";

            Type[] types = new Type[] { DbSet.AsQueryable().ElementType, expession.Body.Type };

            var rs = Expression.Call(typeof(Queryable), method, types, DbSet.AsQueryable().Expression, expession);

            return DbSet.AsQueryable().Provider.CreateQuery<Document>(rs);


        }
    }
}
