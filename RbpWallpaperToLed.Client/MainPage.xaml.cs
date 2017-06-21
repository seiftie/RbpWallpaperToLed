using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using System.Xml.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RbpWallpaperToLed.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ChangeText_Click(object sender, RoutedEventArgs e)
        {
            var xmdock = CreateToast();
            var toast = new ToastNotification(xmdock);

            var notifi = ToastNotificationManager.CreateToastNotifier();
            notifi.Show(toast);
        }

        public static Windows.Data.Xml.Dom.XmlDocument CreateToast()
        {
            var xDoc = new XDocument(
                new XElement("toast", 
                    new XElement("visual", 
                        new XElement("binding", new XAttribute("template", "ToastGeneric"), 
                            new XElement("text", "C# Corner"), 
                            new XElement("text", "Do you got MVP award?")
                        )
                    ),
                
                    // actions
                    new XElement("actions",
                        new XElement("action", new XAttribute("activationType", "background"), new XAttribute("content", "Yes"), new XAttribute("arguments", "yes")),
                            new XElement("action", new XAttribute("activationType", "background"), new XAttribute("content", "No"), new XAttribute("arguments", "no"))
                        )
                    )
                );

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }
    }
}