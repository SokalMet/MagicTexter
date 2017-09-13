using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicTexter.Models.Entities
{
    public class ItemMapping
    {
        public int Id { get; set; }
        public int Index { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }
    }
}