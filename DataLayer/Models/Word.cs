using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Level { get; set; }
        public User User { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
