/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System.Collections.Generic;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Support
{
    /// <summary>
    /// The <c>PrivilegeManager</c> class gives access to privilege related
    /// informations.
    /// </summary>
    public sealed class PrivilegeManager
    {
        private PrivilegeManager()
        {
            this.shieldIcons = new Dictionary<IconSize, Image>();
        }

        /// <summary>
        /// Gets the current privilege manager; this manager is thread specific.
        /// </summary>
        /// <value>The current privilege manager.</value>
        public static PrivilegeManager Current
        {
            get
            {
                if (PrivilegeManager.manager == null)
                {
                    PrivilegeManager.manager = new PrivilegeManager();
                }

                return PrivilegeManager.manager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user is an administrator.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user is an administrator; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserAnAdministrator
        {
            get { return Platform.UserAccountApi.IsUserAnAdministrator; }
        }

        /// <summary>
        /// Gets the shield icon if the user running as an unprivileged user.
        /// </summary>
        /// <param name="iconSize">Size of the icon.</param>
        /// <returns>The shield icon or <c>null</c>.</returns>
        public Image GetShieldIcon(IconSize iconSize)
        {
            /*
            if (Platform.UserAccountApi.IsUserAnAdministrator)
            {
                return null;
            }

            Image bitmap;

            if (this.shieldIcons.TryGetValue(iconSize, out bitmap))
            {
                return bitmap;
            }

            switch (iconSize)
            {
                case IconSize.Default:
                case IconSize.Normal:
                    bitmap = Bitmap.FromNativeIcon(Platform.StockIcons.ShieldIcon);
                    break;

                case IconSize.Small:
                    bitmap = Bitmap.FromNativeIcon(Platform.StockIcons.SmallShieldIcon);
                    break;
            }

            this.shieldIcons[iconSize] = bitmap;

            return bitmap;
            */
            throw new System.NotImplementedException();
            return null;
        }

        /// <summary>
        /// Launches a copy of the current executable using an elevated user
        /// account; this is intended for Windows and UAC.
        /// </summary>
        /// <param name="applicationWindowHandle">The handle of the main window.</param>
        /// <param name="arguments">The arguments for the elevated process.</param>
        /// <returns><c>true</c> if the elevation worked; otherwise, <c>false</c>.</returns>
        public bool LaunchElevated(System.IntPtr applicationWindowHandle, string arguments)
        {
            System.Diagnostics.ProcessStartInfo startInfo =
                new System.Diagnostics.ProcessStartInfo();

            //	Force execution 'as' an administrator, using the same initial directory
            //	and application path as the currently executing process, but with specific
            //	command line arguments.

            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Globals.Directories.InitialDirectory;
            startInfo.FileName = Globals.ExecutablePath;
            startInfo.Arguments = arguments;
            startInfo.Verb = "runas";

            startInfo.ErrorDialog = true;
            startInfo.ErrorDialogParentHandle = applicationWindowHandle;

            try
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo);

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    return true;
                }
            }
            catch
            {
                //	Swallow all exceptions.
            }

            return false;
        }

        [System.ThreadStatic]
        private static PrivilegeManager manager;

        private readonly Dictionary<IconSize, Image> shieldIcons;
    }
}
