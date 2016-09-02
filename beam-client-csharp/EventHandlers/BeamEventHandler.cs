// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamEventHandler.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using beam_client_csharp.Messages;
using beam_client_csharp.Messages.BeamEventMessages.ChatMessage;
using Newtonsoft.Json;

namespace beam_client_csharp.EventHandlers
{
    /// <summary>
    ///     Enum EventHandlerTypes
    /// </summary>
    public enum EventHandlerTypes
    {
        /// <summary>
        ///     The welcome event
        /// </summary>
        WelcomeEvent,

        /// <summary>
        ///     The chat message event
        /// </summary>
        ChatMessageEvent,

        /// <summary>
        ///     The delete message
        /// </summary>
        DeleteMessage,

        /// <summary>
        ///     The purge message
        /// </summary>
        PurgeMessage,

        /// <summary>
        ///     The clear messages
        /// </summary>
        ClearMessages,

        /// <summary>
        ///     The user update
        /// </summary>
        UserUpdate,

        /// <summary>
        ///     The user timeout
        /// </summary>
        UserTimeout,

        /// <summary>
        ///     The user join
        /// </summary>
        UserJoin,

        /// <summary>
        ///     The user leave
        /// </summary>
        UserLeave,

        /// <summary>
        ///     The poll start
        /// </summary>
        PollStart,

        /// <summary>
        ///     The poll end
        /// </summary>
        PollEnd
    }

    /// <summary>
    ///     Class BeamEventHandler.
    /// </summary>
    public class BeamEventHandler
    {
        /// <summary>
        ///     Delegate HandleEventFunc
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="underlayingMessage">The underlaying message.</param>
        public delegate void HandleEventFunc(BeamEventMessage message, string underlayingMessage);

        /// <summary>
        ///     The _event handlers
        /// </summary>
        private static readonly Dictionary<HandleEventFunc, EventHandlerTypes> _eventHandlers =
            new Dictionary<HandleEventFunc, EventHandlerTypes>();

        public BeamEventHandler()
        {
            AddEventHandler(EventHandlerTypes.ChatMessageEvent, (message, underlayingMessage) =>
            {
                var eventChatMsg =
                    JsonConvert.DeserializeObject<BeamEventChatMessage>(underlayingMessage);
                var chatMessage = eventChatMsg.data.message.message[0].text;
                switch (chatMessage)
                {
                    case "!about":
                        BeamChat.SendChatMessage(
                            "Written using the Beam Client C# API by Subtixx! https://github.com/Subtixx/beam-client-csharp");
                        break;
                }
            });
        }

        /// <summary>
        ///     Handles the event.
        /// </summary>
        /// <param name="eventMessage">The event message.</param>
        /// <param name="underlayingMessage">The underlaying message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void HandleEvent(BeamEventMessage eventMessage, string underlayingMessage)
        {
            switch (eventMessage.@event)
            {
                default:
                    throw new NotImplementedException(eventMessage.@event);

                case "WelcomeEvent":
                    SendLoginEvent();

                    RelayEvent(EventHandlerTypes.WelcomeEvent, eventMessage, underlayingMessage);
                    break;

                case "ChatMessage":
                    RelayEvent(EventHandlerTypes.ChatMessageEvent, eventMessage, underlayingMessage);
                    break;

                case "DeleteMessage":
                    RelayEvent(EventHandlerTypes.DeleteMessage, eventMessage, underlayingMessage);
                    break;

                case "PurgeMessage":
                    RelayEvent(EventHandlerTypes.PurgeMessage, eventMessage, underlayingMessage);
                    break;

                case "ClearMessages":
                    RelayEvent(EventHandlerTypes.ClearMessages, eventMessage, underlayingMessage);
                    break;

                case "UserUpdate":
                    RelayEvent(EventHandlerTypes.UserUpdate, eventMessage, underlayingMessage);
                    break;

                case "UserTimeout":
                    RelayEvent(EventHandlerTypes.UserTimeout, eventMessage, underlayingMessage);
                    break;

                // Because of some bizarre reasons, these are not documented to be a event.
                case "UserJoin":
                    RelayEvent(EventHandlerTypes.UserJoin, eventMessage, underlayingMessage);
                    break;

                case "UserLeave":
                    RelayEvent(EventHandlerTypes.UserLeave, eventMessage, underlayingMessage);
                    break;

                case "PollStart":
                    RelayEvent(EventHandlerTypes.PollStart, eventMessage, underlayingMessage);
                    break;

                case "PollEnd":
                    RelayEvent(EventHandlerTypes.PollEnd, eventMessage, underlayingMessage);
                    break;
            }
        }

        /// <summary>
        ///     Relays the event to all event functions.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="eventMessage">The event message.</param>
        /// <param name="underlayingMessage">The underlaying message.</param>
        private static void RelayEvent(EventHandlerTypes eventType, BeamEventMessage eventMessage,
            string underlayingMessage)
        {
            foreach (var handler in _eventHandlers)
                if (handler.Value == eventType)
                    handler.Key(eventMessage, underlayingMessage);
        }

        /// <summary>
        ///     Adds the event handler.
        /// </summary>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="function">The function.</param>
        public static void AddEventHandler(EventHandlerTypes handlerType, HandleEventFunc function)
        {
            _eventHandlers.Add(function, handlerType);
        }

        /// <summary>
        ///     Removes the event handler.
        /// </summary>
        /// <param name="function">The function.</param>
        public static void RemoveEventHandler(HandleEventFunc function)
        {
            _eventHandlers.Remove(function);
        }

        /// <summary>
        ///     Sends the login event.
        /// </summary>
        private static void SendLoginEvent()
        {
            var args = new List<string> {BeamChat.ChannelId};

            if (!string.IsNullOrEmpty(BeamChat.AuthKey) && !string.IsNullOrEmpty(BeamChat.ChannelId) &&
                !string.IsNullOrEmpty(BeamChat.UserId))
            {
                args.Add(BeamChat.UserId); // UserID
                args.Add(BeamChat.AuthKey);
            }
            else if (!string.IsNullOrEmpty(BeamChat.Username) &&
                     !string.IsNullOrEmpty(BeamChat.Password))
            {
                // WARNING: This is unsecure and NOT recommended!
                var beamWeb = new BeamWeb();
                var user = beamWeb.Authenticate(BeamChat.Username, BeamChat.Password).Result;

                if (user == null)
                    return;

                var chat = beamWeb.GetChatInformation(user.channel.id).Result;
                if (string.IsNullOrEmpty(chat.authkey))
                    return;

                args.Clear();
                args.Add(user.channel.id.ToString());
                args.Add(user.id.ToString());
                args.Add(chat.authkey);
            }

            BeamChat.SendBeamMessage(new BeamMethodMessage
            {
                method = "auth",
                arguments = args
            }, replyMessage =>
            {
                if (!(bool) replyMessage.data["authenticated"] && !string.IsNullOrEmpty(BeamChat.Username) &&
                    !string.IsNullOrEmpty(BeamChat.Password)
                    )
                    throw new Exception("Chat authentication failed!");
            });
        }
    }
}