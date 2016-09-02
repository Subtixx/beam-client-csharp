// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamUser.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using beam_client_csharp.BeamWebReplies.BeamChannel;

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    /// Class BeamUser.
    /// https://dev.beam.pro/rest.html#User
    /// </summary>
    public class BeamUser : BeamTimestamped
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The unique ID of the user.</value>
        public uint id { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The users experience level, related to experience.</value>
        public uint level { get; set; }
        /// <summary>
        /// Gets or sets the social.
        /// </summary>
        /// <value>Social links.</value>
        public SocialInfo social { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>minLength: 4
        /// maxLength: 20
        /// pattern: ^[A-Za-z_][\w-]+$</value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string email { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamUser" /> is verified.
        /// </summary>
        /// <value><c>true</c> if verified email; otherwise, <c>false</c>.</value>
        public bool verified { get; set; }
        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        /// <value>The experience.</value>
        public uint experience { get; set; }
        /// <summary>
        /// Gets or sets the sparks.
        /// </summary>
        /// <value>The sparks.</value>
        public uint sparks { get; set; }
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
        public uint? primaryTeam { get; set; }

        /// <summary>
        /// Gets or sets the transcoding profile identifier.
        /// </summary>
        /// <value>The ID of the transcoding profile currently active.</value>
        public uint? transcodingProfileId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has transcodes.
        /// </summary>
        /// <value><c>true</c> if the user can choose a transcode profile; otherwise, <c>false</c>.</value>
        public bool hasTranscodes { get; set; }
        /*
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
        */
    }
}