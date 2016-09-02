// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamPrivateUser.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beam_client_csharp.BeamWebReplies.BeamUser
{
    /// <summary>
    /// A fully populater user with channel, preferences, groups and private details.
    /// </summary>
    public class BeamPrivateUser : BeamUser
    {
        /// <summary>
        /// The users email address.
        /// </summary>
        public string email;
        /// <summary>
        /// The users password.
        /// minLength: 4
        /// </summary>
        public string password;
    }
}
