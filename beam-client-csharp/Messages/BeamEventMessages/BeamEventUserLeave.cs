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
        ///     The user data
        /// </summary>
        public BeamChatLeaveUser data;

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
	
	public class BeamChatLeaveUser
	{
        /// <summary>
        ///     The username
        /// </summary>
        public string username;
		
		/// <summary>
        ///     The user id
        /// </summary>
		public uint id;
	}
}