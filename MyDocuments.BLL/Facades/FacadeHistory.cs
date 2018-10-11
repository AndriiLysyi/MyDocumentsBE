using MyDocuments.BLL.DTO;
using MyDocuments.BLL.Map;
using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.Facades
{
    public class FacadeHistory : BaseFacade
    {
        public FacadeHistory(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<HistoryDTO>> GetHistoryByUserIdAsync(int id)
        {
            var history = await UoW.Histories.GetIQueryable(id);
            var result = history.Take(10).ToList();
            return MapService.ToListHistoryDto(result);
        }

        public async Task AddHistoryByUserIdAndQuery(int id, string query)
        {
            var history = await UoW.Histories.GetIQueryable(id);
            var result = history.Where(i => i.SearchQuery == query).FirstOrDefault();
            if (result != null)
            {
                result.CreateDate = DateTime.UtcNow;
                UoW.Histories.Update(result);
                await UoW.Histories.SaveAsync();
            }
            else
            {
                HistoryDTO dto = new HistoryDTO() { UserId = id , SearchQuery = query, CreateDate = DateTime.UtcNow};
                UoW.Histories.Add(MapService.ToHistoryEntity(dto));
                await UoW.Histories.SaveAsync();
            }
        }
    }
}
