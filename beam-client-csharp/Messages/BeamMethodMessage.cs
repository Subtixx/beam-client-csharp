// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamMethodMessage.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.Messages
{
    /// <summary>
    ///     A method is sent to the chat server the server will respond with a <see cref="BeamReplyMessage">Reply packet</see>.
    /// </summary>
    public class BeamMethodMessage : BeamMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamMethodMessage" /> class.
        /// </summary>
        public BeamMethodMessage()
        {
            type = "method";
            arguments = new List<string>();
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
        public List<string> arguments { get; set; }

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