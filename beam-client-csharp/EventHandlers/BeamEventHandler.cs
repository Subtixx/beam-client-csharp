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
                    args.Add(BeamChat.ChannelId); // TODO: Replace with channelID

                    if (!string.IsNullOrEmpty(BeamChat.AuthKey) && !string.IsNullOrEmpty(BeamChat.ChannelId) &&
                         !string.IsNullOrEmpty(BeamChat.UserId))
                    {
                        args.Add(BeamChat.UserId); // UserID
                        args.Add(BeamChat.AuthKey);
                    }else if (!string.IsNullOrEmpty(BeamChat.Username) &&
                         !string.IsNullOrEmpty(BeamChat.Password))
                    {
                        // WARNING: This is unsecure and NOT recommended!
                        BeamWeb beamWeb = new BeamWeb();
                        BeamUser user = beamWeb.Authenticate(BeamChat.Username, BeamChat.Password).Result;

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
                    BeamChat.SendBeamMessage(sendMessage, replyMessage => {});
                    break;
            }
        }
    }
}
