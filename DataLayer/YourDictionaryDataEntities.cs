using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class YourDictionaryDataEntities : DbContext
    {
        public YourDictionaryDataEntities(DbContextOptions<YourDictionaryDataEntities> options) : base(options)
        {

        }
        public virtual DbSet<AllowedClientApp> AllowedClientApps { get; set; }
        public virtual DbSet<AllowOrigin> AllowOrigins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLanguage> UserLanguages { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestWord> TestWords { get; set; }
        public virtual DbSet<Word> Words { get; set; }
    }
}
