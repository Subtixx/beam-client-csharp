using System.Collections.Generic;

namespace beam_client_csharp.BeamUser
{
    public class Social
    {
        public string player { get; set; }
        public string twitter { get; set; }
        public string youtube { get; set; }
        public List<string> verified { get; set; }
        public string discord { get; set; }
    }
}