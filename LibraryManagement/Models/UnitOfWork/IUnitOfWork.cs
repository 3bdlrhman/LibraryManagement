using LibraryManagement.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
         IGenericRepository<Book> Books { get; }
         IGenericRepository<Borrower> Borrowers { get; }
        IGenericRepository<Borrowings> Borrowings { get; }

        void SaveAll();
    }
}
