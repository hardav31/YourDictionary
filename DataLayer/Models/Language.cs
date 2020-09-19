using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISO { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<UserLanguage> UserLanguages { get; set; }
    }
}
