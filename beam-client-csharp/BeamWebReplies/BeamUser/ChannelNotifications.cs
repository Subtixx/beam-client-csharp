// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="ChannelNotifications.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    ///     Class ChannelNotifications.
    /// </summary>
    public class ChannelNotifications
    {
        /// <summary>
        ///     Gets or sets the ids.
        /// </summary>
        /// <value>The ids.</value>
        public List<string> ids { get; set; }

        /// <summary>
        ///     Gets or sets the transports.
        /// </summary>
        /// <value>The transports.</value>
        public List<string> transports { get; set; }
    }
}