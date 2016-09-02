// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="GameType.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    /// Class GameType.
    /// </summary>
    public class GameType : GameTypeSimple
    {
        /// <summary>
        /// The name of the parent type.
        /// </summary>
        public string parent;
        /// <summary>
        /// The description of the type.
        /// </summary>
        public string description;
        /// <summary>
        /// The source where the type has been imported from.
        /// </summary>
        public string source;
        /// <summary>
        /// Total amount of users watching this type of stream.
        /// </summary>
        public uint viewersCurrent;
        /// <summary>
        /// Amount of streams online with this type.
        /// </summary>
        public uint online;
    }
}