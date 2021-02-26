using System;
using System.IO;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace NotificationReflector
{
    class Program
    {
        static void Main(string[] args)
        {
            ShortCutCreator.TryCreateShortcut("ConsoleToast.App", "ConsoleToast");

            Console.WriteLine("Type 'exit' to quit. ENTER to show a notification");

            while (Console.ReadLine() != "exit")
            {
                // Toaster.ShowImageToast(
                //     "ConsoleToast.App",
                //     DateTime.Now.ToLongTimeString() + " title with image",
                //     "this is a message",
                //     Path.GetFullPath("plasticlogo.png"));

                /*ShowTextToast(
                    "ConsoleToast.App",
                    DateTime.Now.ToLongTimeString() + "title",
                    "this is a message");*/
            Toaster.ShowToast("ConsoleToast.App");
            }
            Toaster.clear();
        }
    }
}