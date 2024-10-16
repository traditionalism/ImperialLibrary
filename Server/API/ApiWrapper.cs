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

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImperialLibrary.Server.API
{
    /// <summary>
    /// ApiWrapper provides an implementation for common API calls to the Imperial CAD API. 
    /// </summary>
    public class ApiWrapper : ApiHandler
    {
        private const string BaseApiUrl = "https://imperialcad.app/api/1.1/wf/";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiWrapper"/> class with the base URL
        /// set to the Imperial CAD API endpoint.
        /// </summary>
        public ApiWrapper() : base(BaseApiUrl)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Requests the Imperial CAD API to check if a community ID exists and returns a
        /// <see cref="CommunityResponse"/> indicating the validity of the ID.
        /// </summary>
        /// <param name="communityId">The community ID to be validated.</param>
        /// <returns>A <see cref="CommunityResponse"/> object containing the result of the API request.</returns>
        public async Task<CommunityAuthorizationResponse> GetCommunityAuthorization(string communityId)
        {
            HttpResponseMessage response = await RequestApi($"community_authorization?community_id={communityId}", HttpMethod.Get);
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommunityAuthorizationResponse>(jsonString);
        }

        /// <summary>
        /// Retrieves data for a specific unit from the Imperial CAD API.
        /// </summary>
        /// <param name="steamHex">The Steam hexadecimal ID of the player or unit to retrieve data for.</param>
        /// <param name="communityId">The ID of the community the unit belongs to.</param>
        /// <returns>
        /// A <see cref="UnitDataResponse"/> object containing <see cref="UnitData.UnitData"/> which holds data associated to the unit (if exists).
        /// </returns>
        public async Task<UnitDataResponse> GetUnitData(string steamHex, string communityId)
        {
            HttpResponseMessage response = await RequestApi($"unit_authorization?steam_hex={steamHex}&community_id={communityId}", HttpMethod.Get);
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UnitDataResponse>(jsonString);
        }

        /// <summary>
        /// Checks if the provided community ID exists.
        /// </summary>
        /// <param name="communityId">The community ID to check.</param>
        /// <returns>A CommunityResponse indicating whether the community ID is valid.</returns>
        public async Task<CommunityResponse> DoesCommunityIdExist(string communityId)
        {
            CommunityAuthorizationResponse response = await GetCommunityAuthorization(communityId);
            return response.Response.VALID ? new CommunityResponse(response.Response.VALID, response.Response.CommunityName) : new CommunityResponse(false, null);
        }

        public class CommunityAuthorizationResponse
        {
            public string Status { get; set; }
            public CommunityResponse Response { get; set; }
        }

        public class CommunityResponse
        {
            public bool VALID { get; set; }
            public string CommunityName { get; set; }

            public CommunityResponse() { }

            public CommunityResponse(bool valid, string communityName)
            {
                VALID = valid;
                CommunityName = communityName;
            }
        }

        public class UnitDataResponse
        {
            public string Status { get; set; }
            public UnitResponse Response { get; set; }
        }

        public class UnitResponse
        {
            public Unit Unit_data { get; set; }
        }

        public class Unit
        {
            public long ModifiedDate { get; set; }
            public long CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public string UnitIdentifier { get; set; }
            public string UnitStatus { get; set; }
            public string CommunityID { get; set; }
            public bool LoggedIn { get; set; }
            public string UnitFirstName { get; set; }
            public string UnitLastName { get; set; }
            public string Agency { get; set; }
            public bool FireUnit { get; set; }
            public bool OnDuty { get; set; }
            public string UnitSearch { get; set; }
            public string UnitAgencyInitials { get; set; }
            public string SteamHex { get; set; }
            public string Id { get; set; }
        }
    }
}