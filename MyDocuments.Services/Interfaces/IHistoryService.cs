using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;

namespace MyDocuments.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<List<HistoryDTO>> GetHistoryByUserId(int id);
        Task AddQueryToHistoryById(int id, string query);
    }
}
