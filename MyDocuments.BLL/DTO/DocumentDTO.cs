using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDocuments.BLL.DTO
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(ValidationRules.MAX_LENGTH_NAME,
            ErrorMessage = "Name too long")]
        [RegularExpression(ValidationRules.ONLY_LETTERS_AND_NUMBERS,
            ErrorMessage = "Name not valid")]
        public string Name { get; set; }
        [Required]
        [StringLength(ValidationRules.MAX_DESCRIPTION_LENGTH,
            ErrorMessage = "Description too long")]
        public string Description { get; set; }
        [Required]
        [StringLength(ValidationRules.MAX_LENGTH_NAME,
            ErrorMessage = "Author too long")]
        [RegularExpression(ValidationRules.ONLY_LETTERS_AND_NUMBERS,
            ErrorMessage = "Author not valid")]
        public string Author { get; set; }
        [Required]
        [RegularExpression(ValidationRules.DOCUMENT_Type,
            ErrorMessage = "Type could be only ['txt','pdf','doc','docx'] ")]
        public string Type { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
