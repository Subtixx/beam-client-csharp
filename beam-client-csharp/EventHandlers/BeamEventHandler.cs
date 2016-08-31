using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.Messages;

namespace beam_client_csharp.EventHandlers
{
    static class BeamEventHandler
    {
        public static void HandleEvent(BeamEventMessage message)
        {
            switch (message.@event)
            {
                default:
                    throw new NotImplementedException(message.@event);

                case "WelcomeEvent":
                    Console.WriteLine("Got hello from server!");
                    List<string> args = new List<string>();
                    args.Add("123456");

                    BeamMethodMessage sendMessage = new BeamMethodMessage();
                    sendMessage.method = "auth";
                    sendMessage.arguments = args;
                    sendMessage.id = 1337;
                    BeamChat.SendBeamMessage(sendMessage, replyMessage => {});
                    break;
            }
        }
    }
}
