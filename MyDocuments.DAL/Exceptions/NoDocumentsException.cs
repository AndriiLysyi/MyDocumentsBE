using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDocuments.PL.Filters
{
    public class NoDocumentsException :Exception
    {
        public NoDocumentsException()
        {
        }

        public NoDocumentsException(string message) : base(message)
        {
        }
        public NoDocumentsException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public NoDocumentsException(string message, Exception innerException) : base(message, innerException)
        {
        }


    }
}