using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicTexter.Models.Entities
{
    public class Template
    {
        public Template()
        {
            ItemMappings = new List<ItemMapping>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> SelectedItemsIds { get; set; } = new List<int> { };
        public IEnumerable<Item> Items { get; set; }

        public virtual ICollection<ItemMapping> ItemMappings { get; set; }
    }

    public class CreateTemplate : Template
    {
        public CreateTemplate() { }
        public IEnumerable<int> ItemsIds { get; set; } = new List<int> { };
        public CreateTemplate(IEnumerable<Item> items)
        {
            Items = items.ToList();
        }
    }
    public class EditTemplate : Template
    {
        public EditTemplate() { }
        public IEnumerable<int> ItemsIds { get; set; }
        public List<string> PreselectedItemsNames { get; set; } = new List<string>{};
        public EditTemplate(Template template)
        {
            Id = template.Id;
            Name = template.Name;
            Description = template.Description;
            ItemsIds = template.ItemMappings.Select(x => x.ItemId);            
            Items = template.Items.ToList();
            SelectedItemsIds = template.SelectedItemsIds;
            foreach (var item in SelectedItemsIds)
            {
                PreselectedItemsNames.Add(Items.FirstOrDefault(x => x.Id == item).Name);
            }           
        }
    }
}