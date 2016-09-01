// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="Social.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace beam_client_csharp.BeamUser
{
    /// <summary>
    /// Class Social.
    /// </summary>
    public class Social
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public string player { get; set; }
        /// <summary>
        /// Gets or sets the twitter.
        /// </summary>
        /// <value>The twitter.</value>
        public string twitter { get; set; }
        /// <summary>
        /// Gets or sets the youtube.
        /// </summary>
        /// <value>The youtube.</value>
        public string youtube { get; set; }
        /// <summary>
        /// Gets or sets the verified.
        /// </summary>
        /// <value>The verified.</value>
        public List<string> verified { get; set; }
        /// <summary>
        /// Gets or sets the discord.
        /// </summary>
        /// <value>The discord.</value>
        public string discord { get; set; }
    }
}