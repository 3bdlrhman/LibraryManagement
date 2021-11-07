using LibraryManagement.Controllers;
using LibraryManagement.Models;
using LibraryManagement.Models.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagement.Tests
{
    [TestClass]
    public class BorrowingsControllerTest
    {
        Mock<IUnitOfWork> UOW_fake;

        [TestInitialize]
        public void Initialize()
        {
            UOW_fake = new Mock<IUnitOfWork>();

            var book = new Book
            {
                BookID = 1,
                BookName = "FirstBook",
                AuthorName = "FirstAuthor",
                TotalCopiesNumber = 10,
                AvailableCopiesNumber = 9,
                AvailableForBorrowing = true
            };
            var borrower = new Borrower
            {
                BorrowerID = 1,
                BorrowerName = "FirstBorrower"
            };
            var borrowings = new List<Borrowings>
            {
                new Borrowings{ book = book, borrower= borrower }
            };

            UOW_fake.Setup(uow => uow.Borrowings.List()).Returns(borrowings);
            UOW_fake.Setup(uow => uow.Books.FirstOrDefault(b => b.BookName.Contains("FirstBook"))).Returns(book);
            UOW_fake.Setup(uow => uow.Borrowers.FirstOrDefault(br => br.BorrowerName.Contains("FirstBorrower"))).Returns(borrower);

            UOW_fake.Setup(uow => uow.Borrowings.FirstOrDefault(br => br.book.BookName.Contains(book.BookName) && br.DeliverdBack == false && br.borrower.BorrowerName == borrower.BorrowerName)).Returns(borrowings[0]);
        }

        // BorrowBook_method test cases

        [TestMethod] // Test BorrowBook method Fail when book name dosen't match
        public void Check_BorrowBook_Method_if_Book_Not_Exist_Returns_NotFound()
        {
            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookID = 1,
                BookName = "booe", //Any Book Name That (NOT) in Setup method above
                AuthorName = "auth1",
                TotalCopiesNumber = 10,
                AvailableCopiesNumber = 8,
                AvailableForBorrowing = true
            };
            var borrower = new Borrower
            {
                BorrowerID = 1,
                BorrowerName = "borrower1"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower
            };
            ViewResult result = controller.BorrowBook(borrowings) as ViewResult;
            var modelState = controller.ModelState;

            var errorMessage = modelState[""].Errors[0].ErrorMessage;
            //Assert
            Assert.AreEqual("Book Not Found", errorMessage);
        }


        [TestMethod] // Test BorrowBook method Fail when book Not Available for borrowing 
        public void Check_BorrowBook_Method_if_Book_Not_Availbe_Now_Returns_BookNotAvailable()
        {
            // SET THE AvailableForBorrowing PROPERTY TO (FALSE) IN THE SETUP METHOD

            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookName = "FirstBook"
            };

            var borrower = new Borrower
            {
                BorrowerName = "borrower1"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower
            };

            ViewResult result = controller.BorrowBook(borrowings) as ViewResult;
            var modelState = controller.ModelState;
            var errorMessage = modelState[""].Errors[0].ErrorMessage;

            //Assert
            Assert.AreEqual("Book Not Available", errorMessage);
        }


        [TestMethod] // Test Success case of BorrowBook Method
        public void Check_BorrowBook_Method_if_Book_Exist_Redirects_to_Index()
        {
            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookID = 1,
                BookName = "FirstBook"
            };
            var borrower = new Borrower
            {
                BorrowerName = "FirstBorrower"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower
            };
            RedirectToRouteResult result = controller.BorrowBook(borrowings) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }


        // ReturnBook_method test cases

        [TestMethod] // Test ReturnBook method Fail when book name or borrower name (or Both) dosen't match a record
        public void Check_ReturnBook_Method_if_Book_OR_Borrower_Info_NOT_Accurate_Returns_NotAccurate()
        {
            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookID = 1,
                BookName = "wrongBookName"
            };
            var borrower = new Borrower
            {
                BorrowerName = "wrongBorrowerName"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower
            };
            ViewResult result = controller.ReturnBook(borrowings) as ViewResult;
            var modelState = controller.ModelState;

            var errorMessage = modelState[""].Errors[0].ErrorMessage;
            //Assert
            Assert.AreEqual("Not Accurate Info Book Name Or Borrower Name", errorMessage);
        }


        [TestMethod] // Test ReturnBook method Fail when book Not Borrowed or has been returned 
        public void Check_ReturnBook_Method_if_Book_Not_Borrowed_Returnes_BookNotBorrowed()
        {
            //This Could be for a wrong name (book/borrower) or because DeliveredBack is true
            //Just comment-up the last setup at Initialize method (if its not already commented)

            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookName = "FirstBook"
            };

            var borrower = new Borrower
            {
                BorrowerName = "FirstBorrower"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower
            };
            ViewResult result = controller.ReturnBook(borrowings) as ViewResult;
            var modelState = controller.ModelState;

            var errorMessage = modelState[""].Errors[0].ErrorMessage;
            //Assert
            Assert.AreEqual("Book Not Borrowed By This User Or has been Returned", errorMessage);
        }


        [TestMethod] // Test Success case of ReturnBook Method when operation done
        public void Check_ReturnBook_Method_if_Success_Redirects_to_Index()
        {
            // unComment last Setup at initialize method (if its commented)

            //Arrange
            BorrowingsController controller = new BorrowingsController(UOW_fake.Object);

            //Act
            var book = new Book
            {
                BookName = "FirstBook"
            };

            var borrower = new Borrower
            {
                BorrowerName = "FirstBorrower"
            };
            var borrowings = new Borrowings
            {
                book = book,
                borrower = borrower,
                DeliverdBack = false
            };

            RedirectToRouteResult result = controller.ReturnBook(borrowings) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
    }
}
