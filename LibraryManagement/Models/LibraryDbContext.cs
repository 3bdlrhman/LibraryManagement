using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("LibraryDB")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<Borrowings> Borrowings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new LibraryDbInitializer());
            base.OnModelCreating(modelBuilder);
        }

        // To inilize the values of the database when dropping it and re-create OR (when the model changes)
        private class LibraryDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryDbContext>
        {
            protected override void Seed(LibraryDbContext context)
            {
                var BookList = new List<Book>
                {
                    new Book{ BookName="Anna Karenina", AuthorName="Leo Tolstoy", AvailableCopiesNumber=19, TotalCopiesNumber=19, AvailableForBorrowing=true},
                    new Book{ BookName="Madame Bovary", AuthorName="Gustav Flaubert", AvailableCopiesNumber=23, TotalCopiesNumber=25, AvailableForBorrowing=true},
                    new Book{ BookName="War and Peace", AuthorName="Leo Tolstoy", AvailableCopiesNumber=8, TotalCopiesNumber=20, AvailableForBorrowing=true},
                    new Book{ BookName="Lolita", AuthorName="Vladimir Nabokov", AvailableCopiesNumber=26, TotalCopiesNumber=35, AvailableForBorrowing=true},
                    new Book{ BookName="The Adventures of Huckleberry Finn", AuthorName="Mark Twain", AvailableCopiesNumber=4, TotalCopiesNumber=15, AvailableForBorrowing=true},
                    new Book{ BookName="Hamlet", AuthorName="William Shakespeare", AvailableCopiesNumber=12, TotalCopiesNumber=20, AvailableForBorrowing=true},
                    new Book{ BookName="The Great Gatsby", AuthorName="F. Scott Fizgerald", AvailableCopiesNumber=17, TotalCopiesNumber=40, AvailableForBorrowing=true},
                    new Book{ BookName="In Search of Lost Time", AuthorName="Marcel Proust", AvailableCopiesNumber=41, TotalCopiesNumber=55, AvailableForBorrowing=true},
                    new Book{ BookName="The Stories of Anton Chekhov", AuthorName="Anton Chekhov", AvailableCopiesNumber=39, TotalCopiesNumber=44, AvailableForBorrowing=true},
                    new Book{ BookName="Middlemarch", AuthorName="George Eliot", AvailableCopiesNumber=18, TotalCopiesNumber=25, AvailableForBorrowing=true}
                };

                var BorrowersList = new List<Borrower>
                {
                    new Borrower{ BorrowerName="Kelly L. Hunger" },
                    new Borrower{ BorrowerName="Mark M. Owens" },
                    new Borrower{ BorrowerName="Gladys L. Porter" },
                    new Borrower{ BorrowerName="Jason L. Lorenzo" },
                    new Borrower{ BorrowerName="Ryan A. Rogers" },
                    new Borrower{ BorrowerName="Cathy A. Minton" },
                    new Borrower{ BorrowerName="Angie W. Furey" }
                };

                context.Borrowers.AddRange(BorrowersList);
                context.Books.AddRange(BookList);
                base.Seed(context);
            }
        }
    }
}