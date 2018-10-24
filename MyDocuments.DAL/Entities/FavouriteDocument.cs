using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.DAL.Entities
{
    public class FavouriteDocument
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Document")]
        public int DocumentId { get; set; }

        public  Document Document { get; set; }
    }

}
