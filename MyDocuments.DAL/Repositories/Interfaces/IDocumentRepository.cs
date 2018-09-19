﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;

namespace MyDocuments.DAL.Repositories.Interfaces
{
    public interface IDocumentRepository: IRepository<Document>
    {
        Task<IQueryable<Document>> GetPagedList(string criterion, string direction);

    }
}
