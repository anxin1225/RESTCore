using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public interface IRESTService
    {
        HandleTunnel HandleTunnel { get; set; }

        void Start();

        void Stop();
    }
}
