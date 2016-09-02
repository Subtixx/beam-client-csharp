// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="Preferences.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    ///     Class Preferences.
    /// </summary>
    public class UserPreferences
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_sounds_html5.
        /// </summary>
        /// <value><c>true</c> if chat_sounds_html5; otherwise, <c>false</c>.</value>
        public bool chat_sounds_html5 { get; set; }

        /// <summary>
        ///     Gets or sets the chat_sounds_play.
        /// </summary>
        /// <value>The chat_sounds_play.</value>
        public string chat_sounds_play { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_whispers.
        /// </summary>
        /// <value><c>true</c> if chat_whispers; otherwise, <c>false</c>.</value>
        public bool chat_whispers { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_timestamps.
        /// </summary>
        /// <value><c>true</c> if chat_timestamps; otherwise, <c>false</c>.</value>
        public bool chat_timestamps { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_chromakey.
        /// </summary>
        /// <value><c>true</c> if chat_chromakey; otherwise, <c>false</c>.</value>
        public bool chat_chromakey { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_tagging.
        /// </summary>
        /// <value><c>true</c> if chat_tagging; otherwise, <c>false</c>.</value>
        public bool chat_tagging { get; set; }

        /// <summary>
        ///     Gets or sets the chat_sounds_volume.
        /// </summary>
        /// <value>The chat_sounds_volume.</value>
        public int chat_sounds_volume { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserPreferences" /> is chat_colors.
        /// </summary>
        /// <value><c>true</c> if chat_colors; otherwise, <c>false</c>.</value>
        public bool chat_colors { get; set; }

        /*
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is chat_lurkmode.
        /// </summary>
        /// <value><c>true</c> if chat_lurkmode; otherwise, <c>false</c>.</value>
        public bool chat_lurkmode { get; set; }
        /// <summary>
        /// Gets or sets the channel_notifications.
        /// </summary>
        /// <value>The channel_notifications.</value>
        public ChannelNotifications channel_notifications { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Preferences"/> is channel_mature_allowed.
        /// </summary>
        /// <value><c>true</c> if channel_mature_allowed; otherwise, <c>false</c>.</value>
        public bool channel_mature_allowed { get; set; }
        /// <summary>
        /// Gets or sets the channel_player.
        /// </summary>
        /// <value>The channel_player.</value>
        public ChannelPlayer channel_player { get; set; }
        */
    }
}