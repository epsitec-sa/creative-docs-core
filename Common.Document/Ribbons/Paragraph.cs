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
    /// La classe Paragraph permet de choisir le style de paragraphe du texte.
    /// </summary>
    public class Paragraph : Abstract
    {
        public Paragraph(DocumentType type, InstallType install, DebugMode debugMode)
            : base(type, install, debugMode)
        {
            this.Title = Res.Strings.Action.ParagraphMain;
            this.PreferredWidth = 123;

            this.buttonJustif = this.CreateIconButtonCombo("ParagraphJustif");
            this.AddIconButtonCombo(
                this.buttonJustif,
                "ParagraphJustifLeft",
                "JustifHLeft",
                Res.Strings.Property.Justif.JustifHLeft
            );
            this.AddIconButtonCombo(
                this.buttonJustif,
                "ParagraphJustifCenter",
                "JustifHCenter",
                Res.Strings.Property.Justif.JustifHCenter
            );
            this.AddIconButtonCombo(
                this.buttonJustif,
                "ParagraphJustifRight",
                "JustifHRight",
                Res.Strings.Property.Justif.JustifHRight
            );
            this.AddIconButtonCombo(
                this.buttonJustif,
                "ParagraphJustifJustif",
                "JustifHJustif",
                Res.Strings.Property.Justif.JustifHJustif
            );
            this.AddIconButtonCombo(
                this.buttonJustif,
                "ParagraphJustifAll",
                "JustifHAll",
                Res.Strings.Property.Justif.JustifHAll
            );

            this.buttonIndentMinus = this.CreateIconButton("ParagraphIndentMinus");
            this.buttonIndentPlus = this.CreateIconButton("ParagraphIndentPlus");

            this.buttonLeading = this.CreateIconButtonCombo("ParagraphLeading");
            this.AddIconButtonCombo(
                this.buttonLeading,
                "ParagraphLeading08",
                "ParagraphLeading08",
                Res.Strings.Property.Leading.Leading08
            );
            this.AddIconButtonCombo(
                this.buttonLeading,
                "ParagraphLeading10",
                "ParagraphLeading10",
                Res.Strings.Property.Leading.Leading10
            );
            this.AddIconButtonCombo(
                this.buttonLeading,
                "ParagraphLeading15",
                "ParagraphLeading15",
                Res.Strings.Property.Leading.Leading15
            );
            this.AddIconButtonCombo(
                this.buttonLeading,
                "ParagraphLeading20",
                "ParagraphLeading20",
                Res.Strings.Property.Leading.Leading20
            );
            this.AddIconButtonCombo(
                this.buttonLeading,
                "ParagraphLeading30",
                "ParagraphLeading30",
                Res.Strings.Property.Leading.Leading30
            );

            this.buttonLeadingMinus = this.CreateIconButton("ParagraphLeadingMinus");
            this.buttonLeadingPlus = this.CreateIconButton("ParagraphLeadingPlus");

            this.buttonClear = this.CreateIconButton("ParagraphClear");

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

            if (this.buttonClear == null)
                return;

            double dx = this.buttonClear.PreferredWidth;
            double dy = this.buttonClear.PreferredHeight;

            Rectangle rect = this.UsefulZone;
            rect.Offset(0, dy + 5);
            rect.Height = dy;
            rect.Width = dx * 1.5;
            this.buttonJustif.SetManualBounds(rect);
            rect.Offset(dx * 1.5 + 5, 0);
            rect.Width = dx;
            this.buttonIndentMinus.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonIndentPlus.SetManualBounds(rect);

            rect = this.UsefulZone;
            rect.Height = dy;
            rect.Width = dx * 1.5;
            this.buttonLeading.SetManualBounds(rect);
            rect.Offset(dx * 1.5 + 5, 0);
            rect.Width = dx;
            this.buttonLeadingMinus.SetManualBounds(rect);
            rect.Offset(dx, 0);
            this.buttonLeadingPlus.SetManualBounds(rect);
            rect.Offset(dx + 10, 0);
            this.buttonClear.SetManualBounds(rect);
        }

        protected IconButtonCombo buttonJustif;
        protected IconButton buttonIndentMinus;
        protected IconButton buttonIndentPlus;
        protected IconButtonCombo buttonLeading;
        protected IconButton buttonLeadingMinus;
        protected IconButton buttonLeadingPlus;
        protected IconButton buttonClear;
    }
}
