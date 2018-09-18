using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.Facades
{
    public class BaseFacade
    {
        protected readonly IUnitOfWork UoW;
        public BaseFacade(IUnitOfWork db)
        {
            this.UoW = db;
        }
    }
}
