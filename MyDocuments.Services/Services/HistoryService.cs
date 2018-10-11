using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Facades;
using MyDocuments.Services.Interfaces;

namespace MyDocuments.Services.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly FacadeHistory facadeHistory;
        public HistoryService(FacadeHistory facade)
        {
            this.facadeHistory = facade;
        }
        public async Task<List<HistoryDTO>> GetHistoryByUserId(int id)
        {
            if (id <= 0) throw new Exception("Id should be more than 0");
            var history = await facadeHistory.GetHistoryByUserIdAsync(id);
            return history;
        }
        public async Task AddQueryToHistoryById(int id, string query)
        {
            await facadeHistory.AddHistoryByUserIdAndQuery(id, query);
        }
    }
}
