// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamExpandedChannel.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.BeamUser;

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    /// Class BeamExpandedChannel.
    /// </summary>
    public class BeamExpandedChannel : BeamChannelAdvanced
    {
        /// <summary>
        /// A resource object representing the thumbnail.
        /// Nullable
        /// </summary>
        public Resource thumbnail;
        /// <summary>
        /// A resource object representing the cover.
        /// Nullable
        /// </summary>
        public Resource cover;
        /// <summary>
        /// A resource object representing the badge.
        /// Nullable
        /// </summary>
        public Resource badge;
        /// <summary>
        /// The cache
        /// </summary>
        public List<object> cache;

        /// <summary>
        /// The channel preferences.
        /// </summary>
        [Obsolete]
        public ChannelPreferences preferences;
    }
}
