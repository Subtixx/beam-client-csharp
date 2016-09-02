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

using System.Collections.Generic;

namespace beam_client_csharp.Messages.BeamEventMessages
{
    /// <summary>
    ///     Class BeamEventUserJoin.
    /// </summary>
    public class BeamEventUserJoin : BeamMessage
    {
        /// <summary>
        ///     The roles
        /// </summary>
        public List<string> roles;

        /// <summary>
        ///     The username
        /// </summary>
        public string username;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamEventUserJoin" /> class.
        /// </summary>
        public BeamEventUserJoin()
        {
            type = "event";
            @event = "UserJoin";
        }

        /// <summary>
        ///     Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        public string @event { get; set; }
    }
}