using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Service.Common
{
    public class YourDictionaryDbContext : YourDictionaryDataEntities
    {
        public YourDictionaryDbContext(DbContextOptions<YourDictionaryDataEntities> options) : base(options)
        {

        }
    }
}
