// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-01-2016
// ***********************************************************************
// <copyright file="BeamWeb.cs" company="Flying Penguin">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace beam_client_csharp
{
    /// <summary>
    /// Class BeamWeb.
    /// </summary>
    public class BeamWeb
    {
        /// <summary>
        /// The _cookie container
        /// </summary>
        private CookieContainer _cookieContainer;

        /// <summary>
        /// Authenticates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;BeamUser.BeamUser&gt;.</returns>
        public async Task<BeamUser.BeamUser> Authenticate(string username, string password)
        {
            _cookieContainer = new CookieContainer();

            using (var handler = new HttpClientHandler {CookieContainer = _cookieContainer})
            using (var client = new HttpClient(handler))
            {
                var values = new Dictionary<string, string>
                {
                    {"username", username},
                    {"password", password}
                };
                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://beam.pro/api/v1/users/login", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
                if (result.ContainsKey("statusCode"))
                    return null;

                return JsonConvert.DeserializeObject<BeamUser.BeamUser>(responseString);
            }
        }

        /// <summary>
        /// Chats the information.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChatInfo&gt;.</returns>
        public async Task<BeamChatInfo> ChatInfo(int channelId)
        {
            if (_cookieContainer == null) _cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = _cookieContainer})
            using (var client = new HttpClient(handler))
            {
                var response = await client.GetAsync("https://beam.pro/api/v1/chats/" + channelId);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);

                return JsonConvert.DeserializeObject<BeamChatInfo>(responseString);
            }
        }
    }
}