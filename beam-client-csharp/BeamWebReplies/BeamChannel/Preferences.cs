// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="Preferences.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamChannel
{
    /// <summary>
    /// Class Preferences.
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// Gets or sets the costream_allow.
        /// </summary>
        /// <value>The costream_allow.</value>
        public string costream_allow { get; set; }
        /// <summary>
        /// Gets or sets the sharetext.
        /// </summary>
        /// <value>The sharetext.</value>
        public string sharetext { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_links_clickable.
        /// </summary>
        /// <value><c>true</c> if channel_links_clickable; otherwise, <c>false</c>.</value>
        public bool channel_links_clickable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_links_allowed.
        /// </summary>
        /// <value><c>true</c> if channel_links_allowed; otherwise, <c>false</c>.</value>
        public bool channel_links_allowed { get; set; }
        /// <summary>
        /// Gets or sets the channel_slowchat.
        /// </summary>
        /// <value>The channel_slowchat.</value>
        public int channel_slowchat { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_notify_follow.
        /// </summary>
        /// <value><c>true</c> if channel_notify_follow; otherwise, <c>false</c>.</value>
        public bool channel_notify_follow { get; set; }
        /// <summary>
        /// Gets or sets the channel_notify_followmessage.
        /// </summary>
        /// <value>The channel_notify_followmessage.</value>
        public string channel_notify_followmessage { get; set; }
        /// <summary>
        /// Gets or sets the channel_notify_hosted by.
        /// </summary>
        /// <value>The channel_notify_hosted by.</value>
        public string channel_notify_hostedBy { get; set; }
        /// <summary>
        /// Gets or sets the channel_notify_hosting.
        /// </summary>
        /// <value>The channel_notify_hosting.</value>
        public string channel_notify_hosting { get; set; }
        /// <summary>
        /// Gets or sets the channel_notify_subscribemessage.
        /// </summary>
        /// <value>The channel_notify_subscribemessage.</value>
        public string channel_notify_subscribemessage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_notify_subscribe.
        /// </summary>
        /// <value><c>true</c> if channel_notify_subscribe; otherwise, <c>false</c>.</value>
        public bool channel_notify_subscribe { get; set; }
        /// <summary>
        /// Gets or sets the channel_partner_submail.
        /// </summary>
        /// <value>The channel_partner_submail.</value>
        public string channel_partner_submail { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [channel_player_mute own].
        /// </summary>
        /// <value><c>true</c> if [channel_player_mute own]; otherwise, <c>false</c>.</value>
        public bool channel_player_muteOwn { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_tweet_enabled.
        /// </summary>
        /// <value><c>true</c> if channel_tweet_enabled; otherwise, <c>false</c>.</value>
        public bool channel_tweet_enabled { get; set; }
        /// <summary>
        /// Gets or sets the channel_tweet_body.
        /// </summary>
        /// <value>The channel_tweet_body.</value>
        public string channel_tweet_body { get; set; }
    }
}