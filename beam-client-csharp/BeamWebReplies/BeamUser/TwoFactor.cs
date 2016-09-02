// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="TwoFactor.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    /// Class TwoFactor.
    /// </summary>
    public class TwoFactor
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TwoFactor"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool enabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [codes viewed].
        /// </summary>
        /// <value><c>true</c> if [codes viewed]; otherwise, <c>false</c>.</value>
        public bool codesViewed { get; set; }
    }
}