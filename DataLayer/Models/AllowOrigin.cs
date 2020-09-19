using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class AllowOrigin
    {
        public int Id { get; set; }
        public int AllowedClientAppId { get; set; }
        public int AllowedOrigin { get; set; }
        public AllowedClientApp AllowedClientApp { get; set; }
    }
}
