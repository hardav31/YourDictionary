using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int State { get; set; }
        public int AllowedFailsCount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastPassDate { get; set; }
        public int PassCount { get; set; }
        public int SuccessCount { get; set; }

        public User User { get; set; }
        public ICollection<TestWord> TestWords { get; set; }
    }
}
