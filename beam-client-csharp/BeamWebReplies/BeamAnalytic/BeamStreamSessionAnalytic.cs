// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamStreamSession.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamAnalytic
{
    /// <summary>
    ///     Class BeamStreamSession.
    /// </summary>
    public class BeamStreamSessionAnalytic
    {
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public string time { get; set; }

        /// <summary>
        ///     Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public int channel { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public object duration { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BeamStreamSession" /> is online.
        /// </summary>
        /// <value><c>true</c> if online; otherwise, <c>false</c>.</value>
        public bool online { get; set; }
    }
}