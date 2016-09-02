﻿// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamReplyMessage.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace beam_client_csharp.Messages
{
    /// <summary>
    ///     A reply is received from the server in response to a <see cref="BeamMethodMessage">Method packet</see>.
    /// </summary>
    public class BeamReplyMessage : BeamMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamReplyMessage" /> class.
        /// </summary>
        public BeamReplyMessage()
        {
            type = "reply";
        }

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        /// <value>If an error has not occured null, otherwise an error message.</value>
        public string error { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>Associated event data - may be of any type, specific to the event.</value>
        public Dictionary<string, object> data { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     Must be a unique numeric ID. The ID will match the ID sent in the Method packet that this reply is in response
        ///     to.
        /// </value>
        public override int id { get; set; }
    }
}