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

using CitizenFX.Core;
using ImperialLibrary.Server.API;
using ImperialLibrary.Utils;
using static CitizenFX.Core.Native.API;
using static ImperialLibrary.Server.API.ApiWrapper;

namespace ImperialLibrary.Server
{
    internal class Init : BaseScript
    {
        private readonly string communityId;
        private readonly ApiWrapper apiWrapper;

        public Init()
        {
            communityId = GetConvar("imperial_community_id", null);
            apiWrapper = new ApiWrapper();

            if (communityId == null)
            {
                Logger.Log("cvar 'imperial_community_id' is NULL! set this in your server.cfg ASAP!", LogLevel.Error);
                return;
            }

            // Check if another plugin with ImperialLibrary loaded before us has already set the 'imperial_community_id' GlobalState
            // containing the community ID after validating the community's existence. 
            // If not set already, perform the validation and set the community ID.
            if (string.IsNullOrEmpty(GlobalState["imperial_community_id"]))
            {
                Logger.Log("GlobalState 'imperial_community_id' hasn't been set already, lets be the first plugin to do it.", LogLevel.Verbose);
                LinkCommunity();
            }
        }

        private async void LinkCommunity()
        {
            CommunityResponse exists = await apiWrapper.DoesCommunityIdExist(communityId);

            if (!exists.VALID)
            {
                Logger.Log($"'{communityId}' doesn't seem to exist. Please verify the Imperial CAD community ID.", LogLevel.Warn);
                return;
            }

            Logger.Log($"Successfully set Imperial CAD to Community: {exists.CommunityName} (https://imperialcad.app/home/{communityId})");
            GlobalState.Set("imperial_community_id", communityId, true);
        }
    }
}