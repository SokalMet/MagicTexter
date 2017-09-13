using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MagicTexter.DAL;
using MagicTexter.Models.Entities;

namespace MagicTexter.Controllers
{
    public class TextItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TextItems
        public ActionResult Index()
        {
            var textItems = db.TextItems.Include(t => t.Language);
            return View(textItems.ToList());
        }

        // GET: TextItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            return View(textItem);
        }

        // GET: TextItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: TextItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,ItemId,LanguageId")] TextItem textItem)
        {
            if (ModelState.IsValid)
            {
                db.TextItems.Add(textItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", textItem.LanguageId);
            return View(textItem);
        }

        // GET: TextItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", textItem.LanguageId);
            return View(textItem);
        }

        // POST: TextItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,ItemId,LanguageId")] TextItem textItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(textItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", textItem.LanguageId);
            return View(textItem);
        }

        // GET: TextItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            return View(textItem);
        }

        // POST: TextItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TextItem textItem = db.TextItems.Find(id);
            db.TextItems.Remove(textItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
