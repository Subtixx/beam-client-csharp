using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using beam_client_csharp;
using beam_client_csharp.EventHandlers;
using beam_client_csharp.Messages;
using beam_client_csharp.Messages.BeamEventMessages;
using beam_client_csharp.Messages.BeamEventMessages.ChatMessage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemoApp
{
    internal class Program
    {
        private static Dictionary<string, string> _configDictionary;
		
		public static List<string> ActiveUsers = new List<string>();

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
                Console.WriteLine("User joined: " + userJoinEv.data.username);
				
				ActiveUsers.Add(userJoinEv.data.username);
            }));

            BeamEventHandler.AddEventHandler(EventHandlerTypes.UserLeave, ((message, underlayingMessage) =>
            {
                var userLeaveEv = JsonConvert.DeserializeObject<BeamEventUserLeave>(underlayingMessage);
                Console.WriteLine("User left: " + userLeaveEv.data.username);
				
				ActiveUsers.Remove(userLeaveEv.data.username);
            }));

            BeamEventHandler.AddEventHandler(EventHandlerTypes.ChatMessageEvent, (message, underlayingMessage) =>
            {
                var chatMessage = JsonConvert.DeserializeObject<BeamEventChatMessage>(underlayingMessage);
				string chatText = chatMessage.data.message.message[0].text;
                Console.WriteLine("Received Chat message: {0}", chatText);
				switch(chatText)
				{
					case "!help":
						BeamChat.SendChatMessage("Hello! This is a little test. This was sent using the Beam Client C# API written by Subtixx and released under GPLv3");
					break;
					
					case "!list":
						string sendString = "";
						if(ActiveUsers.Count > 0)
						{
							for(int i = 0; i <= ActiveUsers.Count; i++)
							{
								sendString += ActiveUsers[i];
								if(i < ActiveUsers.Count)
									sendString += ", ";
							}
							BeamChat.SendChatMessage($"Users in this channel: {sendString}");
						}else
							BeamChat.SendChatMessage("No users online :(");
					break;
					
					case "!shutdown":
						if(chatMessage.data.user_roles.Contains("Owner"))
						{
							BeamChat.SendChatMessage("Shutting down!");
							Environment.Exit(0);
						} else {
							BeamChat.SendChatMessage("No permission!");
						}
					break;
				}
            });
			
			
			BeamEventHandler.AddEventHandler(EventHandlerTypes.WelcomeEvent, (message, underlayingMessage) =>
            {
				BeamChat.SendBeamMessage(new BeamMethodMessage{
					method = "history"
				}, replyMessage =>
				{
					List<Data> historyMessages = ((JArray)replyMessage.data).ToObject<List<Data>>();
					foreach(Data msg in historyMessages)
					{
						Console.WriteLine(msg.message.message[0].text);
					}
				});
			});
			
			string line = "";
			while(line != "quit")
			{
				line = Console.ReadLine();
				switch(line)
				{
					case "quit":
						Environment.Exit(0);
					break;
					
					case "clear":
						BeamChat.SendBeamMessage(new BeamMethodMessage{
							method = "clearMessages"
						});
					break;
					
					default:
						Console.WriteLine("Unknown command!");
					break;
				}
			}

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