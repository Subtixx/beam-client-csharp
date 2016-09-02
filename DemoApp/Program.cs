﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using beam_client_csharp;
using beam_client_csharp.BeamEventMessages;
using beam_client_csharp.BeamEventMessages.ChatMessage;
using beam_client_csharp.EventHandlers;
using Newtonsoft.Json;

namespace DemoApp
{
    internal class Program
    {
        private static Dictionary<string, string> _configDictionary;

        private static void Main(string[] args)
        {
            LoadConfig();

            if (!_configDictionary.ContainsKey("username") ||
                !_configDictionary.ContainsKey("password"))
                return;

            var bWeb = new BeamWeb();
            var res = bWeb.Authenticate(_configDictionary["username"], _configDictionary["password"]);
            var user = res.Result;
            if (user == null)
            {
                throw new ArgumentException("Login incorrect?");
            }

            var chatInfo = bWeb.GetChatInformation(user.channel.id).Result;
            if (chatInfo == null || chatInfo.endpoints.Count == 0)
            {
                throw new ArgumentException("Channel Id incorrect?");
            }

            Console.WriteLine("UserID: {0}, ChannelID: {1}", user.id, user.channel.id);

            var bChat = new BeamChat();
            bChat.SetupWebsocket(chatInfo.endpoints[0]);
            bChat.SetupCredentials(user.id, user.channel.id, chatInfo.authkey);
            bChat.Connect();

            BeamEventHandler.AddEventHandler(EventHandlerTypes.UserJoin, ((message, underlayingMessage) =>
            {
                var userJoinEv = JsonConvert.DeserializeObject<BeamEventUserJoin>(underlayingMessage);
                Console.WriteLine("User joined: " + userJoinEv.username);
            }));

            BeamEventHandler.AddEventHandler(EventHandlerTypes.UserLeave, ((message, underlayingMessage) =>
            {
                var userLeaveEv = JsonConvert.DeserializeObject<BeamEventUserLeave>(underlayingMessage);
                Console.WriteLine("User left: " + userLeaveEv.username);
            }));

            BeamEventHandler.AddEventHandler(EventHandlerTypes.ChatMessageEvent, (message, underlayingMessage) =>
            {
                var chatMessage = JsonConvert.DeserializeObject<BeamEventChatMessage>(underlayingMessage);
                Console.WriteLine("Received Chat message: {0}", chatMessage.data.message.message[0].text);
            });

            Console.ReadKey();

            bChat.Disconnect();
            /*BeamChat beamChat = new BeamChat();

            beamChat.SetupCredentials("");

            beamChat.SetupWebsocket("wss://chat3-dal.beam.pro");
            beamChat.Connect();
            while (true)
            {
                Thread.Sleep(5000);
            }*/

            /*
            beamChat.websocket_MessageReceived(null, new MessageReceivedEventArgs("{\"type\":\"event\",\"event\":\"WelcomeEvent\",\"data\":{\"server\":\"891c12de-d4f8-4b4f-971f-5e69b6b65075\"}}"));*/
        }

        private static void LoadConfig()
        {
            if (!File.Exists("config.xml"))
                return;
            _configDictionary = new Dictionary<string, string>();

            var xdoc = new XmlDocument();
            xdoc.Load("config.xml");
            var xmlAttributeCollection = xdoc.ChildNodes[1].Attributes;
            if (xmlAttributeCollection != null)
                foreach (XmlAttribute xnn in xmlAttributeCollection)
                {
                    _configDictionary.Add(xnn.Name, xnn.Value);
                }
        }
    }
}