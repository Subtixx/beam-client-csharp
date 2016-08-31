using beam_client_csharp.Messages;

namespace beam_client_csharp.BeamEventMessages.ChatMessage
{
    public class BeamEventChatMessage : BeamMessage
    {
        public string @event { get; set; }
        public Data data { get; set; }

        public BeamEventChatMessage()
        {
            type = "event";
            @event = "ChatMessage";
        }
    }
}