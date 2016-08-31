using System.Collections.Generic;

namespace beam_client_csharp
{
    public class BeamChatInfo
    {
        public List<string> endpoints { get; set; }
        public string authkey { get; set; }
        public List<string> permissions { get; set; }
    }
}