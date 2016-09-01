// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="Data.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace beam_client_csharp.BeamEventMessages.ChatMessage
{
    /// <summary>
    /// Class Data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public int channel { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string id { get; set; }
        /// <summary>
        /// Gets or sets the user_name.
        /// </summary>
        /// <value>The user_name.</value>
        public string user_name { get; set; }
        /// <summary>
        /// Gets or sets the user_id.
        /// </summary>
        /// <value>The user_id.</value>
        public int user_id { get; set; }
        /// <summary>
        /// Gets or sets the user_roles.
        /// </summary>
        /// <value>The user_roles.</value>
        public List<string> user_roles { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public Message message { get; set; }
    }
}