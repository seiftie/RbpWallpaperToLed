using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RbpWallpaperToLed.Service.Handlers;

namespace RbpWallpaperToLed.Service
{
    public class WallpaperToLedService : ServiceBase
    {
        Thread wallpaperDetectThread;
        bool isRunning = false;

        public WallpaperToLedService()
        {
            this.ServiceName = "WallpaperChangeDetectingService";
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        static void Main()
        {
            ServiceBase.Run(new WallpaperToLedService());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            wallpaperDetectThread = new Thread(WallpaperThread);
            wallpaperDetectThread.Start();
            isRunning = true;
        }

        private void WallpaperThread()
        {
            while (isRunning)
            {
                WindowsHandlers.ScanWallpaper();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
        protected override void OnPause()
        {
            base.OnPause();
        }
        protected override void OnContinue()
        {
            base.OnContinue();
        }
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
        }
        /// <summary>
        /// OnSessionChange(): To handle a change event
        ///   from a Terminal Server session.
        ///   Useful if you need to determine
        ///   when a user logs in remotely or logs off,
        ///   or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription">The Session Change
        /// Event that occured.</param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }
    }
}
