using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class HttpDataParserHelper
    {
        private List<IHttpDataParser> _HttpDataParsers = new List<IHttpDataParser>();

        private static HttpDataParserHelper _HttpDataParserHelper = new HttpDataParserHelper();

        private HttpDataParserHelper() { }

        public static HttpDataParserHelper GetInstance()
        {
            return _HttpDataParserHelper;
        }

        public IHttpDataParser FindParser(string contenttype)
        {
            foreach (var item in _HttpDataParsers)
            {
                if (item.CanHandle(contenttype))
                    return item;
            }

            return null;
        }

        public void Register(IHttpDataParser parser)
        {
            _HttpDataParsers.Add(parser);
        }
    }
}
