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

        private bool disposed = false;

        public UnitOfWork(DocumentContext context)
        {
            this.context = context;
        }

        public IDocumentRepository Documents => documentsRepository ?? (documentsRepository = new DocumentRepository(context));

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
