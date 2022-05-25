using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MClient
{
    public class Client
    {
        private WebSocket ws;

        public class MessageArgs: EventArgs
        {
            public MessageArgs(string sender, string message)
            {
                this.sender = sender;
                this.message = message;
            }

            public string sender;
            public string message;
        }

        public delegate void MessageHandler(object sender, MessageArgs e);
        public event MessageHandler OnMessage;

        public Client(string url, string id)
        {
            this.ws = new WebSocket(url);
            this.ws.Connect();

            this.ws.OnMessage += (sender, e) =>
            {
                JObject data = JObject.Parse(e.Data);

                switch(data["type"].ToString())
                {
                    case "message":
                        {
                            MessageHandler handler = OnMessage;
                            handler?.Invoke(this, new MessageArgs(data["sender"].ToString(), data["message"].ToString()));
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            };

            ws.OnClose += (sender, e) => {
                Console.WriteLine("uhoh");
            };
            this.ws.Send("{ \"type\": \"identify\", \"id\": \"" + id + "\"}");
        }

        public void Send(string recipient, string message)
        {
            ws.Send("{\"recipient\": \"" + recipient + "\", \"message\": \"" + message + "\"}");
        }
    }
}
