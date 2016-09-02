// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="ChannelPlayer.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    ///     Class ChannelPlayer.
    /// </summary>
    public class ChannelPlayer
    {
        /// <summary>
        ///     Gets or sets the vod.
        /// </summary>
        /// <value>The vod.</value>
        public string vod { get; set; }

        /// <summary>
        ///     Gets or sets the RTMP.
        /// </summary>
        /// <value>The RTMP.</value>
        public string rtmp { get; set; }

        /// <summary>
        ///     Gets or sets the FTL.
        /// </summary>
        /// <value>The FTL.</value>
        public string ftl { get; set; }
    }
}