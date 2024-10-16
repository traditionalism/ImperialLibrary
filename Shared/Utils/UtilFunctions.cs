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
using static CitizenFX.Core.Native.API;

namespace ImperialLibrary.Utils
{
    public class UtilFunctions : BaseScript
    {
        /// <summary>
        /// Returns the set Imperial CAD community ID (if set).
        /// </summary>
        /// <returns>
        /// A string containing the community ID, or null if not set.
        /// </returns>
        public string GetImperialCommunityId()
        {
            string communityId = GlobalState["imperial_community_id"] ?? GetConvar("imperial_community_id", null);

            if (string.IsNullOrEmpty(communityId))
            {
                Logger.Log("'imperial_community_id' cvar is not set.", LogLevel.Warn);
                return null;
            }

            return communityId;
        }
    }
}