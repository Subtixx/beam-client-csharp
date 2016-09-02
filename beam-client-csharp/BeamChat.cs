// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamChat.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using beam_client_csharp.EventHandlers;
using beam_client_csharp.Messages;
using beam_client_csharp.Messages.BeamMethodMessages;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace beam_client_csharp
{
    /// <summary>
    ///     Class to communicate with Beams Chat
    /// </summary>
    public class BeamChat
    {
        /// <summary>
        ///     Delegate ReceiveReply
        /// </summary>
        /// <param name="message">The message.</param>
        public delegate void ReceiveReply(BeamReplyMessage message);

        /// <summary>
        ///     The _websocket
        /// </summary>
        private static WebSocket _websocket;

        /// <summary>
        ///     The _dic reply functions
        /// </summary>
        private static Dictionary<int, ReceiveReply> _dicReplyFunctions;

        /// <summary>
        ///     The authentication key
        /// </summary>
        public static string AuthKey;

        /// <summary>
        ///     The user identifier
        /// </summary>
        public static string UserId;

        /// <summary>
        ///     The channel identifier
        /// </summary>
        public static string ChannelId;

        /// <summary>
        ///     The username
        /// </summary>
        public static string Username;

        /// <summary>
        ///     The password
        /// </summary>
        public static string Password;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamChat" /> class.
        /// </summary>
        public BeamChat()
        {
            _dicReplyFunctions = new Dictionary<int, ReceiveReply>();
        }

        /// <summary>
        ///     Setups the websocket and readies it to connect.
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
        ///     Connects the websocket to the server.
        /// </summary>
        public void Connect()
        {
            _websocket.Open();
        }

        /// <summary>
        ///     Handles the Opened event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void websocket_Opened(object sender, EventArgs e)
        {
#if DEBUG
            Console.WriteLine("Websocket opened!");
#endif
        }

        /// <summary>
        ///     Handles the Closed event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void websocket_Closed(object sender, EventArgs e)
        {
#if DEBUG
            Console.WriteLine("Websocket closed!");
#endif
        }

        /// <summary>
        ///     Handles the Error event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ErrorEventArgs" /> instance containing the event data.</param>
        private void websocket_Error(object sender, ErrorEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Websocket error {0}", e.Exception.Message);
#endif
        }

        /// <summary>
        ///     Handles the MessageReceived event of the websocket control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MessageReceivedEventArgs" /> instance containing the event data.</param>
        public void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Received: " + e.Message);
#endif
            ProcessMessage(e.Message);
        }

        /// <summary>
        ///     Processes the messages from beam chat.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ProcessMessage(string message)
        {
            var beamMessage = JsonConvert.DeserializeObject<BeamMessage>(message);

            switch (beamMessage.type)
            {
                case "event":
                    var mEvent = JsonConvert.DeserializeObject<BeamEventMessage>(message);
                    BeamEventHandler.HandleEvent(mEvent, message);
                    break;
                case "reply":
                    var mReply = JsonConvert.DeserializeObject<BeamReplyMessage>(message);
                    if (_dicReplyFunctions.ContainsKey(beamMessage.id)) // Relay message to sender class
                    {
                        _dicReplyFunctions[beamMessage.id](mReply);
                    }
                    break;

                default:
                    throw new NotImplementedException(beamMessage.type);
            }
        }

        /// <summary>
        ///     Sends a message to beam, reports back to the specified function.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="replyFunc">The reply function.</param>
        public static void SendBeamMessage(BeamMessage message, ReceiveReply replyFunc = null)
        {
            if (replyFunc != null) // reply is optional.
            {
                message.id = message.GetHashCode();
                _dicReplyFunctions.Add(message.id, replyFunc);
            }

            var jsonMessage = JsonConvert.SerializeObject(message);

            _websocket.Send(jsonMessage);
        }

        /// <summary>
        ///     Sends a chat message.
        /// </summary>
        /// <param name="message">A list of messages.</param>
        public static void SendChatMessage(List<string> message)
        {
            SendBeamMessage(new BeamMethodChatMessage {arguments = message});
        }

        /// <summary>
        ///     Sends a chat message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void SendChatMessage(string message)
        {
            SendBeamMessage(new BeamMethodChatMessage
            {
                arguments = new List<string>
                {
                    message
                }
            });
        }

        /// <summary>
        ///     Setups the credentials.
        /// </summary>
        /// <param name="userId">The user unique identifier</param>
        /// <param name="channelId">The channel unique identifier</param>
        /// <param name="authKey">The authentication key.</param>
        public void SetupCredentials(uint userId, uint channelId, string authKey)
        {
            UserId = userId.ToString();
            ChannelId = channelId.ToString();
            AuthKey = authKey;
        }

        /// <summary>
        ///     Setups the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void SetupCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            _websocket.Close(0, "Application Exit");
        }
    }
}