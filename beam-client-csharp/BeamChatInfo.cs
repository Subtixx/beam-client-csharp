// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamChatInfo.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace beam_client_csharp
{
    /// <summary>
    /// Class BeamChatInfo.
    /// </summary>
    public class BeamChatInfo
    {
        /// <summary>
        /// Gets or sets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        public List<string> endpoints { get; set; }
        /// <summary>
        /// Gets or sets the authkey.
        /// </summary>
        /// <value>The authkey.</value>
        public string authkey { get; set; }
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public List<string> permissions { get; set; }
    }
}