// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamAchievement.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp
{
    /// <summary>
    ///     Class BeamAchievement.
    /// </summary>
    public class BeamAchievement
    {
        /// <summary>
        ///     Gets or sets the slug.
        /// </summary>
        /// <value>The slug.</value>
        public string slug { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }

        /// <summary>
        ///     Gets or sets the sparks.
        /// </summary>
        /// <value>The sparks.</value>
        public int sparks { get; set; }
    }
}