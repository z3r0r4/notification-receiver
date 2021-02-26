using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using System;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET; // QueryString.NET
// using  Windows.ApplicationModel.Activation;

namespace NotificationReflector
{
    class Toaster{
        static ToastNotificationHistory history;
        public static void clear(){
            ToastNotificationManager.History.Remove("T2", "G1");//doesnt work anywhere for unknown reason
        }
        public static void ShowToast(string appId){
            history = ToastNotificationManager.History;
            // Construct the visuals of the toast (using Notifications library)
            ToastContent toastContent = new ToastContentBuilder()
             // Arguments returned when user taps body of notification
                .AddToastActivationInfo(new QueryString() // Using QueryString.NET
                {
                    { "action", "viewConversation" },
                    { "conversationId", "conversationId.ToString()" }
                }.ToString(), ToastActivationType.Foreground)

                // .AddToastActivationInfo("action=viewConversation&conversationId=5", ToastActivationType.Foreground)
                .AddText("Hello world!")
                .AddInputTextBox("tbReply", "Type a reply")
                .AddButton("Reply", ToastActivationType.Background, "action=reply&threadId=9218")
                .AddButton("See more details", ToastActivationType.Foreground, "action=viewdetails&contentId=351")
                .AddButton("Remind me later", ToastActivationType.Background, "action=remindlater&contentId=351")
                .AddCustomTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc))//deosnt work
                .AddAttributionText("Via SMS")
                .GetToastContent();

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());

            ToastEvents events = new ToastEvents();

            toast.Activated += events.ToastActivated;
            toast.Dismissed += events.ToastDismissed;
            toast.Failed += events.ToastFailed;

            toast.Tag = "T2";
            toast.Group = "G1";
            // toast.SuppressPopup = true;

            // And then show it
            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }

        public static void ShowTextToast(string appId, string title, string message)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
                ToastTemplateType.ToastText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(message));

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            ToastEvents events = new ToastEvents();

            toast.Activated += events.ToastActivated;
            toast.Dismissed += events.ToastDismissed;
            toast.Failed += events.ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId
            // on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }

        public static void ShowImageToast(string appId, string title, string message, string image)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
                ToastTemplateType.ToastImageAndText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(message));

            // Specify the absolute path to an image
            String imagePath = "file:///" + image;
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            ToastEvents events = new ToastEvents();

            toast.Activated += events.ToastActivated;
            toast.Dismissed += events.ToastDismissed;
            toast.Failed += events.ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId
            // on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }

        private class ToastEvents
        {
            internal void ToastActivated(ToastNotification sender, object e)
            {
                Console.WriteLine(e.GetType());
                // Handle toast activation
                if (e is Windows.UI.Notifications.ToastActivatedEventArgs toastActivationArgs)
                {
                    Console.WriteLine("Got ToastActivationargs");
                    // Parse the query string (using QueryString.NET)
                    QueryString args = QueryString.Parse(toastActivationArgs.Arguments);

                    // See what action is being requested 
                    switch (args["action"]) 
                    {
                        case "reply": 
                            Console.WriteLine("User activated the toast with reply");
                            break;
                                
                    }
                }
                Console.WriteLine("User activated the toast");
            }

            internal void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
            {
                String outputText = "";
                switch (e.Reason)
                {
                    case ToastDismissalReason.ApplicationHidden:
                        outputText = "The app hid the toast using ToastNotifier.Hide";
                        break;
                    case ToastDismissalReason.UserCanceled:
                        outputText = "The user dismissed the toast";
                        break;
                    case ToastDismissalReason.TimedOut:
                        outputText = "The toast has timed out";
                        break;
                }

                Console.WriteLine(outputText);
            }


            internal void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
            {
                Console.WriteLine("The toast encountered an error.");
            }
        }


    }
}