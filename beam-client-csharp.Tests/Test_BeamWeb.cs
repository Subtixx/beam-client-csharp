using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.BeamWebReplies.BeamUser;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_BeamWeb
    {
        [Test]
        public void Test_LoginFail()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamPrivatePopulatedUser> res = bWeb.Authenticate("app-veyor", "app-veyor");
            if (res.Result == null)
            {
                Assert.Pass("BeamWeb Login fail test success\n");
            }
        }

        [Test]
        public void Test_ChatInfo()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamChatInfo> res = bWeb.GetChatInformation(197242);
            BeamChatInfo chatInfo = res.Result;

            if (chatInfo == null || chatInfo.endpoints.Count == 0)
            {
                Assert.Fail("Could not get chatinfo\n");
            }

            Assert.Pass("Got Chatinfo!\n");
        }
    }
}
