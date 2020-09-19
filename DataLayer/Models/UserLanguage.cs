using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class UserLanguage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LanguageId { get; set; }
        public bool IsDefault { get; set; }
        public User User { get; set; }
        public Language Language { get; set; }
    }
}
