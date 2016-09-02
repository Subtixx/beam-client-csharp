// ***********************************************************************
// Assembly         : beam_client_csharp
// Author           : Subtixx
// Created          : 08-31-2016
//
// Last Modified By : Subtixx
// Last Modified On : 09-02-2016
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
using System.Text;
using System.Threading.Tasks;
using beam_client_csharp.BeamWebReplies;
using beam_client_csharp.BeamWebReplies.BeamAnalytic;
using beam_client_csharp.BeamWebReplies.BeamChannel;
using beam_client_csharp.BeamWebReplies.BeamUser;
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
        public async Task<BeamPrivatePopulatedUser> Authenticate(string username, string password)
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
                : JsonConvert.DeserializeObject<BeamPrivatePopulatedUser>(loginResult);
        }

        /// <summary>
        /// Get information about a channel chat.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChatInfo&gt;.</returns>
        public async Task<BeamChatInfo> GetChatInformation(uint channelId)
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
        /// <exception cref="NotImplementedException"></exception>
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
        /// Call_s the API.
        /// </summary>
        /// <param name="subUrl">The sub URL.</param>
        /// <param name="values">The values.</param>
        /// <param name="method">The method.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// <exception cref="NotImplementedException">$Method {method.Method} is not implemented!</exception>
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
                else if (method == HttpMethod.Put) // TODO: Put actual data
                    response = await client.PutAsync($"https://beam.pro/api/v1/{subUrl}", new MultipartFormDataContent());
                else if(method == HttpMethod.Get)
                    response = await client.GetAsync($"https://beam.pro/api/v1/{subUrl}");
                else if(method == HttpMethod.Delete)
                    response = await client.DeleteAsync($"https://beam.pro/api/v1/{subUrl}");
                else if (method == new HttpMethod("PATCH"))
                { // Not tested!
                    if(values == null || !values.ContainsKey("content"))
                        throw new ArgumentException("PATCH requests must have a dic with key content");
                    var request = new HttpRequestMessage(method, $"https://beam.pro/api/v1/{subUrl}")
                    {
                        Content = new StringContent(values["content"], Encoding.UTF8, "application/json")
                    };
                    response = await client.SendAsync(request);
                }
                else
                    throw new NotImplementedException($"Method {method.Method} is not implemented!");

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
#else // Generate no warning on release build.
                            break;
