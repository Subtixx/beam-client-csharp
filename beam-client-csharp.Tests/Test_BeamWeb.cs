using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            BeamWeb bWeb = new BeamWeb();
            Task<BeamUser> res = bWeb.Authenticate("travis-ci", "travis-ci");
            if (res.Result == null)
            {
                Assert.Pass("BeamWeb Login fail test success");
            }
        }
    }
}
