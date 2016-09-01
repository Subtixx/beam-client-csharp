// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamMessage.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.Messages
{
    /// <summary>
    /// Class BeamMessage.
    /// </summary>
    public class BeamMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BeamMessage"/> class.
        /// </summary>
        public BeamMessage()
        {
            type = "unknown";
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string type { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual int id { get; set; }
    }
}