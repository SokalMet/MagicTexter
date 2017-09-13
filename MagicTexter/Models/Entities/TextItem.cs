using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicTexter.Models.Entities
{
    public class TextItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}