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
    public class TemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Templates
        public ActionResult Index()
        {
            return View(db.Templates.ToList());
        }

        // GET: Templates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        public ActionResult Create()
        {
            IEnumerable<Item> items = db.Items;
            return View(new CreateTemplate(items));
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Template template)
        {
            if (ModelState.IsValid)
            {
                List<ItemMapping> itemMappings = new List<ItemMapping> { };
                template.Items = db.Items.ToList();
                for (int i = 0; i < template.SelectedItemsIds.Count(); i++)
                {
                    ItemMapping itemMapping = new ItemMapping { ItemId = template.SelectedItemsIds.ToList()[i], Index = i, TemplateId = template.Id };
                    itemMappings.Add(itemMapping);
                    db.ItemMappings.Add(itemMapping);
                }
                template.ItemMappings = db.ItemMappings.Where(x => x.TemplateId == template.Id).ToList();
                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(template);
        }

        // GET: Templates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            template.Items = db.Items.ToList();
            template.ItemMappings = db.ItemMappings.Where(x => x.TemplateId == template.Id).ToList();
            template.SelectedItemsIds = db.ItemMappings.Where(x => x.TemplateId == template.Id).Select(x => x.ItemId).ToList();
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(new EditTemplate(template));
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Template template)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                List<ItemMapping> itemMappingsFromDb = db.ItemMappings.Where(x => x.TemplateId == template.Id).ToList();


                foreach (var itemId in template.SelectedItemsIds)
                {
                    ItemMapping itemMapping = new ItemMapping { Index = i++, TemplateId = template.Id, ItemId = itemId };
                    if (db.ItemMappings.Any(x => x.TemplateId == template.Id))
                    {
                        if (itemMappingsFromDb.Any(x => x.Index == itemMapping.Index))
                        {
                            var ItemMapId = itemMappingsFromDb.FirstOrDefault(x => x.Index == itemMapping.Index).Id;
                            db.ItemMappings.Find(ItemMapId).ItemId = itemMapping.ItemId;
                        }
                        else
                        {
                            db.ItemMappings.Add(itemMapping);
                        }
                    }
                    else
                    {
                        db.ItemMappings.Add(itemMapping);
                    }
                }

                if (db.ItemMappings.Any(x => x.TemplateId == template.Id && x.Index > (template.SelectedItemsIds.Count()) - 1))
                {
                    do
                    {
                        ItemMapping im = db.ItemMappings.FirstOrDefault(x => x.TemplateId == template.Id && x.Index > (template.SelectedItemsIds.Count()) - 1);
                        if (im != null)
                        {
                            db.ItemMappings.Remove(im);
                            db.SaveChanges();
                        }
                        else { break; }
                    } while (db.ItemMappings.Any(x => x.Index > (template.SelectedItemsIds.Count() - 1)));
                }
                db.Entry(template).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates.Find(id);
            db.Templates.Remove(template);
            if (db.ItemMappings.Any(x=>x.Id == id))
            {
                foreach (var item in db.ItemMappings.Where(x=>x.Id == id))
                {
                    db.ItemMappings.Remove(item);
                }
            }
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
