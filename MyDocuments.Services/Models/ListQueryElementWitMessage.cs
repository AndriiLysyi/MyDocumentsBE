using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.Services.Models
{
    public class ListQueryElementWitMessage
    {
        public List<QueryElement> qElements;
        public string message { get; set; }

        public ListQueryElementWitMessage (List<QueryElement> elements,  string message )
        {
            this.qElements = elements;
            this.message = message;
        }
    }
}
