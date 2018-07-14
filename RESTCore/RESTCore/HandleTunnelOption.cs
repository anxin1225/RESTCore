using System;
using System.Collections.Generic;
using System.Text;

namespace RESTCore
{
    public class HandleTunnelOption
    {
        public List<HandleTunnel> _HandleTunnel = new List<HandleTunnel>();

        public HandleTunnelOption AddHandleTunnel(HandleTunnel tunnel)
        {
            _HandleTunnel.Add(tunnel);

            return this;
        }

        public HandleTunnel GetHandleTunnel()
        {
            if (_HandleTunnel.Count == 0)
                return null;

            for (int i = 0; i < _HandleTunnel.Count; i++)
            {
                var current = _HandleTunnel[i];
                var next = i + 1 < _HandleTunnel.Count ? _HandleTunnel[i + i] : null;

                current.ChildTunnel = next;
                next.ChildTunnel = null;
            }

            return _HandleTunnel[0];
        }
    }
}
