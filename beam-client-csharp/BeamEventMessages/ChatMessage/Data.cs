using System.Collections.Generic;

namespace beam_client_csharp.BeamEventMessages.ChatMessage
{
    public class Data
    {
        public int channel { get; set; }
        public string id { get; set; }
        public string user_name { get; set; }
        public int user_id { get; set; }
        public List<string> user_roles { get; set; }
        public Message message { get; set; }
    }
}