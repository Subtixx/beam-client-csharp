// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-01-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="Type.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace beam_client_csharp.BeamChannel
{
    /// <summary>
    /// Class Type.
    /// </summary>
    public class Type
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public string parent { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string source { get; set; }
        /// <summary>
        /// Gets or sets the viewers current.
        /// </summary>
        /// <value>The viewers current.</value>
        public int viewersCurrent { get; set; }
        /// <summary>
        /// Gets or sets the cover URL.
        /// </summary>
        /// <value>The cover URL.</value>
        public string coverUrl { get; set; }
        /// <summary>
        /// Gets or sets the online.
        /// </summary>
        /// <value>The online.</value>
        public int online { get; set; }
    }
}