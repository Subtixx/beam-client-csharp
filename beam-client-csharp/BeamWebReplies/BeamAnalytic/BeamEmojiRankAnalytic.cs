// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamEmojiRankAnalytic.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp
{
    /// <summary>
    ///     Class BeamEmojiRankAnalytic.
    /// </summary>
    public class BeamEmojiRankAnalytic
    {
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public string time { get; set; }

        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int count { get; set; }

        /// <summary>
        ///     Gets or sets the emoji.
        /// </summary>
        /// <value>The emoji.</value>
        public string emoji { get; set; }

        /// <summary>
        ///     Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public int channel { get; set; }
    }
}