using MagicTexter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicTexter.Models.Entities;
using System.Data.Entity;
using System.Net;

namespace MagicTexter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            IEnumerable<Template> templates = db.Templates;            
            IEnumerable<Language> languages = db.Languages;
            var model = new Home(templates, languages);
                return View(model);            
        }

        public PartialViewResult GetIndexes(int templateId, int languageId)
        {
            List<int> itemsIds = db.ItemMappings.Where(x => x.TemplateId == templateId).Select(x => x.ItemId).ToList();
            IEnumerable<Item> items = db.Items.Where(x=>itemsIds.Contains(x.Id) == true);
            foreach (var item in items)
            {
                foreach (var textItem in item.TextItems.Where(x=>x.Text != null))
                {
                    string textForRemove = "";
                    string textForEdit = "";
                    var text = textItem.Text.ToString();
                    if ( text.IndexOf('[') >= 0)
                    {
                        int indexOfStart = text.IndexOf('[');
                        int indexOfEnd = text.IndexOf(']');
                        for (int i = indexOfStart; i <= indexOfEnd; i++)
                        {
                            textForRemove += text[i].ToString();
                        }
                        for (int i = indexOfStart+1; i < indexOfEnd; i++)
                        {
                            textForEdit += text[i].ToString();
                        }
                        var elementsArrayFromText = textForEdit.Split(';').ToArray();
                        int arrayLangth = elementsArrayFromText.Length;
                        Random rnd = new Random();
                        textForEdit = elementsArrayFromText[rnd.Next(0, arrayLangth)];
                        text = text.Replace(textForRemove, textForEdit);
                        text.Insert(indexOfStart, textForEdit);
                        textItem.Text = text;
                    }
                }
            }
            IEnumerable<ItemMapping> itemMappings = db.ItemMappings.Where(x => x.TemplateId == templateId);
            Language language = db.Languages.FirstOrDefault(x => x.Id == languageId);
            return PartialView("_PartialItemsPage", new PartialIndexesModel(items, itemMappings, language));
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}