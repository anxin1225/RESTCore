using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class SourcePage<T> : SourceData
    {
        public int TotalPage { get; set; }

        public int CurrentPage { get; set; }

        public int TatalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
