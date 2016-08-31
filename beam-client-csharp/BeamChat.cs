using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using beam_client_csharp.EventHandlers;
using beam_client_csharp.Messages;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace beam_client_csharp
{
    public class BeamChat
    {
        private static WebSocket _websocket;

        public BeamChat()
        {
            _dicReplyFunctions = new Dictionary<int, ReceiveReply>();
        }

        public void SetupWebsocket(string webSocketUrl)
        {
            _websocket = new WebSocket(webSocketUrl);
            _websocket.Opened += websocket_Opened;
            _websocket.Error += websocket_Error;
            _websocket.Closed += websocket_Closed;
            _websocket.MessageReceived += websocket_MessageReceived;
        }

        public void Connect()
        {
            _websocket.Open();
        }

        private void websocket_Opened(object sender, EventArgs e)
        {
            //websocket.Send("Hello World!");
        }
        private void websocket_Closed(object sender, EventArgs e)
        {
            //websocket.Send("Hello World!");
        }
        private void websocket_Error(object sender, ErrorEventArgs e)
        {
            //websocket.Send("Hello World!");
        }

        public void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Received: " + e.Message);
            ProcessMessage(e.Message);
        }

        private void ProcessMessage(string message)
        {
            BeamMessage beamMessage = JsonConvert.DeserializeObject<BeamMessage>(message);

            switch (beamMessage.type)
            {
                case "event":
                    BeamEventMessage mEvent = JsonConvert.DeserializeObject<BeamEventMessage>(message);
                    BeamEventHandler.HandleEvent(mEvent);
                    break;
                case "reply":
                    BeamReplyMessage mReply = JsonConvert.DeserializeObject<BeamReplyMessage>(message);
                    if (_dicReplyFunctions.ContainsKey(beamMessage.id)) // Relay message to sender class
                    {
                        _dicReplyFunctions[beamMessage.id](mReply);
                    }
                    break;
                case "method":
                    BeamMethodMessage mMethod = JsonConvert.DeserializeObject<BeamMethodMessage>(message);
                    BeamMethodHandler.HandleMethod(mMethod);
                    break;
                default:
                    throw new NotImplementedException(beamMessage.type);
            }
        }

        public delegate void ReceiveReply(BeamReplyMessage message);

        private static Dictionary<int, ReceiveReply> _dicReplyFunctions;
        public static void SendBeamMessage(BeamMessage message, ReceiveReply replyFunc)
        {
            _dicReplyFunctions.Add(message.id, replyFunc);

            string jsonMessage = JsonConvert.SerializeObject(message);

            _websocket.Send(jsonMessage);
        }

        public void SetupCredentials(string authKey)
        {
            throw new NotImplementedException();
        }

        public void SetupCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
