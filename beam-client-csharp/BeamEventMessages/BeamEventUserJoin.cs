// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamEventUserJoin.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.BeamEventMessages.ChatMessage;
using beam_client_csharp.Messages;

namespace beam_client_csharp.BeamEventMessages
{
    /// <summary>
    /// Class BeamEventUserJoin.
    /// </summary>
    public class BeamEventUserJoin : BeamMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BeamEventUserJoin"/> class.
        /// </summary>
        public BeamEventUserJoin()
        {
            type = "event";
            @event = "UserJoin";
        }

        /// <summary>
        /// The username
        /// </summary>
        public string username;
        /// <summary>
        /// The roles
        /// </summary>
        public List<string> roles;

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        public string @event { get; set; }
    }
}
