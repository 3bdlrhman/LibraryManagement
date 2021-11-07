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
    public class BorrowersController : Controller
    {
        IUnitOfWork _uow;
        public BorrowersController()
        {
            _uow = new UnitOfWork();
        }
        public BorrowersController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Borrowers
        public ActionResult Index()
        {
            return View(_uow.Borrowers.List());
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowerID,BorrowerName")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                _uow.Borrowers.Add(borrower);
                _uow.SaveAll();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = _uow.Borrowers.Find(id.Value);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowerID,BorrowerName")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                _uow.Borrowers.Edit(borrower);
                _uow.SaveAll();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = _uow.Borrowers.Find(id.Value);
            if (borrower == null)
                return HttpNotFound("This User Has Not Been Added To The System");
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = _uow.Borrowers.Find(id);
            
            _uow.Borrowers.Delete(borrower.BorrowerID);
            _uow.SaveAll();
            return RedirectToAction("Index");
        }
    }
}
