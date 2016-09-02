// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamUser.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using beam_client_csharp.BeamWebReplies.BeamChannel;

namespace beam_client_csharp.BeamUser
{
    /// <summary>
    /// Class BeamUser.
    /// </summary>
    public class BeamUser : BeamTimestamped
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int level { get; set; }
        /// <summary>
        /// Gets or sets the social.
        /// </summary>
        /// <value>The social.</value>
        public Social social { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string email { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamUser"/> is verified.
        /// </summary>
        /// <value><c>true</c> if verified; otherwise, <c>false</c>.</value>
        public bool verified { get; set; }
        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        /// <value>The experience.</value>
        public int experience { get; set; }
        /// <summary>
        /// Gets or sets the sparks.
        /// </summary>
        /// <value>The sparks.</value>
        public int sparks { get; set; }
        /// <summary>
        /// Gets or sets the avatar URL.
        /// </summary>
        /// <value>The avatar URL.</value>
        public string avatarUrl { get; set; }
        /// <summary>
        /// Gets or sets the bio.
        /// </summary>
        /// <value>The bio.</value>
        public object bio { get; set; }
        /// <summary>
        /// Gets or sets the primary team.
        /// </summary>
        /// <value>The primary team.</value>
        public int primaryTeam { get; set; }
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public List<Group> groups { get; set; }
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public Channel channel { get; set; }
        /// <summary>
        /// Gets or sets the two factor.
        /// </summary>
        /// <value>The two factor.</value>
        public TwoFactor twoFactor { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has two factor.
        /// </summary>
        /// <value><c>true</c> if this instance has two factor; otherwise, <c>false</c>.</value>
        public bool hasTwoFactor { get; set; }
        /// <summary>
        /// Gets or sets the preferences.
        /// </summary>
        /// <value>The preferences.</value>
        public Preferences preferences { get; set; }
    }
}