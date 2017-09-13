using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicTexter.Models.Entities
{
    public class Language
    {
        public Language()
        {
            TextItems = new List<TextItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }


        public virtual ICollection<TextItem> TextItems { get; set; }
    }
}