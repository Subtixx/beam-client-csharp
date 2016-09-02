// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamEventChatMessage.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.Messages.BeamEventMessages.ChatMessage
{
    /// <summary>
    ///     Class BeamEventChatMessage.
    /// </summary>
    public class BeamEventChatMessage : BeamMessage
    {
        /// <summary>
        ///     The event
        /// </summary>
        public string @event;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamEventChatMessage" /> class.
        /// </summary>
        public BeamEventChatMessage()
        {
            type = "event";
            @event = "ChatMessage";
        }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public Data data { get; set; }
    }
}