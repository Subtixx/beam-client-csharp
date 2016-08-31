using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using beam_client_csharp;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BeamWeb bWeb = new BeamWeb();
            Task<Dictionary<string, object>> res = bWeb.Authenticate("lol", "lol");
            if (res.Result.ContainsKey("statusCode"))
            {
                switch (int.Parse(res.Result["statusCode"].ToString()))
                {
                    default:
                        throw new NotImplementedException(res.Result["statusCode"].ToString());

                    case 401:
                        Console.WriteLine("Login was not successfull");
                        break;
                }
            }
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
    }
}
