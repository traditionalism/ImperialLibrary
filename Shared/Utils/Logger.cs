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

namespace ImperialLibrary.Utils
{
    /// <summary>
    /// Enum representing different levels of logging.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Information about high-level, successful operations.
        /// </summary>
        Info,

        /// <summary>
        /// Warns of an unexpected condition, or a state which is likely to cause an error in the future..
        /// </summary>
        Warn,

        /// <summary>
        /// Indicates a failure in the system.
        /// </summary>
        Error,

        /// <summary>
        /// Used by developers to understand the system and may contain detailed trace information. Should generally not be turned on when not debugging.
        /// </summary>
        Debug,

        /// <summary>
        /// More detailed information containing intermediate steps of high-level operations.
        /// </summary>
        Verbose
    }

    public static class Logger
    {
        /// <summary>
        /// Logs a message to the console with an optional log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The level of the log message. Defaults to <see cref="LogLevel.Info"/>.</param>
        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            string prefix = level switch
            {
                LogLevel.Info => "^7[INFO]",
                LogLevel.Warn => "^3[WARN]",
                LogLevel.Error => "^1[ERROR]",
                LogLevel.Debug => "^6[DEBUG]",
                LogLevel.Verbose => "^4[VERBOSE]",
                _ => "^7[INFO]"
            };

            CitizenFX.Core.Debug.WriteLine($"{prefix}[ImperialLibrary] {message}^0");
        }
    }
}