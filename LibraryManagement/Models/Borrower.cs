using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Borrower
    {
        [Key]
        public int BorrowerID { get; set; }
        [DisplayName("Borrower Name"), Required(AllowEmptyStrings = false)]
        public string BorrowerName { get; set; }
    }
}