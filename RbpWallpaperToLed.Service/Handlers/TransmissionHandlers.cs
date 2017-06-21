using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RbpWallpaperToLed.Core;
using System.Drawing;

namespace RbpWallpaperToLed.Service.Handlers
{
    public static class TransmissionHandlers
    {
        public static bool Send()
        {
            Sockets.ServerInit(IPAddress.Any);

            return true;
        }
    }
}
