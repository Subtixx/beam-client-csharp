// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamPartnershipApp.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using beam_client_csharp.BeamWebReplies.BeamChannel;

namespace beam_client_csharp.BeamWebReplies
{
    /// <summary>
    ///     Class BeamPartnershipApp.
    /// </summary>
    public class BeamPartnershipApp : BeamTimestamped
    {
        /// <summary>
        ///     The date of the next possible application, only set if status is denied.If the value is null while status is
        ///     denied, the channel is banned from re-applying.
        /// </summary>
        public string reapplies;

        /// <summary>
        ///     The reason of the denial, only set if status is denied.
        /// </summary>
        public string reason;

        /// <summary>
        ///     The status
        ///     string(applied, accepted, denied)
        /// </summary>
        public string status;
    }
}