using System;
using RESTCore;
using MovieShopREST;

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

            RouteOption<BaseRESTSource> source_option = new RouteOption<BaseRESTSource>()
                .AddRouteFormatKey("/api/{Source}")
                .Register(new TagsSource());

            rest_service.HandleTunnel = new HandleTunnelOption()
                .AddHandleTunnel(new ErrorHandleTunnel())
                .AddHandleTunnel(new RouteHandleTunnel
                {
                    Route = source_option.GetRoute()
                })
                .GetHandleTunnel();

            rest_service.Start();
        }
    }
}
