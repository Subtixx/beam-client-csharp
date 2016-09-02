// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamPrivatePopulatedUser.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    ///     A fully populater user with channel, preferences, groups and private details.
    /// </summary>
    public class BeamPrivatePopulatedUser : BeamPrivateUser
    {
        /// <summary>
        ///     The users channel.
        /// </summary>
        public BeamChannel.BeamChannel channel;

        /// <summary>
        ///     The global user groups.
        /// </summary>
        public List<UserGroup> groups;

        /// <summary>
        ///     The preferences the user has.
        /// </summary>
        public UserPreferences preferences;

        /// <summary>
        ///     Two factor related data.
        /// </summary>
        public object twoFactor;
    }
}