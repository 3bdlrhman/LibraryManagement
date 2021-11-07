using System.Net;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Models.UnitOfWork;

namespace LibraryManagement.Controllers
{
    public class BorrowingsController : Controller
    {
        IUnitOfWork _uow;
        public BorrowingsController()
        {
            _uow = new UnitOfWork();
        }
        public BorrowingsController(IUnitOfWork uow)
        {
            _uow = uow;
        }
     
        // GET: Borrowings
        public ActionResult Index()
        {
            return View(_uow.Borrowings.List());
        }

        // GET: Borrowings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrowings borrowings = _uow.Borrowings.Find(id.Value);
            if (borrowings == null)
            {
                return HttpNotFound();
            }
            return View(borrowings);
        }

        // GET: Borrowings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrowings/Create
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public ActionResult BorrowBook(Borrowings borrowings)
        {
            var bookName = borrowings.book.BookName;
            var book = _uow.Books.FirstOrDefault(b => b.BookName.Contains(bookName));
            if (book == null)
            {
                ModelState.AddModelError("", "Book Not Found");
                return View(borrowings);
            }

            var borrowerName = borrowings.borrower.BorrowerName;
            var borrower = _uow.Borrowers.FirstOrDefault(br => br.BorrowerName.Contains(borrowerName));
            if (borrower == null)
            {
                var newBorrower = new Borrower { BorrowerName = borrowerName };
                _uow.Borrowers.Add(newBorrower);
                borrowings.borrower = newBorrower;
            }
            else
            {
                borrowings.borrower = borrower;
            }

            if (!book.AvailableForBorrowing || book.AvailableCopiesNumber == 0)
            {
                ModelState.AddModelError("", "Book Not Available");
                return View(borrowings);
            }
            book.AvailableCopiesNumber -= 1;
            if (book.AvailableCopiesNumber == 0)
                book.AvailableForBorrowing = false;

            borrowings.book = book;
            _uow.Borrowings.Add(borrowings);
            _uow.SaveAll();

            return RedirectToAction("Index");
        }

        // GET: Borrowings/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Borrowings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public ActionResult ReturnBook(Borrowings borrowings)
        {
            var bookName = borrowings.book.BookName;
            var book = _uow.Books.FirstOrDefault(b => b.BookName.Contains(bookName));

            var borrowerName = borrowings.borrower.BorrowerName;
            var borrower = _uow.Borrowers.FirstOrDefault(bor => bor.BorrowerName.Contains(borrowerName));

            if (book == null || borrower == null)
            {
                ModelState.AddModelError("", "Not Accurate Info Book Name Or Borrower Name");
                return View(borrowings);
            }

            var borrowingRecord = _uow.Borrowings.FirstOrDefault(
                br => br.book.BookName.Contains(book.BookName)
                && br.DeliverdBack == false
                && br.borrower.BorrowerName == borrower.BorrowerName);

            if (borrowingRecord == null)
            {
                ModelState.AddModelError("", "Book Not Borrowed By This User Or has been Returned");
                return View(borrowings);
            }

            borrowingRecord.DeliverdBack = true;

            book.AvailableCopiesNumber += 1;
            book.AvailableForBorrowing = true;

            _uow.SaveAll();
            return RedirectToAction("Index");
        }

        // GET: Borrowings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrowings borrowings = _uow.Borrowings.Find(id.Value);
            if (borrowings == null)
            {
                return HttpNotFound();
            }
            return View(borrowings);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrowings borrowings = _uow.Borrowings.Find(id);
            if (borrowings == null)
                return HttpNotFound();
            _uow.Borrowings.Delete(borrowings.BorrowingID);
            _uow.SaveAll();
            return RedirectToAction("Index");
        }
    }
}
