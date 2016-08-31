using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using beam_client_csharp;
using Newtonsoft.Json.Linq;

namespace DemoApp
{
    class Program
    {
        private static Dictionary<string, string> _configDictionary;

        static void Main(string[] args)
        {
            loadConfig();

            if (!_configDictionary.ContainsKey("username") ||
                !_configDictionary.ContainsKey("password"))
                return;

            BeamWeb bWeb = new BeamWeb();
            Task<BeamUser> res = bWeb.Authenticate(_configDictionary["username"], _configDictionary["password"]);
            BeamUser user = res.Result;
            if (user == null)
            {
                throw new ArgumentException("Login incorrect?");
            }

            Console.WriteLine("UserID: {0}, ChannelID: {1}", user.id, user.channel.id);

            Console.ReadKey();
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

        private static void loadConfig()
        {
            _configDictionary = new Dictionary<string, string>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("config.xml");
            foreach (XmlAttribute xnn in xdoc.ChildNodes[1].Attributes)
            {
                _configDictionary.Add(xnn.Name, xnn.Value);
            }
        }
    }
}
