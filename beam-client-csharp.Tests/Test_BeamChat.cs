using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_BeamChat
    {
        public void Test_ChatConnect()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamChatInfo> res = bWeb.ChatInfo(197242);
            BeamChatInfo chatInfo = res.Result;
            if (chatInfo == null || chatInfo.endpoints.Count == 0)
            {
                Assert.Fail("Could not get chatinfo");
            }

            BeamChat bChat = new BeamChat();
            bChat.SetupWebsocket(chatInfo.endpoints[0]);
            bChat.Connect();
            bChat.Disconnect();

            Assert.Pass("Successfully connected to beam chat subtixx!");
        }
    }
}