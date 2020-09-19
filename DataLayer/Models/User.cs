using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int State { get; set; }
        public bool IsEmailVerfied { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public ICollection<UserLanguage> UserLanguages { get; set; }
        public ICollection<Word> Words { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
