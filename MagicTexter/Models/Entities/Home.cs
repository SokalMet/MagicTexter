using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicTexter.Models.Entities
{
    public class Home
    {
        public int Id { get; set; }
        public Template template { get; set; }
        public SelectList TemplateList { get; set; }
        public SelectList LanguageList { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<ItemMapping> ItemMappings { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public string HomeName { get; set; }

        public Home(IEnumerable<Template> templates, IEnumerable<Language> languages)
        {
            TemplateList = new SelectList(templates.Select(x => x.Name));
            LanguageList = new SelectList(languages.Select(x => x.Name));            
            Languages = languages;
            Templates = templates;
        }
        
    }
}