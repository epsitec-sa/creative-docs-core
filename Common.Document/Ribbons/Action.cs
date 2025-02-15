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

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Document.Ribbons
{
    /// <summary>
    /// La classe Action permet de gérer les commandes du menu aide.
    /// </summary>
    public class Action : Abstract
    {
        public Action(DocumentType type, InstallType install, DebugMode debugMode)
            : base(type, install, debugMode)
        {
            this.Title = Res.Strings.Action.ActionMain;
            this.PreferredWidth = 8 + 22 * 1.5 + 4 + 22 * 2;

            this.buttonSettings = this.CreateIconButton("Settings", "Large");
            this.buttonInfos = this.CreateIconButton("Infos");
            this.buttonPageStack = this.CreateIconButton("PageStack");
            this.buttonUpdate = this.CreateIconButton("UpdateApplication");
            this.buttonAbout = this.CreateIconButton("AboutApplication");

            //			this.UpdateClientGeometry();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { }

            base.Dispose(disposing);
        }

        protected override void UpdateClientGeometry()
        {
            //	Met à jour la géométrie.
            base.UpdateClientGeometry();

            if (this.buttonSettings == null)
                return;

            double dx = this.buttonSettings.PreferredWidth;
            double dy = this.buttonSettings.PreferredHeight;

            Rectangle rect = this.UsefulZone;
            rect.Width = dx * 1.5;
            rect.Height = dy * 1.5;
            rect.Offset(0, dy * 0.5);
            this.buttonSettings.SetManualBounds(rect);

            rect = this.UsefulZone;
            rect.Width = dx;
            rect.Height = dy;
            rect.Offset(dx * 1.5 + 4, dy + 5);
            this.buttonInfos.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonPageStack.SetManualBounds(rect);

            rect = this.UsefulZone;
            rect.Width = dx;
            rect.Height = dy;
            rect.Offset(dx * 1.5 + 4, 0);
            this.buttonUpdate.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonAbout.SetManualBounds(rect);
        }

        protected IconButton buttonSettings;
        protected IconButton buttonInfos;
        protected IconButton buttonPageStack;
        protected IconButton buttonKey;
        protected IconButton buttonUpdate;
        protected IconButton buttonAbout;
    }
}
