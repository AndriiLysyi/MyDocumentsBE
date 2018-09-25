using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDocuments.PL.Filters
{
    public class NotFoundDocumentException : Exception
    {
        public NotFoundDocumentException()
        {
        }

        public NotFoundDocumentException(string message) : base(message)
        {
        }
        public NotFoundDocumentException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public NotFoundDocumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
