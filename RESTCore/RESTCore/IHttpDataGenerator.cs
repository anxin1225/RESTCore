using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public interface IHttpDataGenerator
    {
        bool CanHandle(string contenttype);

        string Generator(object obj);
    }
}
