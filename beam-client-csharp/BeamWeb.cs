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
using beam_client_csharp.BeamWebReplies.BeamAnalytic;
using beam_client_csharp.Utils;
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
        /// The _CSRF token
        /// </summary>
        private string _csrfToken;

        /// <summary>
        /// The API call reset
        /// </summary>
        private readonly Dictionary<string, DateTime> _apiCallReset = new Dictionary<string, DateTime>();

        /// <summary>
        /// The remaining API calls
        /// </summary>
        private readonly Dictionary<string, int> _remainingApiCalls = new Dictionary<string, int>();

        /// <summary>
        /// The total allowed API calls
        /// </summary>
        private readonly Dictionary<string, int> _totalAllowedApiCalls = new Dictionary<string, int>();

        /// <summary>
        /// Authenticates the specified username.
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

            var loginResult = await Call_API("users/login", values, HttpMethod.Post);
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResult);
            return result.ContainsKey("statusCode")
                ? null
                : JsonConvert.DeserializeObject<BeamUser.BeamUser>(loginResult);
        }

        /// <summary>
        /// Get information about a channel chat.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChatInfo&gt;.</returns>
        public async Task<BeamChatInfo> GetChatInformation(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChatInfo>(await Call_API($"chats/{channelId}"));
        }

        /// <summary>
        /// Gets the achievements.
        /// </summary>
        /// <returns>Task&lt;List&lt;BeamAchievement&gt;&gt;.</returns>
        public async Task<List<BeamAchievement>> GetAchievements()
        {
            return JsonConvert.DeserializeObject<List<BeamAchievement>>(await Call_API("achievements"));
        }

        /// <summary>
        /// Creates the announcement.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
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
        /// Call_s the API.
        /// </summary>
        /// <param name="subUrl">The sub URL.</param>
        /// <param name="values">The values.</param>
        /// <param name="method">The method.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private async Task<string> Call_API(string subUrl, Dictionary<string, string> values = null, HttpMethod method = null)
        {
            if (method == null)
                method = HttpMethod.Get;
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
                if (method == HttpMethod.Post)
                {
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>());
                    if (values != null)
                        content = new FormUrlEncodedContent(values);

                    response = await client.PostAsync($"https://beam.pro/api/v1/{subUrl}", content);
                }
                else if (method == HttpMethod.Put)
                {
                    response = await client.PutAsync($"https://beam.pro/api/v1/{subUrl}", new MultipartFormDataContent());
                }
                else if(method == HttpMethod.Get)
                    response = await client.GetAsync($"https://beam.pro/api/v1/{subUrl}");
                else
                {
                    throw new NotImplementedException($"Method {method.Method} is not implemented!");
                }

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
                    switch (response.StatusCode)
                    {
                        default:
                            Console.WriteLine(
                            $"Error occurred, the status code is: {response.StatusCode}\nPlease contact the developer!");
#if !DEBUG
                            throw new Exception($"Error occurred, the status code is: {response.StatusCode}\nPlease contact the developer!");
#endif
                            break;
                        case (HttpStatusCode)429: // API Rate limit
                            // Prevent calling this page again.
                            if (_remainingApiCalls.ContainsKey(subUrl)) // I dunno but I feel like I don't need to check this as the prev. one is already inserting..
                                _remainingApiCalls[subUrl] = 0;
                            else
                                _remainingApiCalls.Add(subUrl, 0);
                            Console.WriteLine("API Rate limit hit. Preventing further calling!");
                            break;
                        case (HttpStatusCode)461: // CSRF Missing
                            // Retry with CSRF, maybe bad because it's recursive call?
                            _csrfToken = response.Headers.GetValues("X-CSRF-Token").FirstOrDefault();
                            return await Call_API(subUrl, values, method);
                        
                        // Bad or expired token. This can happen if the user or Beam revoked or expired an access token.
                        // To fix, you should re- authenticate the user.
                        case (HttpStatusCode)401: // Unauthorized / Login missing
                            break;

                        //Bad OAuth request (wrong consumer key, bad nonce, expired timestamp...).
                        // Unfortunately, re-authenticating the user won't help here.
                        //Can also indicate a missing permission for the action attempted.
                        //case (HttpStatusCode)403: // Forbidden
                    }
                }
                return responseString;
            }
        }

        #region Channels

        /// <summary>
        /// Lists the channels.
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
            if (limit > 50)
                limit = 50;
            return
                JsonConvert.DeserializeObject<List<BeamChannel.BeamChannel>>(
                    await Call_API($"channels?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChannel.BeamChannel&gt;.</returns>
        public async Task<BeamChannel.BeamChannel> GetChannel(int channelId)
        {
            return JsonConvert.DeserializeObject<BeamChannel.BeamChannel>(await Call_API($"channels/{channelId}"));
        }

        /// <summary>
        /// Gets the channel details.
        /// </summary>
        /// <param name="channelIdOrToken">The channel identifier or token.</param>
        /// <returns>Task&lt;BeamChannel.BeamChannel&gt;.</returns>
        public async Task<BeamChannel.BeamChannel> GetChannelDetails(int channelIdOrToken)
        {
            return JsonConvert.DeserializeObject<BeamChannel.BeamChannel>(await Call_API($"channels/{channelIdOrToken}/details"));
        }
        #endregion

        #region Analytics

        /// <summary>
        /// Gets the viewer count.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From (isoDate).</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamViewerCount&gt;&gt;.</returns>
        public async Task<List<BeamViewerAnalytic>> GetViewers(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamViewerAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/viewers?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the viewers metrics.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamViewerMetricAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamViewerMetricAnalytic>> GetViewersMetrics(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamViewerMetricAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/viewersMetrics?from={from}&to={to}"));
        }
        /// <summary>
        /// Gets the stream sessions.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamStreamSessionAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamStreamSessionAnalytic>> GetStreamSessions(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamStreamSessionAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/streamSessions?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the stream hosts.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamStreamHostAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamStreamHostAnalytic>> GetStreamHosts(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamStreamHostAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/streamHosts?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamSubscriptionAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamSubscriptionAnalytic>> GetSubscriptions(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamSubscriptionAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/subscriptions?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the followers.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamFollowerAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamFollowerAnalytic>> GetFollowers(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamFollowerAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/followers?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the sparks spent.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamFollowerAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamFollowerAnalytic>> GetSparksSpent(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamFollowerAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/sparkSpent?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the emoji ranks.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamEmojiRankAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamEmojiRankAnalytic>> GetEmojiRanks(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamEmojiRankAnalytic>> (await Call_API($"channels/{channelId}/analytics/tsdb/emojiUsageRanks?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the emoji usage.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamEmojiRankAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamEmojiRankAnalytic>> GetEmojiUsage(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamEmojiRankAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/emojiUsage?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the game ranks.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamGameRankAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamGameRankAnalytic>> GetGameRanks(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamGameRankAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/gameRanks?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the global game ranks.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamGameRankAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamGameRankAnalytic>> GetGlobalGameRanks(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamGameRankAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/gameRanksGlobal?from={from}&to={to}"));
        }

        /// <summary>
        /// Gets the sub revenue.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>Task&lt;List&lt;BeamSubRevenueAnalytic&gt;&gt;.</returns>
        public async Task<List<BeamSubRevenueAnalytic>> GetSubRevenue(int channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamSubRevenueAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/subRevenue?from={from}&to={to}"));
        }
        #endregion

        #region Misc Actions

        public void SetBadge(int channelId)
        {
            // Needs api to upload things.
            // See https://dev.beam.pro/rest.html#channels__channelId__badge_post
            // And https://dev.beam.pro/rest.html#ExpandedChannel
            // And https://github.com/Subtixx/beam-client-csharp/issues/4
            throw new NotImplementedException();
            /*Dictionary<string, string> values = new Dictionary<string, string>();
            await Call_API($"channels/{channelId}/badge", values);*/
        }

        public async Task<List<BeamChannel.BeamChannel>> ListFollowers(int channelId, int page = 1, int limit = 25, string where = "",
            string fields = "", string order = "")
        {
            if (limit > 25)
                limit = 25;
            //follow 
            return
                JsonConvert.DeserializeObject<List<BeamChannel.BeamChannel>>(
                    await Call_API($"channels/{channelId}/follow?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        /*public async Task<> FollowChannel(int channelId)
        {
            
        }*/
        #endregion
    }
}