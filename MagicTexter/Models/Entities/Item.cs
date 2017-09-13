using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicTexter.Models.Entities
{
    public class Item
    {
        public Item()
        {
            ItemMappings = new List<ItemMapping>();
            TextItems = new List<TextItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasValue { get; set; } = false;
        public IEnumerable<Language> Languages { get; set; }

        public virtual ICollection<TextItem> TextItems { get; set; }
        public virtual ICollection<ItemMapping> ItemMappings { get; set; }

        public SelectListItem ToSelectListItem(bool selected = false)
        {
            return new SelectListItem()
            {
                Text = Name,
                Value = Id.ToString(),
                Selected = selected
            };
        }
    }

    public class CreateItem
    {
        public CreateItem(IEnumerable<Language> languages)
        {
            Item item = new Item();
            foreach (var langItem in languages)
            {
                TextItem textItem = new TextItem();
                textItem.LanguageId = langItem.Id;
                textItem.ItemId = item.Id;
                textItem.Language = langItem;
                TextItems.Add(textItem);
            }
            Languages = languages;
        }
        public string Name { get; set; }
        public bool HasValue { get; set; } = false;
        public List<TextItem> TextItems { get; set; } = new List<TextItem> { };
        public IEnumerable<Language> Languages { get; set; }

    }
}