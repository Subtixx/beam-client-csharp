// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="Group.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    /// Class Group.
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The unique ID of the group.</value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the group.</value>
        public string name { get; set; }
    }
}