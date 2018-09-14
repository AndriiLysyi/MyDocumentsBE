using MyDocuments.DAL.Entities;
using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.Services.Interfaces
{
    public interface IBaseService
    {
        Task<List<Document>> GetAllAsync();
        Task AddAsync(Document document);
        Task<Document> GetAsync(int id);
        Task UpdateAsync(Document document);
    }
}
