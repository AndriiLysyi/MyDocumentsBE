using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.DTO
{
    public class PagedListDocumentDTO
    {
        public int PageNumber { get; set; } = 1;
        public List<DocumentDTO> Items { get; set; }
        public int PageSize { get; set; } = 10;
        public int NumberOfPages { get; set; }
       
    }
}
