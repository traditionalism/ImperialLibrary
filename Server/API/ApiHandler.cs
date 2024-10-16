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

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImperialLibrary.Server.API
{
    /// <summary>
    /// ApiHandler is an base class for making API requests. 
    /// </summary>
    public abstract class ApiHandler
    {
        protected readonly HttpClient client;

        /// <summary>
        /// Initializes the ApiHandler.
        /// </summary>
        protected ApiHandler(string baseUrl)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        /// <summary>
        /// Makes a generic API request to the specified endpoint using the base URL of the Imperial CAD API.
        /// This method is for internal use only.
        /// </summary>
        /// <param name="endpoint">The API endpoint to request (relative to the base URL).</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="payload">Optional payload for POST/PUT requests.</param>
        /// <returns>A <see cref="HttpResponseMessage"/> containing the API response.</returns>
        internal async Task<HttpResponseMessage> RequestApi(string endpoint, HttpMethod method, object payload = null)
        {
            HttpRequestMessage request = new(method, endpoint);

            if (payload != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                string json = JsonConvert.SerializeObject(payload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return await client.SendAsync(request);
        }
    }
}