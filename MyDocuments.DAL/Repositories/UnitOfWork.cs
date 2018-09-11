using System.Threading.Tasks;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.DAL.EF;
using System;

namespace MyDocuments.DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DocumentContext  context;

        private IDocumentRepository documentsRepository;

        public UnitOfWork(DocumentContext context)
        {
            this.context = context;
        }

        public IDocumentRepository Documents => documentsRepository ?? (documentsRepository = new DocumentRepository(context));

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
           if(this.context != null)
           {
                context.Dispose();
           }
        }
    }
}
