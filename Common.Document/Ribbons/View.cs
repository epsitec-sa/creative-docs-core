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
    /// La classe View définit les modes d'affichage.
    /// </summary>
    public class View : Abstract
    {
        public View(DocumentType type, InstallType install, DebugMode debugMode)
            : base(type, install, debugMode)
        {
            this.Title = Res.Strings.Action.ViewMain;
            this.PreferredWidth = 8 + 22 * 5 + 5;

            this.buttonPreview = this.CreateIconButton("Preview");
            this.buttonGrid = this.CreateIconButton("Grid");
            this.buttonTextGrid = this.CreateIconButton("TextGrid");
            this.buttonMagnet = this.CreateIconButton("Magnet");
            this.buttonMagnetLayer = this.CreateIconButton("MagnetLayer");
            this.buttonRulers = this.CreateIconButton("Rulers");
            this.buttonLabels = this.CreateIconButton("Labels");
            this.buttonAggregates = this.CreateIconButton("Aggregates");
            this.buttonConstrain = this.CreateIconButton("Constrain");

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

            if (this.buttonPreview == null)
                return;

            double dx = this.buttonPreview.PreferredWidth;
            double dy = this.buttonPreview.PreferredHeight;

            Rectangle rect = this.UsefulZone;
            rect.Width = dx;
            rect.Height = dy;
            rect.Offset(0, dy + 5);
            this.buttonPreview.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonGrid.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonTextGrid.SetManualBounds(rect);
            rect.Offset(dx + 5, 0);
            this.buttonMagnet.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonMagnetLayer.SetManualBounds(rect);

            rect = this.UsefulZone;
            rect.Width = dx;
            rect.Height = dy;
            this.buttonRulers.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonLabels.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonAggregates.SetManualBounds(rect);
            rect.Offset(dx + 5 + dx, 0);
            this.buttonConstrain.SetManualBounds(rect);
        }

        protected IconButton buttonPreview;
        protected IconButton buttonGrid;
        protected IconButton buttonTextGrid;
        protected IconButton buttonMagnet;
        protected IconButton buttonMagnetLayer;
        protected IconButton buttonRulers;
        protected IconButton buttonLabels;
        protected IconButton buttonAggregates;
        protected IconButton buttonConstrain;
    }
}
