using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class HttpDataGeneratorHelper
    {
        private List<IHttpDataGenerator> _HttpDataGenerators = new List<IHttpDataGenerator>();
        private static HttpDataGeneratorHelper _HttpDataGeneratorHelper = new HttpDataGeneratorHelper();

        private HttpDataGeneratorHelper() { }

        public static HttpDataGeneratorHelper GetInstance() { return _HttpDataGeneratorHelper; }

        public IHttpDataGenerator FindGenerator(string contenttype)
        {
            foreach (var item in _HttpDataGenerators)
            {
                if (item.CanHandle(contenttype))
                    return item;
            }

            return null;
        }

        public void Register(IHttpDataGenerator generator)
        {
            _HttpDataGenerators.Add(generator);
        }

        internal IHttpDataGenerator DefaultGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
