using System.Collections.Generic;

namespace beam_client_csharp.Messages
{
    /// <summary>
    ///     An event is received from the chat server when an event occurs in the channel's chat.
    ///     This includes chat messages themselves, polls, and role changes.
    ///     The full list can be found in the
    ///     <see href="https://dev.beam.pro/reference/chat/index.html#chat__events">Events section</see>.
    /// </summary>
    public class BeamEventMessage : BeamMessage
    {
        public BeamEventMessage()
        {
            type = "event";
        }

        /// <summary>
        ///     Gets or sets the event.
        /// </summary>
        /// <value>The event name.</value>
        public string @event { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>Associated event data - may be of any type, specific to the event.</value>
        public virtual Dictionary<string, object> data { get; set; }
    }
}