using beam_client_csharp.Messages;

namespace beam_client_csharp.BeamEventMessages.ChatMessage
{
    public class BeamEventChatMessage : BeamMessage
    {
        public BeamEventChatMessage()
        {
            type = "event";
            @event = "ChatMessage";
        }

        public string @event { get; set; }
        public Data data { get; set; }
    }
}