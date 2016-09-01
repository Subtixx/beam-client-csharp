// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamSubscription.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamWebReplies.BeamAnalytic
{
    /// <summary>
    /// Class BeamSubscription.
    /// </summary>
    public class BeamSubscriptionAnalytic
    {
        /// <summary>
        /// The channel
        /// </summary>
        public uint channel;
        /// <summary>
        /// The total
        /// </summary>
        public uint total;

        /// <summary>
        /// The delta
        /// </summary>
        public int delta;
        /// <summary>
        /// The user
        /// </summary>
        public uint user;
        /// <summary>
        /// The time
        /// </summary>
        public string time;
    }
}