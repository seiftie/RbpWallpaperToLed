using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RbpWallpaperToLed.Service
{
    public class WallpaperToLedServiceInstaller : Installer
    {
        public WallpaperToLedServiceInstaller()
        {
            ServiceProcessInstaller spi = new ServiceProcessInstaller();
            ServiceInstaller si = new ServiceInstaller();

            spi.Account = ServiceAccount.LocalSystem;
            spi.Username = null;
            spi.Password = null;

            si.DisplayName = "Wallpaper Change Detecting Service";
            si.StartType = ServiceStartMode.Automatic;

            si.ServiceName = "WallpaperChangeDetectingService";

            this.Installers.Add(spi);
            this.Installers.Add(si);
        }
    }
}
