namespace beam_client_csharp.BeamUser
{
    public class Channel
    {
        public int tetrisGameId { get; set; }
        public int id { get; set; }
        public int userId { get; set; }
        public string token { get; set; }
        public bool online { get; set; }
        public bool featured { get; set; }
        public bool partnered { get; set; }
        public int transcodingProfileId { get; set; }
        public bool suspended { get; set; }
        public string name { get; set; }
        public string audience { get; set; }
        public int viewersTotal { get; set; }
        public int viewersCurrent { get; set; }
        public int numFollowers { get; set; }
        public int numSubscribers { get; set; }
        public int maxConcurrentSubscribers { get; set; }
        public string description { get; set; }
        public int typeId { get; set; }
        public bool interactive { get; set; }
        public int interactiveGameId { get; set; }
        public int ftl { get; set; }
        public bool hasVod { get; set; }
        public object languageId { get; set; }
        public int coverId { get; set; }
        public int thumbnailId { get; set; }
        public object badgeId { get; set; }
        public object hosteeId { get; set; }
        public bool hasTranscodes { get; set; }
        public bool vodsEnabled { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public object deletedAt { get; set; }
    }
}