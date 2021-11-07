using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [DisplayName("Book Name"), Required(AllowEmptyStrings = false)]
        public string BookName { get; set; }

        [DisplayName("Author Name"), Required(AllowEmptyStrings = false)]
        public string AuthorName { get; set; }

        [DisplayName("Total Copies")]
        [Range(1, 1000, ErrorMessage ="Total Copies must be between 1 and 1000")]
        public int TotalCopiesNumber { get; set; }

        [DisplayName("Available Copies")]
        [Range(0, 1000, ErrorMessage = "Available Copies must be > 0")]
        public int AvailableCopiesNumber { get; set; }
        [DefaultValue(true)]
        public bool AvailableForBorrowing { get; set; }
    }
}