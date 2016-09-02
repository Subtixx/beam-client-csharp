// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="ChannelPreferences.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    ///     Class ChannelPreferences.
    /// </summary>
    public class ChannelPreferences
    {
        /// <summary>
        ///     Specified whether links are allowed in the chat.
        ///     Nullable
        /// </summary>
        public bool channel_links_allowed;

        /// <summary>
        ///     Specifies whether links are clickable in the chat.
        /// </summary>
        public bool channel_links_clickable;

        /// <summary>
        ///     Indicated whether a notification should be shown upon follow.
        /// </summary>
        public bool channel_notify_follow;

        /// <summary>
        ///     The message to be used when a user followed the channel.
        ///     The template parameter "%USER%" will be replaced with the followers name.
        /// </summary>
        public string channel_notify_followmessage;

        /// <summary>
        ///     Indicates whether a notification should be shown upon subscription.
        /// </summary>
        public bool channel_notify_subscribe;

        /// <summary>
        ///     The message to be used when a user subscribed to the channel.
        ///     The template parameter %USER% will be replaced with the subscribers name.
        /// </summary>
        public string channel_notify_subscribemessage;

        /// <summary>
        ///     Indicates whether to mute when the streamer opens his own stream.
        /// </summary>
        public bool channel_partner_muteOwn;

        /// <summary>
        ///     The text to be added to the subscription email.
        /// </summary>
        public string channel_partner_submail;

        /// <summary>
        ///     Interval required between each chat message.
        /// </summary>
        public int channel_slowchat;

        /// <summary>
        ///     The message to be used when a user tweets about the channel.The template parameter %URL% will be replaced with the
        ///     share url.
        /// </summary>
        public string channel_tweet_body;

        /// <summary>
        ///     Indicates whether the tweet button should be shown.
        /// </summary>
        public bool channel_tweet_enabled;

        /// <summary>
        ///     The costream_allow
        /// </summary>
        public string costream_allow;

        /// <summary>
        ///     The text used when sharing the stream.
        ///     The template parameter %URL% will be replaced with the share url.
        ///     The template parameter %USER% will be replaced with the sharers name.
        ///     Nullable
        /// </summary>
        public string sharetext;
    }
}