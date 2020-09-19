using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class TestWord
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int WordId { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public Word Word { get; set; }
        public Test Test { get; set; }
    }
}
