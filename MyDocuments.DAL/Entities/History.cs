using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.DAL.Entities
{
    public class History
    {
        public int Id { get; set; }
        public string SearchQuery { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
