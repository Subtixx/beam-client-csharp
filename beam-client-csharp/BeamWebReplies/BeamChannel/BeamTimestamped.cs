// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamTimestamped.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    /// A type that contains information about creation, update and deletion dates.
    /// </summary>
    public class BeamTimestamped
    {
        /// <summary>
        /// The creation date of the channel.
        /// </summary>
        public string createdAt;
        /// <summary>
        /// The update date of the channel.
        /// </summary>
        public string updatedAt;
        /// <summary>
        /// The deletion date of the channel.
        /// </summary>
        public string deletedAt;
    }
}
