using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_ChatInfo
    {
        [Test]
        public void TestPermissions()
        {
            BeamChatInfo chatInfo = JsonConvert.DeserializeObject<BeamChatInfo>(TestHelpers.GetResource("beam_client_csharp.Tests.TestData.chatinfo.json"));

            List<string> perms = new List<string>
            {
                "change_ban",
                "edit_options",
                "change_role",
                "bypass_links",
                "bypass_slowchat",
                "clear_messages",
                "remove_message",
                "poll_start",
                "giveaway_start",
                "timeout",
                "view_deleted",
                "chat",
                "connect",
                "whisper",
                "poll_vote"
            };
            if (chatInfo.permissions.Count != perms.Count)
                Assert.Fail($"ChatInfo permission mismatch {chatInfo.permissions.Count} != {perms.Count}");

            foreach (var permission in chatInfo.permissions)
            {
                if (!perms.Contains(permission))
                    Assert.Fail($"ChatInfo permission mismatch {permission} missing in perms");
            }
            Assert.Pass("Permissions passed successfully");
        }

        [Test]
        public void TestAuthkey()
        {
            BeamChatInfo chatInfo = JsonConvert.DeserializeObject<BeamChatInfo>(TestHelpers.GetResource("beam_client_csharp.Tests.TestData.chatinfo.json"));
            if (chatInfo.authkey != "strippedforobviousreasons")
                Assert.Fail("Authkey mismatch {chatInfo.authkey} != \"strippedforobviousreasons\"");
            Assert.Pass("Authkey passed successfully");
        }

        [Test]
        public void TestEndpoints()
        {
            BeamChatInfo chatInfo = JsonConvert.DeserializeObject<BeamChatInfo>(TestHelpers.GetResource("beam_client_csharp.Tests.TestData.chatinfo.json"));
            if (chatInfo.endpoints[0] != "wss://chat1-dal.beam.pro:443")
                Assert.Fail($"ChatInfo endpoint mismatch {chatInfo.endpoints[0]} != \"wss://chat1-dal.beam.pro:443\"");
            if (chatInfo.endpoints[1] != "wss://chat3-dal.beam.pro:443")
                Assert.Fail($"ChatInfo endpoint mismatch {chatInfo.endpoints[1]} != \"wss://chat3-dal.beam.pro:443\"");
            if (chatInfo.endpoints[2] != "wss://chat2-dal.beam.pro:443")
                Assert.Fail($"ChatInfo endpoint mismatch {chatInfo.endpoints[2]} != \"wss://chat2-dal.beam.pro:443\"");

            Assert.Pass("Endpoints passed successfully");
        }
    }
}
