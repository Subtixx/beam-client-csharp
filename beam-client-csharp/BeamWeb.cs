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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using beam_client_csharp.Utils;
using Newtonsoft.Json;

namespace beam_client_csharp
{
    /// <summary>
    ///     Class BeamWeb.
    /// </summary>
    public class BeamWeb
    {
        /// <summary>
        ///     The _cookie container
        /// </summary>
        private CookieContainer _cookieContainer;

        /// <summary>
        ///     Authenticates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;BeamUser.BeamUser&gt;.</returns>
        public async Task<BeamUser.BeamUser> Authenticate(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password}
            };

            var loginResult = await POST_Api("users/login", values);
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResult);
            return result.ContainsKey("statusCode") ? null : JsonConvert.DeserializeObject<BeamUser.BeamUser>(loginResult);
        }

        /// <summary>
        ///     Get information about a channel chat.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChatInfo&gt;.</returns>
        public async Task<BeamChatInfo> GetChatInformation(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChatInfo>(await GET_Api($"chats/{channelId}"));
        }

        /// <summary>
        ///     Gets the achievements.
        /// </summary>
        /// <returns>Task&lt;List&lt;BeamAchievement&gt;&gt;.</returns>
        public async Task<List<BeamAchievement>> GetAchievements()
        {
            return JsonConvert.DeserializeObject<List<BeamAchievement>>(await GET_Api("achievements"));
        }

        /// <summary>
        ///     Creates the announcement.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        [Obsolete("This method is not implemented", false)]
        public void CreateAnnouncement()
        {
            throw new NotImplementedException();
            /*if (_cookieContainer == null) _cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>());

                var response = await client.PostAsync("https://beam.pro/api/v1/announcements", content);
                var responseString = await response.Content.ReadAsStringAsync();

#if DEBUG
                Console.WriteLine($"CreateAnnouncement result: {responseString}");
#endif
                //return JsonConvert.DeserializeObject<List<BeamAchievement>>(responseString);
            }*/
        }

        #region Channels

        public async Task<List<BeamChannel.BeamChannel>> ListChannels(int page = 1, int limit = 50, string where = "",
            string fields = "", string order = "")
        {
            return JsonConvert.DeserializeObject<List<BeamChannel.BeamChannel>>(await GET_Api($"channels?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        public async Task<BeamChannel.BeamChannel> GetChannel(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChannel.BeamChannel>(await GET_Api($"channels/{channelId}"));
        }

        #endregion

        private Dictionary<string, int> remainingAPICalls = new Dictionary<string, int>();
        private Dictionary<string, int> totalAllowedAPICalls = new Dictionary<string, int>();
        private Dictionary<string, DateTime> APICallReset = new Dictionary<string, DateTime>();

        private async Task<string> GET_Api(string subUrl)
        {
            if (remainingAPICalls.ContainsKey(subUrl))
            {
                if (remainingAPICalls[subUrl] == 0)
                {
#if DEBUG
                    Console.WriteLine("API Rate limit!");
#endif
                    return "";
                }
            }

            if (_cookieContainer == null) _cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler { CookieContainer = _cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var response = await client.GetAsync($"https://beam.pro/api/v1/{subUrl}");
                return await Call_API(response, subUrl);
            }
        }

        private async Task<string> POST_Api(string subUrl, Dictionary<string, string> values)
        {
            if (remainingAPICalls.ContainsKey(subUrl))
            {
                if (remainingAPICalls[subUrl] == 0)
                {
#if DEBUG
                    Console.WriteLine("API Rate limit!");
#endif
                    return "";
                }
            }

            if (_cookieContainer == null) _cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = _cookieContainer})
            using (var client = new HttpClient(handler))
            {
                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync($"https://beam.pro/api/v1/{subUrl}", content);
                return await Call_API(response, subUrl);
            }
        }

        private async Task<string> Call_API(HttpResponseMessage response, string subUrl)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            string x_ratelimit_remaining = response.Headers.GetValues("x-ratelimit-remaining").FirstOrDefault();
            string x_ratelimit = response.Headers.GetValues("x-rate-limit").FirstOrDefault();
            string x_ratelimit_reset = response.Headers.GetValues("x-ratelimit-reset").FirstOrDefault();

            int remaining = string.IsNullOrEmpty(x_ratelimit_remaining) ? 0 : int.Parse(x_ratelimit_remaining);
            if (remainingAPICalls.ContainsKey(subUrl))
                remainingAPICalls[subUrl] = remaining;
            else
                remainingAPICalls.Add(subUrl, remaining);

            DateTime reset = string.IsNullOrEmpty(x_ratelimit_reset)
                    ? new DateTime()
                    : UnixTimestamp.DateTimeFromUnixTimestampMillis(long.Parse(x_ratelimit_reset));
            if (APICallReset.ContainsKey(subUrl))
                APICallReset[subUrl] = reset;
            else
                APICallReset.Add(subUrl, reset);

            // I don't know why we should every want to access this but.. Hey :)
            int total = string.IsNullOrEmpty(x_ratelimit) ? 0 : int.Parse(x_ratelimit);
            if (totalAllowedAPICalls.ContainsKey(subUrl))
                totalAllowedAPICalls[subUrl] = total;
            else
                totalAllowedAPICalls.Add(subUrl, total);

#if DEBUG
            Console.WriteLine($"{subUrl} result: {responseString}");
#endif
            return responseString;
        }
    }
}