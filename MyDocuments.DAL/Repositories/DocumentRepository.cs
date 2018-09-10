using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using System.Data.Entity;
using MyDocuments.DAL.Repositories.Interfaces;

namespace MyDocuments.DAL.Repositories
{
    public class DocumentRepository: Repository<Document>, IDocumentRepository
    {
        public DocumentRepository( DbContext context): base(context)
        {
        }
    }
}
