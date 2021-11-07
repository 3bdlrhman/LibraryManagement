using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Borrowings
    {
        [Key]
        public int BorrowingID { get; set; }
        [DisplayName("Delivered Back")]
        public bool DeliverdBack { get; set; }

        public virtual Book book { get; set; }
        public virtual Borrower borrower { get; set; }
    }
}