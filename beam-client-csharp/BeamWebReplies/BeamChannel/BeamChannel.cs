// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamChannel.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    /// Class BeamChannel.
    /// </summary>
    public class BeamChannel
    {
        /// <summary>
        /// The target audience of the channel.
        /// string(family, teen, 18+)
        /// </summary>
        public string audience;

        /// <summary>
        /// The ID of the cover resource.
        /// </summary>
        public uint? coverId;

        /// <summary>
        /// The description of the channel, can contain HTML.
        /// </summary>
        public string description;

        /// <summary>
        /// Indicates if the channel is featured.
        /// </summary>
        public bool featured;

        /// <summary>
        /// The FTL stream id.
        /// This is actually specified as uint. But has a value of -1 when not using FTL
        /// </summary>
        public int ftl;

        /// <summary>
        /// Indicates if the channel has vod saved.
        /// </summary>
        public bool hasVod;

        /// <summary>
        /// The unique ID of the channel.
        /// </summary>
        public uint id;

        /// <summary>
        /// Indicates if that channel is interactive.
        /// </summary>
        public bool interactive;

        /// <summary>
        /// The ID of the interactive game used.
        /// </summary>
        public uint? interactiveGameId;

        /// <summary>
        /// ISO 639 language id.
        /// Nullable on Beam
        /// </summary>
        public string languageId;


        /// <summary>
        /// The title of the channel.
        /// </summary>
        public string name;

        /// <summary>
        /// Amount of followers.
        /// </summary>
        public uint numFollower;

        /// <summary>
        /// Indicates if the channel is active.
        /// </summary>
        public bool online;

        /// <summary>
        /// Indicates if the channel is partnered.
        /// </summary>
        public bool partnered;

        /// <summary>
        /// Indicates if the channel is suspended.
        /// </summary>
        public bool suspended;

        /// <summary>
        /// The ID of the interactive game used.
        /// </summary>
        public uint? tetrisGameId;

        /// <summary>
        /// The resource ID of the thumbnail.
        /// </summary>
        public uint? thumbnailId;

        /// <summary>
        /// The name and url of the channel.
        /// </summary>
        public string token;

        /// <summary>
        /// The id of the transcoding profile.
        /// </summary>
        public uint? transcodingProfileId;

        /// <summary>
        /// The ID of the game type.
        /// </summary>
        public uint? typeId;

        /// <summary>
        /// The ID of the user owning the channel.
        /// </summary>
        public uint userId;

        /// <summary>
        /// Amount of current viewers.
        /// </summary>
        public uint viewersCurrent;

        /// <summary>
        /// Amount of unique viewers that ever viewed this channel.
        /// </summary>
        public uint viewersTotal;

        /// <summary>
        /// Indicates if the channel has vod recording enabled.
        /// </summary>
        public bool vodsEnabled;
    }
}