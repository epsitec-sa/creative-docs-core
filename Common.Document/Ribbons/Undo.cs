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
    /// La classe Undo gère les commandes undo/redo.
    /// </summary>
    public class Undo : Abstract
    {
        public Undo(DocumentType type, InstallType install, DebugMode debugMode)
            : base(type, install, debugMode)
        {
            this.Title = Res.Strings.Action.UndoMain;
            this.PreferredWidth = 8 + 22 * 3.0;

            double dx = 22;
            double dy = 22;

            this.groupUndo = new Widget(this);

            this.buttonList = this.CreateMenuButton(
                "UndoRedoList",
                Res.Strings.Action.UndoRedoList,
                this.HandleListPressed
            );
            this.buttonList.ContentAlignment = ContentAlignment.BottomCenter;
            this.buttonList.GlyphSize = new Size(dx * 3.0, dy * 0.5);
            this.buttonList.Anchor = AnchorStyles.All;
            this.buttonList.SetParent(this.groupUndo);

            this.buttonUndo = this.CreateIconButton("Undo", "Large");
            this.buttonUndo.ButtonStyle = ButtonStyle.ComboItem;
            this.buttonUndo.PreferredSize = new Size(dx * 1.5, dy * 1.5);
            this.buttonUndo.Dock = DockStyle.Left;
            this.buttonUndo.VerticalAlignment = VerticalAlignment.Top;
            this.buttonUndo.SetParent(this.groupUndo);

            this.buttonRedo = this.CreateIconButton("Redo", "Large");
            this.buttonRedo.ButtonStyle = ButtonStyle.ComboItem;
            this.buttonRedo.PreferredSize = new Size(dx * 1.5, dy * 1.5);
            this.buttonRedo.Dock = DockStyle.Left;
            this.buttonRedo.VerticalAlignment = VerticalAlignment.Top;
            this.buttonRedo.SetParent(this.groupUndo);

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

            if (this.buttonUndo == null)
                return;

            double dx = 22;
            double dy = 22;

            Rectangle rect = this.UsefulZone;
            rect.Width = dx * 3.0;
            rect.Height = dy * 2.0;
            this.groupUndo.SetManualBounds(rect);
        }

        private void HandleListPressed(object sender, MessageEventArgs e)
        {
            //	Bouton pour ouvrir la liste cliqué.
            if (this.document.Modifier.ActiveViewer.IsCreating)
            {
                return;
            }

            if (
                !this.document.Modifier.OpletQueue.CanUndo
                && !this.document.Modifier.OpletQueue.CanRedo
            )
            {
                return;
            }

            GlyphButton button = sender as GlyphButton;
            if (button == null)
                return;
            VMenu menu = this.document.Modifier.CreateUndoRedoMenu(null);
            menu.Host = this;
            menu.MinWidth = button.ActualWidth;
            TextFieldCombo.AdjustComboSize(button, menu, false);
            menu.ShowAsComboList(button, Point.Zero, button);
        }

        protected Widget groupUndo;
        protected IconButton buttonUndo;
        protected IconButton buttonRedo;
        protected GlyphButton buttonList;
    }
}
