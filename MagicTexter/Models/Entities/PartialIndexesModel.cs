using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicTexter.Models.Entities
{
    public class PartialIndexesModel
    {
        public int Id { get; set; }
        public SelectList LanguageList { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<ItemMapping> ItemMappings { get; set; }
        public Language Language { get; set; }
        public Language ItemsLanguage { get; set; }
        public PartialIndexesModel(IEnumerable<Item> items, IEnumerable<ItemMapping> itemMappings, Language language)
        {
            Items = items;
            ItemMappings = itemMappings;
            Language = language;
        }
    }
}