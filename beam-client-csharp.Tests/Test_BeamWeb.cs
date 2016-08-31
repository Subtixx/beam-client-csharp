using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_BeamWeb
    {
        [Test]
        public void Test_LoginFail()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamUser> res = bWeb.Authenticate("travis-ci", "travis-ci");
            if (res.Result == null)
            {
                Assert.Pass("BeamWeb Login fail test success");
            }
        }

        [Test]
        public void Test_ChatInfo()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamChatInfo> res = bWeb.ChatInfo(197242);
            BeamChatInfo chatInfo = res.Result;

            if (chatInfo == null || chatInfo.endpoints.Count == 0)
            {
                Assert.Fail("Could not get chatinfo");
            }

            Assert.Pass("Got Chatinfo!");
        }
    }
}
