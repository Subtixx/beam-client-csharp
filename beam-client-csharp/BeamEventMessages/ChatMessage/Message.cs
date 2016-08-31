using System.Collections.Generic;

namespace beam_client_csharp.BeamEventMessages.ChatMessage
{
    public class Message
    {
        public List<Message2> message { get; set; }
        public Meta meta { get; set; }
    }
}