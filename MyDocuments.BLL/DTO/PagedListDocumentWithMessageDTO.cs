using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.DTO
{
    public class PagedListDocumentWithMessageDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public List<DocumentDTO> Items { get; set; }
        public string Message { get; set; }
    }
}
