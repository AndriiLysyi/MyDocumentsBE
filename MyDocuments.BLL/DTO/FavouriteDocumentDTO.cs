using MyDocuments.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.DTO
{
    public class FavouriteDocumentDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
    }
}
