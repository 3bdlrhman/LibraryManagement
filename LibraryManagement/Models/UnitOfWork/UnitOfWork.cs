using LibraryManagement.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext _context;
        public UnitOfWork()
        {
            _context = new LibraryDbContext();
        }

        public Repositories.IGenericRepository<Book> Books
        {
            get
            {
                return new EfGenericRepository<Book>(_context);
            }
        }

        public Repositories.IGenericRepository<Borrower> Borrowers
        {
            get
            {
                return new EfGenericRepository<Borrower>(_context);
            }
        }

        public Repositories.IGenericRepository<Borrowings> Borrowings
        {
            get
            {
                return new EfGenericRepository<Borrowings>(_context);
            }
        }

        public void SaveAll()
        {
            _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}