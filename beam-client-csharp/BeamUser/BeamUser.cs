using System.Collections.Generic;

namespace beam_client_csharp.BeamUser
{
    public class BeamUser
    {
        public int level { get; set; }
        public Social social { get; set; }
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool verified { get; set; }
        public int experience { get; set; }
        public int sparks { get; set; }
        public string avatarUrl { get; set; }
        public object bio { get; set; }
        public int primaryTeam { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public object deletedAt { get; set; }
        public List<Group> groups { get; set; }
        public Channel channel { get; set; }
        public TwoFactor twoFactor { get; set; }
        public bool hasTwoFactor { get; set; }
        public Preferences preferences { get; set; }
    }
}