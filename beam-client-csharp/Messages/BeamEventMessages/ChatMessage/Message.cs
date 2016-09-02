// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="Message.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.Messages.BeamEventMessages.ChatMessage
{
    /// <summary>
    ///     Class Message.
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public List<Message2> message { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public Meta meta { get; set; }
    }
}