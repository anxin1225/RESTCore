using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class SourceItem<T> : SourceData
    {
        public T Item { get; set; }
    }
}
