using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RbpWallpaperToLed.Service.Handlers
{
    public static class WindowsHandlers
    {
        public static void ScanWallpaper()
        {
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Desktop)
            {
                string userName = WindowsIdentity.GetCurrent().Name;
                string[] wallpapers = Directory.GetFiles(string.Format(@"C:\Users\{0}\AppData\Roaming\Microsoft\Windows\Themes\", userName));

                // TODO: check if this alternative works
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string[] alternativeWallpapers = Directory.GetFiles(string.Format(@"{0}\Roaming\Microsoft\Windows\Themes\", appData));

                // TODO: Test for multiple wallpapers
                foreach (var wallpaper in wallpapers)
                {
                    string path = Path.ChangeExtension(wallpaper, "jpeg");
                    ImageHandlers.Process(path);
                }
            }
        }
    }
}
