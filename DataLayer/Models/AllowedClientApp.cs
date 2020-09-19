using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class AllowedClientApp
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public int ApplicationTypes { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public int ApiType { get; set; }
        public ICollection<AllowOrigin> AllowOrigins { get; set; }
    }
}
