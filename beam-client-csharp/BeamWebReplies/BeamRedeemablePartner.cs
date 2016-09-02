using beam_client_csharp.BeamWebReplies.BeamChannel;

namespace beam_client_csharp.BeamWebReplies
{
    public class BeamRedeemablePartner : BeamTimestamped
    {
        /// <summary>
        ///     The unique ID of the channel.
        /// </summary>
        public uint id;

        /// <summary>
        ///     The id of the related partner.
        /// </summary>
        public uint partnerId;

        /// <summary>
        ///     The id of the related redeemable.
        /// </summary>
        public uint redeemableId;
    }
}