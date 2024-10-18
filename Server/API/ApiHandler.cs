// ImperialLibrary - LGPLv3 License
// Copyright (C) 2024 Imperial Solutions
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation, either version 3 of the License,
// or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library. If not, see <https://www.gnu.org/licenses/>.

using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ImperialLibrary.Utils;
using Newtonsoft.Json;

namespace ImperialLibrary.Server.API
{
    /// <remarks>
    /// Initializes the ApiHandler with the base URL.
    /// </remarks>
    public abstract class ApiHandler(string baseUrl)
    {
        /// <summary>
        /// Makes a generic API request to the specified endpoint using the base URL of the Imperial CAD API.
        /// </summary>
        /// <param name="endpoint">The API endpoint to request (relative to the base URL).</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="payload">Optional payload for POST/PUT requests.</param>
        /// <param name="queryParams">Optional query parameters for GET requests.</param>
        /// <returns>A Task representing the API response as a string.</returns>
        internal async Task<string> RequestApi(string endpoint, HttpMethod method, object payload = null, object queryParams = null)
        {
            try
            {
                Url requestUrl = baseUrl.AppendPathSegment(endpoint);

                if (method == HttpMethod.Get)
                {
                    if (queryParams != null)
                    {
                        requestUrl = requestUrl.SetQueryParams(queryParams);
                    }
                    return await requestUrl.GetStringAsync();
                }
                else
                {
                    string payloadJson = payload != null ? JsonConvert.SerializeObject(payload) : "null";

                    return method == HttpMethod.Post || method == HttpMethod.Put
                        ? await requestUrl.SendJsonAsync(method, payload).ReceiveString()
                        : await requestUrl.SendAsync(method).ReceiveString();
                }
            }
            catch (FlurlHttpException ex)
            {
                string errorResponse = await ex.GetResponseStringAsync();
                Logger.Log($"Error during API request: {errorResponse}", LogLevel.Error);
                return null;
            }
        }
    }
}