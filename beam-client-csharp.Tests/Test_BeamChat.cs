using NUnit.Framework;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_BeamChat
    {
        public void Test_ChatConnect()
        {
            var bWeb = new BeamWeb();
            var res = bWeb.GetChatInformation(197242);
            var chatInfo = res.Result;
            if (chatInfo == null || chatInfo.endpoints.Count == 0)
            {
                Assert.Fail("Could not get chatinfo\n");
            }

            var bChat = new BeamChat();
            bChat.SetupWebsocket(chatInfo.endpoints[0]);
            bChat.Connect();
            bChat.Disconnect();

            Assert.Pass("Successfully connected to beam chat subtixx!\n");
        }
    }
}