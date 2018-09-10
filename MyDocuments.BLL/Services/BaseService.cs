using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Repositories.Interfaces;
namespace MyDocuments.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UoW;
        public BaseService(IUnitOfWork db)
        {
            this.UoW = db;
        }
    }
}
