﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.BLL.DTO;

namespace MyDocuments.BLL.Interfaces
{
    public interface IDocumentService
    {
        Task<List <DocumentDTO>> GetAllDocuments();
        Task<DocumentDTO> GetDocumentById(int id);
        Task<bool> RemoveDocumentById(int id);
        Task<bool> AddDocument(DocumentDTO documentDTO );
        Task<bool> UpdateDocumentById(int id, DocumentDTO dto);
    }
}
