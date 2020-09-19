using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WordId { get; set; }
        public string LanguagId { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Word Word { get; set; }
        public Language Language { get; set; }
    }
}
