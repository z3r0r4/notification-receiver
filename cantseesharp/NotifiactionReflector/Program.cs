﻿using System;

using System.Net;      //required
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace NotifiactionReflector
{
    class Program
    {
        static void Main(string[] args)
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
    class MirrorNotification
    {
        public String id;
        // public String tag;
        public String key;
        public String appName;
        public String title;
        public String text;
        public String ticker;
        public String time;
        public bool isCancel;
        public bool isReplyable;
        public bool isActionable;
        public MirrorNotification(String jsonMessage)
        {
            Console.WriteLine("\nExtracting new MirrorNotification");
            try
            {
                JObject json = JObject.Parse(jsonMessage);

                this.id = json.GetValue("id").ToString();
                this.key = json.GetValue("key").ToString();
                this.appName = json.GetValue("appName").ToString();
                this.title = json.GetValue("title").ToString();
                this.text = json.GetValue("text").ToString();
                this.ticker = json.GetValue("ticker").ToString();
                this.time = json.GetValue("time").ToString();
                this.isCancel = Convert.ToBoolean(json.GetValue("isCancel").ToString());
                this.isReplyable = Convert.ToBoolean(json.GetValue("isReplyable").ToString());
                this.isActionable = Convert.ToBoolean(json.GetValue("isActionable").ToString());

            }
            catch (Newtonsoft.Json.JsonReaderException j)
            {
                Console.WriteLine("couldnt extract. not a jsonstring?");
                // return;
                throw (new Exception("not a proper json string"));
            }

            log();
            Console.WriteLine("\nFinished extraction");
        }
        public void log()
        {
            Console.Write("MirrorNotification:"
                    + "\nID     :" + this.id
                    + "\nkey    :" + this.key
                    + "\nappName:" + this.appName
                    + "\ntime   :" + this.time
                    + "\ntitle  :" + this.title
                    + "\ntext   :" + this.text
                    + "\nticker :" + this.ticker
                    + "\nactions:" + this.isActionable
                    + "\nrepAct :" + this.isReplyable);
        }
    }
}