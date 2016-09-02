// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="Social.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    /// Class Social.
    /// </summary>
    public class SocialInfo
    {
        /// <summary>
        /// Gets or sets the twitter.
        /// </summary>
        /// <value>Twitter profile URL.</value>
        public string twitter { get; set; }
        /// <summary>
        /// Gets or sets the facebook.
        /// </summary>
        /// <value>Facebook profile URL.</value>
        public string facebook { get; set; }
        /// <summary>
        /// Gets or sets the youtube.
        /// </summary>
        /// <value>Youtube profile URL.</value>
        public string youtube { get; set; }
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>Player.me profile URL.</value>
        public string player { get; set; }
        /// <summary>
        /// Gets or sets the discord.
        /// </summary>
        /// <value>Discord username and tag.</value>
        public string discord { get; set; }
        /// <summary>
        /// Gets or sets the verified.
        /// </summary>
        /// <value>A list of social keys which have been verified via linking the Beam account with the account on the corresponding external service.</value>
        public List<string> verified { get; set; }
    }
}