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
            Task<Dictionary<string, object>> res = bWeb.Authenticate("travis-ci", "travis-ci");
            if (res.Result.ContainsKey("statusCode"))
            {
                switch (int.Parse(res.Result["statusCode"].ToString()))
                {
                    default:
                        throw new NotImplementedException(res.Result["statusCode"].ToString());

                    case 401:
                        Assert.Pass("BeamWeb 401 Login success");
                        break;
                }
            }
        }
    }
}
