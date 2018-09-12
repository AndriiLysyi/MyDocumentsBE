using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuments.BLL.DTO
{
    public class ValidationRules
    {
        public const int MAX_LENGTH_NAME = 50;
        public const string ONLY_LETTERS_AND_NUMBERS = @"^[a-zA-z0-9 ]*$";
        public const int MAX_DESCRIPTION_LENGTH = 500;
        public const string DOCUMENT_Type = @"^\b(txt|pdf|doc|docx)\b$";
    }
}
