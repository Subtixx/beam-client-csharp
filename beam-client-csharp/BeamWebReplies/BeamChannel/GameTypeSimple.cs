// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="GameTypeSimple.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    ///     Base game type.
    /// </summary>
    public class GameTypeSimple
    {
        /// <summary>
        ///     The url to the types cover.
        ///     Nullable on beam
        /// </summary>
        public string coverUrl;

        /// <summary>
        ///     The unique ID of the game type.
        /// </summary>
        public uint id;

        /// <summary>
        ///     The name of the type.
        /// </summary>
        public string name;
    }
}