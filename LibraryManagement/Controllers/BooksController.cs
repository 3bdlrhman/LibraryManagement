using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Models.UnitOfWork;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private IUnitOfWork _uow;
        public BooksController()
        {
            _uow = new UnitOfWork();
        }
        public BooksController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View(_uow.Books.List());
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookName,AuthorName,TotalCopiesNumber,AvailableCopiesNumber,AvailableForBorrowing")] Book book)
        {
            if (ModelState.IsValid)
            {
                if(book.AvailableCopiesNumber > book.TotalCopiesNumber)
                {
                    ModelState.AddModelError("", "Available Copies Must be Less than Total copies");
                    return View(book);
                }

                book.AvailableForBorrowing = book.AvailableCopiesNumber > 0 ? true : false;

                _uow.Books.Add(book);
                _uow.SaveAll();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _uow.Books.Find(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookName,AuthorName,TotalCopiesNumber,AvailableCopiesNumber,AvailableForBorrowing")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.AvailableCopiesNumber > book.TotalCopiesNumber)
                {
                    ModelState.AddModelError("", "Available Copies Must be Less than total copies");
                    return View(book);
                }
                
                book.AvailableForBorrowing = book.AvailableCopiesNumber == 0 ? false : true;
                _uow.Books.Edit(book);
                _uow.SaveAll();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _uow.Books.Find(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _uow.Books.Find(id);
            if(book == null)
            {
                ModelState.AddModelError("", "Book Not Found");
                return View(book);
            }
            _uow.Books.Delete(book.BookID);
            _uow.SaveAll();
            return RedirectToAction("Index");
        }
    }
}