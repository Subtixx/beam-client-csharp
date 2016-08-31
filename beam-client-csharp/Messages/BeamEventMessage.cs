using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.Messages
{
    /// <summary>
    /// An event is received from the chat server when an event occurs in the channel's chat.
    /// This includes chat messages themselves, polls, and role changes.
    /// The full list can be found in the <see href="https://dev.beam.pro/reference/chat/index.html#chat__events">Events section</see>. 
    /// </summary>
    public class BeamEventMessage : BeamMessage
    {
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event name.</value>
        public string @event { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>Associated event data - may be of any type, specific to the event.</value>
        public virtual Dictionary<string, object> data { get; set; }

        public BeamEventMessage()
        {
            type = "event";
        }
    }
}
