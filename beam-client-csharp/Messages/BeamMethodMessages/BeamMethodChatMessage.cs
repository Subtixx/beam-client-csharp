using System.Collections.Generic;

namespace beam_client_csharp.Messages.BeamMethodMessages
{
    public class BeamMethodChatMessage : BeamMethodMessage
    {
        public BeamMethodChatMessage()
        {
            type = "method";
            method = "msg";
            arguments = new List<string>();
        }
    }
}