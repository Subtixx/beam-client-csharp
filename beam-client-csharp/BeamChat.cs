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
    /// <summary>
    /// Class to communicate with Beams Chat
    /// </summary>
    public class BeamChat
    {
        private static WebSocket _websocket;

        public BeamChat()
        {
            _dicReplyFunctions = new Dictionary<int, ReceiveReply>();
        }

        /// <summary>
        /// Setups the websocket and readies it to connect.
        /// </summary>
        /// <param name="webSocketUrl">The web socket URL.</param>
        public void SetupWebsocket(string webSocketUrl)
        {
            _websocket = new WebSocket(webSocketUrl);
            _websocket.Opened += websocket_Opened;
            _websocket.Error += websocket_Error;
            _websocket.Closed += websocket_Closed;
            _websocket.MessageReceived += websocket_MessageReceived;
        }

        /// <summary>
        /// Connects the websocket to the server.
        /// </summary>
        public void Connect()
        {
            _websocket.Open();
        }

        /// <summary>
        /// Handles the Opened event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void websocket_Opened(object sender, EventArgs e)
        {
            //websocket.Send("Hello World!");
        }

        /// <summary>
        /// Handles the Closed event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void websocket_Closed(object sender, EventArgs e)
        {
            //websocket.Send("Hello World!");
        }

        /// <summary>
        /// Handles the Error event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ErrorEventArgs"/> instance containing the event data.</param>
        private void websocket_Error(object sender, ErrorEventArgs e)
        {
            //websocket.Send("Hello World!");
        }

        /// <summary>
        /// Handles the MessageReceived event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MessageReceivedEventArgs"/> instance containing the event data.</param>
        public void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Received: " + e.Message);
            ProcessMessage(e.Message);
        }

        /// <summary>
        /// Processes the messages from beam chat.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
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
        /// <summary>
        /// Sends a message to beam, reports back to the specified function.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="replyFunc">The reply function.</param>
        public static void SendBeamMessage(BeamMessage message, ReceiveReply replyFunc)
        {
            message.id = message.GetHashCode();

            _dicReplyFunctions.Add(message.id, replyFunc);

            string jsonMessage = JsonConvert.SerializeObject(message);

            _websocket.Send(jsonMessage);
        }

        public static string AuthKey;
        public static string UserId;
        public static string ChannelId;
        public static string Username;
        public static string Password;

        /// <summary>
        /// Setups the credentials.
        /// </summary>
        /// <param name="userId">The user unique identifier</param>
        /// <param name="channelId">The channel unique identifier</param>
        /// <param name="authKey">The authentication key.</param>
        public void SetupCredentials(int userId, int channelId, string authKey)
        {
            UserId = userId.ToString();
            ChannelId = channelId.ToString();
            AuthKey = authKey;
        }

        /// <summary>
        /// Setups the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void SetupCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Disconnect()
        {
            _websocket.Close(0, "Application Exit");
        }
    }
}
