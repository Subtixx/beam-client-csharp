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

        private string _csrfToken;

        /// <summary>
        ///     The API call reset
        /// </summary>
        private readonly Dictionary<string, DateTime> _apiCallReset = new Dictionary<string, DateTime>();

        /// <summary>
        ///     The remaining API calls
        /// </summary>
        private readonly Dictionary<string, int> _remainingApiCalls = new Dictionary<string, int>();

        /// <summary>
        ///     The total allowed API calls
        /// </summary>
        private readonly Dictionary<string, int> _totalAllowedApiCalls = new Dictionary<string, int>();

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

            var loginResult = await Call_API("users/login", values);
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResult);
            return result.ContainsKey("statusCode")
                ? null
                : JsonConvert.DeserializeObject<BeamUser.BeamUser>(loginResult);
        }

        /// <summary>
        ///     Get information about a channel chat.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChatInfo&gt;.</returns>
        public async Task<BeamChatInfo> GetChatInformation(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChatInfo>(await Call_API($"chats/{channelId}"));
        }

        /// <summary>
        ///     Gets the achievements.
        /// </summary>
        /// <returns>Task&lt;List&lt;BeamAchievement&gt;&gt;.</returns>
        public async Task<List<BeamAchievement>> GetAchievements()
        {
            return JsonConvert.DeserializeObject<List<BeamAchievement>>(await Call_API("achievements"));
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

        /// <summary>
        ///     Call_s the API.
        /// </summary>
        /// <param name="subUrl">The sub URL.</param>
        /// <param name="values">The values.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private async Task<string> Call_API(string subUrl, Dictionary<string, string> values = null)
        {
            if (_remainingApiCalls.ContainsKey(subUrl))
            {
                if (_remainingApiCalls[subUrl] == 0)
                {
                    // Check for reset time
                    if (_apiCallReset[subUrl] > new DateTime())
                    {
#if DEBUG
                        Console.WriteLine("API Rate limit!");
#endif
                        return "";
                    }
                    _remainingApiCalls.Remove(subUrl);
                }
            }

            if (_cookieContainer == null) _cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = _cookieContainer})
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("X-CSRF-Token", _csrfToken);
                HttpResponseMessage response;
                if (values != null)
                {
                    var content = new FormUrlEncodedContent(values);

                    response = await client.PostAsync($"https://beam.pro/api/v1/{subUrl}", content);
                }
                else
                    response = await client.GetAsync($"https://beam.pro/api/v1/{subUrl}");

                var responseString = await response.Content.ReadAsStringAsync();

                var xRatelimitRemaining = response.Headers.GetValues("x-ratelimit-remaining").FirstOrDefault();
                var xRatelimit = response.Headers.GetValues("x-rate-limit").FirstOrDefault();
                var xRatelimitReset = response.Headers.GetValues("x-ratelimit-reset").FirstOrDefault();

                var remaining = string.IsNullOrEmpty(xRatelimitRemaining) ? 0 : int.Parse(xRatelimitRemaining);
                if (_remainingApiCalls.ContainsKey(subUrl))
                    _remainingApiCalls[subUrl] = remaining;
                else
                    _remainingApiCalls.Add(subUrl, remaining);

                var reset = string.IsNullOrEmpty(xRatelimitReset)
                    ? new DateTime()
                    : UnixTimestamp.DateTimeFromUnixTimestampMillis(long.Parse(xRatelimitReset));
                if (_apiCallReset.ContainsKey(subUrl))
                    _apiCallReset[subUrl] = reset;
                else
                    _apiCallReset.Add(subUrl, reset);

                // I don't know why we should every want to access this but.. Hey :)
                var total = string.IsNullOrEmpty(xRatelimit) ? 0 : int.Parse(xRatelimit);
                if (_totalAllowedApiCalls.ContainsKey(subUrl))
                    _totalAllowedApiCalls[subUrl] = total;
                else
                    _totalAllowedApiCalls.Add(subUrl, total);

#if DEBUG
                Console.WriteLine($"{subUrl} result: {responseString}");
#endif

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == (HttpStatusCode)429) // API Rate limit
                    {
                        // Prevent calling this page again.
                        if (_remainingApiCalls.ContainsKey(subUrl))
                            _remainingApiCalls[subUrl] = 0;
                        else
                            _remainingApiCalls.Add(subUrl, 0);
                        Console.WriteLine("API Rate limit hit. Preventing further calling!");
                    }
                    else if (response.StatusCode == (HttpStatusCode)461) // CSRF Missing
                    {
                        _csrfToken = response.Headers.GetValues("X-CSRF-Token").FirstOrDefault();
                        return await Call_API(subUrl, values);
                        // Retry with CSRF, maybe bad because it's recursive call?
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Error occurred, the status code is: {response.StatusCode}\nPlease contact the developer!");
#if !DEBUG
                        throw new Exception($"Error occurred, the status code is: {response.StatusCode}\nPlease contact the developer!");
#endif
                    }
                }
                return responseString;
            }
        }

        #region Channels

        /// <summary>
        ///     Lists the channels.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="where">The where.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="order">The order.</param>
        /// <returns>Task&lt;List&lt;BeamChannel.BeamChannel&gt;&gt;.</returns>
        public async Task<List<BeamChannel.BeamChannel>> ListChannels(int page = 1, int limit = 50, string where = "",
            string fields = "", string order = "")
        {
            return
                JsonConvert.DeserializeObject<List<BeamChannel.BeamChannel>>(
                    await Call_API($"channels?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        /// <summary>
        ///     Gets the channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChannel.BeamChannel&gt;.</returns>
        public async Task<BeamChannel.BeamChannel> GetChannel(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChannel.BeamChannel>(await Call_API($"channels/{channelId}"));
        }

        #endregion
    }
}