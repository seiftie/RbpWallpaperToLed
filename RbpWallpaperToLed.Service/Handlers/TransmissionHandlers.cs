using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace RbpWallpaperToLed.Service.Handlers
{
    public static class TransmissionHandlers
    {
        public static bool Send()
        {
            Socket sListener;
            SocketPermission permission = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
            IPHostEntry ipHost = Dns.GetHostEntry("");
            IPAddress ip = ipHost.AddressList[0];
            
            return true;
        }
    }
}
