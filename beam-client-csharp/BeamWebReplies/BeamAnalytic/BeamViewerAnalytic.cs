// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamViewer.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamWebReplies.BeamAnalytic
{
    /// <summary>
    /// Class BeamViewerCount.
    /// </summary>
    public class BeamViewerAnalytic
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public string time { get; set; }
        /// <summary>
        /// Gets or sets the anon.
        /// </summary>
        /// <value>The anon.</value>
        public int anon { get; set; }
        /// <summary>
        /// Gets or sets the authed.
        /// </summary>
        /// <value>The authed.</value>
        public int authed { get; set; }
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public int channel { get; set; }
    }
}