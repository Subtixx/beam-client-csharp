using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace beam_client_csharp.Tests
{
    [TestFixture]
    public class Test_BeamAchievement
    {
        [Test]
        public void TestAllAchievements()
        {
            List<BeamAchievement> controlGroup = new List<BeamAchievement>
            {
                new BeamAchievement
                {
                    slug = "alpha-user",
                    name = "Alpha",
                    description = "Be a part of Beam’s Alpha testing phase.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "beam-me-up",
                    name = "BeamMeUp",
                    description = "Thanks for signing up for Beam!",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "beta-user",
                    name = "Beta",
                    description = "Be a part of Beam’s Beta user group.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "followers-gold",
                    name = "Followers Gold",
                    description = "Have at least one thousands followers.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "followers-plat",
                    name = "Followers Platinum",
                    description = "Have at least ten thousand followers.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "followers-silver",
                    name = "Followers Silver",
                    description = "Have at least one hundred followers.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "helper-gold",
                    name = "Helper Gold",
                    description = "Be modded in 5 channels.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "helper-silver",
                    name = "Helper Silver",
                    description = "Be modded in at least one channel.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "stalker-gold",
                    name = "Stalker Gold",
                    description = "Follow at least one hundred channels.",
                    sparks = 0
                }, new BeamAchievement{
                    slug = "stalker-silver",
                    name = "Stalker Silver",
                    description = "Follow 10 channels.",
                    sparks = 0
                }
            };
            List<BeamAchievement> achievements = JsonConvert.DeserializeObject<List<BeamAchievement>>(TestHelpers.GetResource("beam_client_csharp.Tests.TestData.achievements.json"));

            if (achievements.Count != controlGroup.Count)
                Assert.Fail($"Achievement count {achievements.Count} != {controlGroup.Count} does not match");

            int i = 0;
            foreach (var beamAchievement in achievements)
            {
                if (beamAchievement.name != controlGroup[i].name)
                    Assert.Fail($"Achievement name {beamAchievement.name} != \"{controlGroup[i].name}\" is not correct!");
                if (beamAchievement.slug != controlGroup[i].slug)
                    Assert.Fail($"Achievement slug {beamAchievement.slug} != \"{controlGroup[i].slug}\" is not correct!");
                if (beamAchievement.sparks != controlGroup[i].sparks)
                    Assert.Fail($"Achievement sparks {beamAchievement.sparks} != {controlGroup[i].sparks} is not correct!");
                if (beamAchievement.description != controlGroup[i].description)
                    Assert.Fail($"Achievement description {beamAchievement.description} != \"{controlGroup[i].description}\" is not correct!");
                i++;
            }

            Assert.Pass("Test all Achievements passed!");
        }

        [Test]
        public void TestOneBeamAchievement()
        {
            List<BeamAchievement> achievements = JsonConvert.DeserializeObject<List<BeamAchievement>>(TestHelpers.GetResource("beam_client_csharp.Tests.TestData.achievements.json"));
            if (achievements.Count != 10)
                Assert.Fail($"Achievement count {achievements.Count} != 10 does not match");
            if (achievements[0].name != "Alpha")
                Assert.Fail($"Achievement name {achievements[0].name} != \"Alpha\" is not correct!");
            if(achievements[0].slug != "alpha-user")
                Assert.Fail($"Achievement slug {achievements[0].slug} != \"alpha - user\" is not correct!");
            if (achievements[0].sparks != 0)
                Assert.Fail($"Achievement sparks {achievements[0].sparks} != 0 is not correct!");
            if(achievements[0].description != "Be a part of Beam’s Alpha testing phase.")
                Assert.Fail($"Achievement description {achievements[0].description} != \"Be a part of Beam’s Alpha testing phase.\" is not correct!");

            Assert.Pass("Achievement class passed!");
        }
    }
}
