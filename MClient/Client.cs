using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;


namespace MClient
{
    public class MClient
    {
        private WebSocket ws;

        public MClient(string url, string id)
        {
            ws = new WebSocket(url);
            ws.Connect();

            ws.Send("{ \"type\": \"identifier\", \"id\": \"" + id + "\"}");
        }
    }
}
