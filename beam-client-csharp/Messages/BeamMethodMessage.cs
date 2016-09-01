using System.Collections.Generic;

namespace beam_client_csharp.Messages
{
    /// <summary>
    ///     A method is sent to the chat server the server will respond with a <see cref="BeamReplyMessage">Reply packet</see>.
    /// </summary>
    internal class BeamMethodMessage : BeamMessage
    {
        public BeamMethodMessage()
        {
            type = "method";
        }

        /// <summary>
        ///     Gets or sets the method name.
        /// </summary>
        /// <value>The method name to execute.</value>
        public string method { get; set; }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value>An array of arguments, specific per method type.</value>
        public IList<string> arguments { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     Must be a unique numeric ID. If the method you sent has a reply, the numeric ID will be sent back down in the
        ///     Reply packet.
        /// </value>
        public override int id { get; set; }
    }
}