#endif
                        case HttpStatusCode.NoContent: // ?????
                            return "true";
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
        public async Task<List<BeamChannel>> ListChannels(int page = 1, int limit = 50, string where = "",
            string fields = "", string order = "")
        {
            if (limit > 50)
                limit = 50;
            return
                JsonConvert.DeserializeObject<List<BeamChannel>>(
                    await Call_API($"channels?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;BeamChannel.BeamChannel&gt;.</returns>
        public async Task<BeamChannel> GetChannel(uint channelId)
        {
            return JsonConvert.DeserializeObject<BeamChannel>(await Call_API($"channels/{channelId}"));
        }

        /// <summary>
        /// Gets the channel details.
        /// </summary>
        /// <param name="channelIdOrToken">The channel identifier or token.</param>
        /// <returns>Task&lt;BeamChannel.BeamChannel&gt;.</returns>
        public async Task<BeamChannel> GetChannelDetails(uint channelIdOrToken)
        {
            return JsonConvert.DeserializeObject<BeamChannel>(await Call_API($"channels/{channelIdOrToken}/details"));
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
        public async Task<List<BeamViewerAnalytic>> GetViewers(uint channelId, string from, string to = null)
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
        public async Task<List<BeamViewerMetricAnalytic>> GetViewersMetrics(uint channelId, string from, string to = null)
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
        public async Task<List<BeamStreamSessionAnalytic>> GetStreamSessions(uint channelId, string from, string to = null)
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
        public async Task<List<BeamStreamHostAnalytic>> GetStreamHosts(uint channelId, string from, string to = null)
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
        public async Task<List<BeamSubscriptionAnalytic>> GetSubscriptions(uint channelId, string from, string to = null)
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
        public async Task<List<BeamFollowerAnalytic>> GetFollowers(uint channelId, string from, string to = null)
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
        public async Task<List<BeamFollowerAnalytic>> GetSparksSpent(uint channelId, string from, string to = null)
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
        public async Task<List<BeamEmojiRankAnalytic>> GetEmojiRanks(uint channelId, string from, string to = null)
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
        public async Task<List<BeamEmojiRankAnalytic>> GetEmojiUsage(uint channelId, string from, string to = null)
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
        public async Task<List<BeamGameRankAnalytic>> GetGameRanks(uint channelId, string from, string to = null)
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
        public async Task<List<BeamGameRankAnalytic>> GetGlobalGameRanks(uint channelId, string from, string to = null)
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
        public async Task<List<BeamSubRevenueAnalytic>> GetSubRevenue(uint channelId, string from, string to = null)
        {
            if (to == null)
                to = DateTime.UtcNow.ToString("o");

            return JsonConvert.DeserializeObject<List<BeamSubRevenueAnalytic>>(await Call_API($"channels/{channelId}/analytics/tsdb/subRevenue?from={from}&to={to}"));
        }
        #endregion

        #region Misc Actions

        /// <summary>
        /// Sets the badge.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetBadge(uint channelId)
        {
            // Needs api to upload things.
            // See https://dev.beam.pro/rest.html#channels__channelId__badge_post
            // And https://dev.beam.pro/rest.html#ExpandedChannel
            // And https://github.com/Subtixx/beam-client-csharp/issues/4
            throw new NotImplementedException();
            /*Dictionary<string, string> values = new Dictionary<string, string>();
            await Call_API($"channels/{channelId}/badge", values);*/
        }

        /// <summary>
        /// Lists the followers.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="where">The where.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="order">The order.</param>
        /// <returns>Task&lt;List&lt;BeamChannel&gt;&gt;.</returns>
        public async Task<List<BeamChannel>> ListFollowers(uint channelId, int page = 1, int limit = 25, string where = "",
            string fields = "", string order = "")
        {
            if (limit > 25)
                limit = 25;
            //follow 
            return
                JsonConvert.DeserializeObject<List<BeamChannel>>(
                    await Call_API($"channels/{channelId}/follow?page={page}&limit={limit}&where={where}&fields={fields}&order={order}"));
        }

        /// <summary>
        /// Follows the channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        // This currently has no sample of a response.
        public async Task<string> FollowChannel(uint channelId)
        {
            return await Call_API($"channels/{channelId}/follow", null, HttpMethod.Put);
        }

        /// <summary>
        /// Unfollows the channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        // This currently has no sample of a response.
        public async Task<string> UnfollowChannel(uint channelId)
        {
            return await Call_API($"channels/{channelId}/follow", null, HttpMethod.Delete);
        }

        /// <summary>
        /// Gets the emoticons.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        // This currently has no sample of a response.
        public async Task<string> GetEmoticons(uint channelId, uint? userId = null)
        {
            if(userId == null)
                return await Call_API($"channels/{channelId}/emoticons", null, HttpMethod.Get);

            return await Call_API($"channels/{channelId}/emoticons?user={userId}", null, HttpMethod.Get);
        }

        /// <summary>
        /// Changes the channel's emoticons.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="change">A list of changes described by https://tools.ietf.org/html/rfc6902.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<List<BeamEmoticonGroup>> ChangeEmoticons(uint channelId, string change)
        {
            Dictionary<string, string> values = new Dictionary<string, string> {{"content", change}};

            return JsonConvert.DeserializeObject< List<BeamEmoticonGroup>>(await Call_API($"channels/{channelId}/emoticons", values, new HttpMethod("PATCH")));
        }

        /// <summary>
        /// Redirects to the channel that is being hosted, if this channel is actively hosting.
        /// </summary>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// This currently has no sample of a response.
        public async Task<string> GetHostee(uint channelId)
        {
            return await Call_API($"channels/{channelId}/hostee", null, HttpMethod.Get);
        }

        /// <summary>
        /// Sets the hosted channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> SetHostee(uint channelId, uint id)
        {
            Dictionary<string, string> values = new Dictionary<string, string> { { "content", "{\"id\":"+id+"}" } };

            return await Call_API($"channels/{channelId}/hostee", null, HttpMethod.Put);
        }

        /// <summary>
        ///  	Stops hosting another channel 
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// This currently has no sample of a response.
        public async Task<string> StopHostee(uint channelId)
        {
            return await Call_API($"channels/{channelId}/hostee", null, HttpMethod.Delete);
        }

        /// <summary>
        /// Gets a list of channels hosting this channel
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<List<BeamChannelAdvanced>> GetHosters(uint channelId)
        {
            return JsonConvert.DeserializeObject<List<BeamChannelAdvanced>>(await Call_API($"channels/{channelId}/hosters", null, HttpMethod.Get));
        }

        /// <summary>
        /// Gets a stream manifest. Please note that if FTL is enabled for a stream, the manifest will differ.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="type">string(smil, m3u8, light)</param>
        /// <param name="showAudioOnly">if set to <c>true</c> [show audio only].</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// This currently has no sample response
        /*
        Body
            Media Type: application/xml
            Data Type: object

            Media Type: application/x-mpegurl
            Data Type: object

            Media Type: application/json
            Data Type: LightVideoManifest | FTLVideoManifest
        */
        public async Task<string> GetStreamManifest(uint channelId, string type, bool showAudioOnly = false)
        {
            return await Call_API($"channels/{channelId}/manifest.{type}?showAudioOnly={showAudioOnly}", null, HttpMethod.Get);
        }

        /// <summary>
        /// Gets the partnership application status for this channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<BeamPartnershipApp> GetPartnershipApp(uint channelId)
        {
            return JsonConvert.DeserializeObject<BeamPartnershipApp>(await Call_API($"channels/{channelId}/partnership/app", null, HttpMethod.Get));
        }

        /// <summary>
        /// Denies a partnership application.
        /// Why is this even a thing?
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reapply">The reapply.</param>
        /// <param name="ban">if set to <c>true</c> [ban].</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> DenyPartnershipApp(uint channelId, string reason, int reapply, bool ban)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                {"reason", reason},
                {"reapply", reapply.ToString()},
                {"ban", ban.ToString()}
            };
            return await Call_API($"channels/{channelId}/partnership/app/deny", values, HttpMethod.Post);
        }

        /// <summary>
        /// Accepts the partnership application.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> AcceptPartnershipApp(uint channelId)
        {
            return await Call_API($"channels/{channelId}/partnership/app/deny", null, HttpMethod.Post) == "true";
        }

        /// <summary>
        /// Gets the partnership codes.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;List&lt;BeamRedeemable&gt;&gt;.</returns>
        public async Task<List<BeamRedeemable>> GetPartnershipCodes(uint channelId)
        {
            return JsonConvert.DeserializeObject<List<BeamRedeemable>>(await Call_API($"channels/{channelId}/partnership/codes", null, HttpMethod.Get));
        }

        /// <summary>
        /// Gets the preferences.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;ChannelPreferences&gt;.</returns>
        public async Task<ChannelPreferences> GetPreferences(uint channelId)
        {
            return JsonConvert.DeserializeObject<ChannelPreferences>(await Call_API($"channels/{channelId}/preferences", null, HttpMethod.Get));
        }

        /// <summary>
        /// Sets the preferences.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns>Task&lt;ChannelPreferences&gt;.</returns>
        // TODO: Send actual json instead of empty post request ^.^
        public async Task<ChannelPreferences> SetPreferences(uint channelId)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            return
                JsonConvert.DeserializeObject<ChannelPreferences>(
                    await Call_API($"channels/{channelId}/preferences", values, HttpMethod.Post));
        }
        #endregion
    }
}