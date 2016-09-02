// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="BeamEventUserLeave.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.Messages.BeamEventMessages
{
    /// <summary>
    ///     Class BeamEventUserLeave.
    /// </summary>
    public class BeamEventUserLeave : BeamMessage
    {
        /// <summary>
        ///     The username
        /// </summary>
        public string username;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BeamEventUserLeave" /> class.
        /// </summary>
        public BeamEventUserLeave()
        {
            type = "event";
            @event = "UserLeave";
        }

        /// <summary>
        ///     The event
        /// </summary>
        /// <value>The event.</value>
        public string @event { get; set; }
    }
}