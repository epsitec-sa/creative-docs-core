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
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Document.Panels
{
    /// <summary>
    /// La classe PageName permet de choisir le nom d'une page.
    /// </summary>
    [SuppressBundleSupport]
    public class PageName : Abstract
    {
        public PageName(Document document)
            : base(document)
        {
            this.label = new StaticText(this);
            this.label.Alignment = ContentAlignment.MiddleLeft;
            this.label.Text = Res.Strings.Panel.PageName.Label.Name;

            this.field = new TextField(this);
            this.field.TextChanged += new EventHandler(this.HandleTextChanged);
            this.field.TabIndex = 1;
            this.field.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
            ToolTip.Default.SetToolTip(this.field, Res.Strings.Panel.PageName.Tooltip.Name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.field.TextChanged -= new EventHandler(this.HandleTextChanged);
                this.field = null;
                this.label = null;
            }

            base.Dispose(disposing);
        }

        protected override void PropertyToWidgets()
        {
            //	Propriété -> widget.
            DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
            int sel = context.CurrentPage;

            string text = this.document.Modifier.PageName(sel);
            if (this.field.Text != text)
            {
                this.ignoreChanged = true;
                this.field.Text = text;
                this.ignoreChanged = false;
            }
        }

        protected override void WidgetsToProperty()
        {
            //	Widgets -> propriété.
            DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
            int sel = context.CurrentPage;

            if (this.document.Modifier.PageName(sel) != this.field.Text)
            {
                this.document.Modifier.PageName(sel, this.field.Text);
            }
        }

        protected override void UpdateClientGeometry()
        {
            //	Met à jour la géométrie.
            base.UpdateClientGeometry();

            if (this.field == null)
                return;

            Rectangle rect = this.Client.Bounds;
            rect.Deflate(this.extendedZoneWidth, 0);
            rect.Deflate(5);

            Rectangle r = rect;
            r.Bottom = r.Top - 20;
            r.Right = rect.Right - 110;
            this.label.Bounds = r;

            r = rect;
            r.Left = r.Right - 110;
            this.field.Bounds = r;
        }

        private void HandleTextChanged(object sender)
        {
            //	Une valeur a été changée.
            if (this.ignoreChanged)
                return;

            DrawingContext context = this.document.Modifier.ActiveViewer.DrawingContext;
            int sel = context.CurrentPage;
            this.document.Modifier.PageName(sel, this.field.Text);
        }

        public void SetDefaultFocus()
        {
            //	Met le focus par défaut.
            this.field.SelectAll();
            this.field.Focus();
        }

        protected StaticText label;
        protected TextField field;
    }
}
