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
using CitizenFX.Core;
using ImperialLibrary.Utils;
using static CitizenFX.Core.Native.API;

namespace ImperialLibrary.Server
{
    public class Version : BaseScript
    {
        /// <summary>
        /// The current version of ImperialLibrary.
        /// </summary>
        public static readonly string LibraryVersion = "1.0.0";

        [EventHandler("onServerResourceStart")]
        private void OnServerResourceStart(string resourceName)
        {
            if (resourceName != GetCurrentResourceName())
            {
                return;
            }

            Logger.Log($"Successfully loaded ImperialLibrary version: {LibraryVersion}");
        }
    }
}