// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamChannel.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamChannel
{
    /// <summary>
    /// Class BeamChannel.
    /// </summary>
    public class BeamChannel
    {
        /// <summary>
        /// Gets or sets the tetris game identifier.
        /// </summary>
        /// <value>The tetris game identifier.</value>
        public int? tetrisGameId { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int userId { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string token { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamChannel"/> is online.
        /// </summary>
        /// <value><c>true</c> if online; otherwise, <c>false</c>.</value>
        public bool online { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamChannel"/> is featured.
        /// </summary>
        /// <value><c>true</c> if featured; otherwise, <c>false</c>.</value>
        public bool featured { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamChannel"/> is partnered.
        /// </summary>
        /// <value><c>true</c> if partnered; otherwise, <c>false</c>.</value>
        public bool partnered { get; set; }
        /// <summary>
        /// Gets or sets the transcoding profile identifier.
        /// </summary>
        /// <value>The transcoding profile identifier.</value>
        public int? transcodingProfileId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamChannel"/> is suspended.
        /// </summary>
        /// <value><c>true</c> if suspended; otherwise, <c>false</c>.</value>
        public bool suspended { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>The audience.</value>
        public string audience { get; set; }
        /// <summary>
        /// Gets or sets the viewers total.
        /// </summary>
        /// <value>The viewers total.</value>
        public int viewersTotal { get; set; }
        /// <summary>
        /// Gets or sets the viewers current.
        /// </summary>
        /// <value>The viewers current.</value>
        public int viewersCurrent { get; set; }
        /// <summary>
        /// Gets or sets the number followers.
        /// </summary>
        /// <value>The number followers.</value>
        public int numFollowers { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }
        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public int? typeId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamChannel"/> is interactive.
        /// </summary>
        /// <value><c>true</c> if interactive; otherwise, <c>false</c>.</value>
        public bool interactive { get; set; }
        /// <summary>
        /// Gets or sets the interactive game identifier.
        /// </summary>
        /// <value>The interactive game identifier.</value>
        public int? interactiveGameId { get; set; }
        /// <summary>
        /// Gets or sets the FTL.
        /// </summary>
        /// <value>The FTL.</value>
        public int ftl { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has vod.
        /// </summary>
        /// <value><c>true</c> if this instance has vod; otherwise, <c>false</c>.</value>
        public bool hasVod { get; set; }
        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>The language identifier.</value>
        public object languageId { get; set; }
        /// <summary>
        /// Gets or sets the cover identifier.
        /// </summary>
        /// <value>The cover identifier.</value>
        public int? coverId { get; set; }
        /// <summary>
        /// Gets or sets the thumbnail identifier.
        /// </summary>
        /// <value>The thumbnail identifier.</value>
        public int? thumbnailId { get; set; }
        /// <summary>
        /// Gets or sets the badge identifier.
        /// </summary>
        /// <value>The badge identifier.</value>
        public object badgeId { get; set; }
        /// <summary>
        /// Gets or sets the hostee identifier.
        /// </summary>
        /// <value>The hostee identifier.</value>
        public object hosteeId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has transcodes.
        /// </summary>
        /// <value><c>true</c> if this instance has transcodes; otherwise, <c>false</c>.</value>
        public bool hasTranscodes { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [vods enabled].
        /// </summary>
        /// <value><c>true</c> if [vods enabled]; otherwise, <c>false</c>.</value>
        public bool vodsEnabled { get; set; }
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
        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>The thumbnail.</value>
        public Thumbnail thumbnail { get; set; }
        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        /// <value>The cover.</value>
        public Cover cover { get; set; }
        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>The badge.</value>
        public object badge { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type type { get; set; }
        /// <summary>
        /// Gets or sets the preferences.
        /// </summary>
        /// <value>The preferences.</value>
        public Preferences preferences { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public User user { get; set; }
    }
}