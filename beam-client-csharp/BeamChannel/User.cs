// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="User.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamChannel
{
    /// <summary>
    /// Class User.
    /// </summary>
    public class User
    {
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
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is verified.
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
        public string bio { get; set; }
        /// <summary>
        /// Gets or sets the primary team.
        /// </summary>
        /// <value>The primary team.</value>
        public int? primaryTeam { get; set; }
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public string createdAt { get; set; }
        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        public string updatedAt { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>The deleted at.</value>
        public object deletedAt { get; set; }
    }
}