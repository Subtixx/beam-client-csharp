// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 09-02-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
// ***********************************************************************
// <copyright file="Resource.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace beam_client_csharp.BeamWebReplies.BeamChannel
{
    /// <summary>
    ///     Class Resource.
    /// </summary>
    public class Resource
    {
        /// <summary>
        ///     The unique ID of the resource.
        /// </summary>
        /// <value>The identifier.</value>
        public uint id { get; set; }

        /// <summary>
        ///     The type of the resource.
        /// </summary>
        /// <value>The type.</value>
        public string type { get; set; }

        /// <summary>
        ///     Id linking to the parent object.
        /// </summary>
        /// <value>The relid.</value>
        public uint relid { get; set; }

        /// <summary>
        ///     The url of the resource.
        /// </summary>
        /// <value>The URL.</value>
        public string url { get; set; }

        /// <summary>
        ///     The storage type of the resource.
        /// </summary>
        /// <value>The store.</value>
        public string store { get; set; }

        /// <summary>
        ///     Relative url to the resource.
        /// </summary>
        /// <value>The remote path.</value>
        public string remotePath { get; set; }

        /// <summary>
        ///     Additional resource information.
        ///     Nullable on beam
        /// </summary>
        /// <value>The meta.</value>
        public object meta { get; set; }
    }
}