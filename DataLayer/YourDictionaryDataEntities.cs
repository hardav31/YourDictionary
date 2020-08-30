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

        public virtual DbSet<User> Users { get; set; }
    }
}
