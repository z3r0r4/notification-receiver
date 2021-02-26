using System;
using System.Net.Sockets;

using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
/*
At first you need to declare that your program will be using winRT libraries:
1. Right click on your yourProject, select Unload Project
2. Right click on your youProject(unavailable) and click Edit yourProject.csproj
3. Add a new property group:<TargetPlatformVersion>8.0</TargetPlatformVersion>
4. Reload project
5. Add referece Windows from Windows > Core
*/
using System;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace NotifiactionReflector
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //TODO http://blog.plasticscm.com/2016/08/how-to-send-windows-toast-notifications.html this describes how to send notifications from console apps
             // Register AUMID and COM server (for MSIX/sparse package apps, this no-ops)
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<MyNotificationActivator>("r4.notification-reflector");
            // Register COM server and activator type
            DesktopNotificationManagerCompat.RegisterActivator<MyNotificationActivator>();

            // Construct the visuals of the toast (using Notifications library)
            ToastContent toastContent = new ToastContentBuilder()
            .AddToastActivationInfo("action=viewConversation&conversationId=5", ToastActivationType.Foreground)
            .AddText("Hello world!")
            .GetToastContent();

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());

            // And then show it
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);

            Console.WriteLine("created Toast");



            // receive();
        }

        private static void receive()
        {
            TcpListener server = new TcpListener(System.Net.IPAddress.Parse("192.168.178.84"), 9003);
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting");
                TcpClient client = server.AcceptTcpClient();

                NetworkStream ns = client.GetStream();

                while (client.Connected)//not needed?
                {
                    byte[] msg = new byte[1024];
                    ns.Read(msg, 0, msg.Length);
                    String jsonMessage = System.Text.Encoding.Default.GetString(msg);

                    Console.Write(jsonMessage);
                    try
                    {
                        MirrorNotification mn = new MirrorNotification(jsonMessage);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("didnt extract");
                        break;
                    }
                }
            }
        }
    }
     public class NewToastNotification
    {
        public NewToastNotification(string input, int type)
        {
            string NotificationTextThing = input;
            string Toast = "";
            switch (type)
            {
                case 1:
                    {
                        //Basic Toast
                        Toast = "<toast><visual><binding template=\"ToastImageAndText01\"><text id = \"1\" >";
                        Toast += NotificationTextThing;
                        Toast += "</text></binding></visual></toast>";
                        break;
                    }
                default:
                    {
                        Toast = "<toast><visual><binding template=\"ToastImageAndText01\"><text id = \"1\" >";
                        Toast += "Default Text String";
                        Toast += "</text></binding></visual></toast>";
                        break;
                    }
            }
            XmlDocument tileXml = new XmlDocument();
            tileXml.LoadXml(Toast);
            var toast = new ToastNotification(tileXml);
            
            ToastNotificationManager.CreateToastNotifier("New Toast Thing").Show(toast);
        }
}
}
