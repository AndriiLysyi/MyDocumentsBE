using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using MyDocuments.DAL.Repositories.Interfaces;
using MyDocuments.Services.Interfaces;

namespace MyDocuments.Services.Services
{
    public class BaseService: IBaseService
    {

        protected readonly IUnitOfWork UoW;
        public BaseService(IUnitOfWork db)
        {
            this.UoW = db;
        }
        public async Task<List<Document>> GetAllAsync()
        {
            return await UoW.Documents.GetAll();
        }
        public async Task<Document> GetAsync(int id)
        {
            if (id <= 0) throw new Exception("Id should be more than 0");
            return await UoW.Documents.Get(id);
        
        }
        public async Task AddAsync(Document document)
        {
            UoW.Documents.Add(document);
            await UoW.Documents.SaveAsync();

        }

        public async Task UpdateAsync(Document document)
        {
            UoW.Documents.Update(document);
            await UoW.Documents.SaveAsync();

        }


    }
}
