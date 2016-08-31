using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.BeamEventMessages.ChatMessage;
using beam_client_csharp.Messages;
using Newtonsoft.Json;

namespace beam_client_csharp.EventHandlers
{
    public enum EventHandlerTypes
    {
        WelcomeEvent,
        ChatMessageEvent,
        DeleteMessage,
        PurgeMessage,
        ClearMessages,
        UserUpdate,
        UserTimeout
    }

    public static class BeamEventHandler
    {
        public delegate void HandleEventFunc(BeamEventMessage message, string underlayingMessage);
        private static Dictionary<HandleEventFunc, EventHandlerTypes> _eventHandlers = new Dictionary<HandleEventFunc, EventHandlerTypes>();

        public static void HandleEvent(BeamEventMessage eventMessage, string underlayingMessage)
        {
            switch (eventMessage.@event)
            {
                default:
                    throw new NotImplementedException(eventMessage.@event);

                case "WelcomeEvent":
                    SendLoginEvent();

                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.WelcomeEvent)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "ChatMessage":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.ChatMessageEvent)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "DeleteMessage":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.DeleteMessage)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "PurgeMessage":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.PurgeMessage)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "ClearMessages":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.ClearMessages)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "UserUpdate":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.UserUpdate)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;

                case "UserTimeout":
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Value == EventHandlerTypes.UserTimeout)
                        {
                            handler.Key(eventMessage, underlayingMessage);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Adds the event handler.
        /// </summary>
        /// <param name="handlerType">Type of the handler.</param>
        /// <param name="function">The function.</param>
        public static void AddEventHandler(EventHandlerTypes handlerType, HandleEventFunc function)
        {
            _eventHandlers.Add(function, handlerType);
        }

        /// <summary>
        /// Removes the event handler.
        /// </summary>
        /// <param name="function">The function.</param>
        public static void RemoveEventHandler(HandleEventFunc function)
        {
            _eventHandlers.Remove(function);
        }

        private static void SendLoginEvent()
        {
            List<string> args = new List<string> {BeamChat.ChannelId};

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
                BeamWeb beamWeb = new BeamWeb();
                BeamUser.BeamUser user = beamWeb.Authenticate(BeamChat.Username, BeamChat.Password).Result;

                if (user == null)
                    return;

                BeamChatInfo chat = beamWeb.ChatInfo(user.channel.id).Result;
                if (string.IsNullOrEmpty(chat.authkey))
                    return;

                args.Clear();
                args.Add(user.channel.id.ToString());
                args.Add(user.id.ToString());
                args.Add(chat.authkey);
            }

            BeamMethodMessage sendMessage = new BeamMethodMessage
            {
                method = "auth",
                arguments = args
            };
            BeamChat.SendBeamMessage(sendMessage, replyMessage => { });
        }
    }
}
