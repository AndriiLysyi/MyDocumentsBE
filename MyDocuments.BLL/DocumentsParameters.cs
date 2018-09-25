using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDocuments.PL.Models
{
    public class DocumentsParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string criterion { get; set; }
        public string direction { get; set; }
        public string searchValue { get; set; }
    }
}