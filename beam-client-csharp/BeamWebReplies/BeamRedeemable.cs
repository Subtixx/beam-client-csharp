// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamRedeemable.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using beam_client_csharp.BeamWebReplies.BeamChannel;

namespace beam_client_csharp.BeamWebReplies
{
    /// <summary>
    /// Class BeamRedeemable.
    /// </summary>
    public class BeamRedeemable : BeamTimestamped
    {
        /// <summary>
        /// The unique ID of the redeemable.
        /// </summary>
        public uint id;
        /// <summary>
        /// The ID of the owning user.
        /// </summary>
        public uint ownerId;
        /// <summary>
        /// string(unpaid, paid, redeemed)
        /// </summary>
        public string state;
        /// <summary>
        /// string(pro)
        /// </summary>
        public string type;
        /// <summary>
        /// The redeem code.
        /// </summary>
        public string code;
        /// <summary>
        /// The ID of the user that used the code.
        /// </summary>
        public uint redeemedById;
        /// <summary>
        /// The time the item was redeemed.
        /// </summary>
        public string redeemedAt;
    }
}