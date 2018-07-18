using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class SourceList<T> : SourceData
    {
        public List<T> Items { get; set; }
    }
}
