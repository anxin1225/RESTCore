using System;
using RESTCore;

namespace RESTCoreConsle
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRESTService rest_service = new HttpRESTService
            {
                Port = 8099,
            };

            rest_service.Start();
        }
    }
}
