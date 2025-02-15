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

using Epsitec.Common.Document;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Platform;

namespace Epsitec.Common.DocumentEditor.Dialogs
{
    /// <summary>
    /// Dialogue des informations sur le document.
    /// </summary>
    public class Splash : Abstract
    {
        public Splash(DocumentEditor editor)
            : base(editor) { }

        public override void Show()
        {
            /*
            //	Crée et montre la fenêtre du dialogue.
            if (this.window == null)
            {
                double dx = 400;
                double dy = 200;
                Rectangle mw = this.globalSettings.MainWindowBounds;
                Rectangle display = ScreenInfo.Find(mw.Center).Bounds;

                if (!display.IsValid)
                {
                    display = ScreenInfo.FitIntoWorkingArea(mw);
                }

                Rectangle wrect = new Rectangle(
                    display.Center.X - dx / 2,
                    display.Center.Y - dy / 2,
                    dx,
                    dy
                );

                this.window = new PlatformWindow();
                this.window.MakeFramelessWindow();
                this.window.MakeTopLevelWindow();
                this.window.ClientSize = wrect.Size;
                this.window.WindowLocation = wrect.Location;
                this.window.PreventAutoClose = true;
                this.window.Root.PaintForeground += this.HandleSplashPaintForeground;
                this.window.Owner = this.editor.PlatformWindow;

                StaticText image = About.CreateWidgetSplash(
                    this.window.Root,
                    this.editor.InstallType,
                    this.editor.DocumentType
                );
                image.Clicked += this.HandleSplashImageClicked;

                this.workInProgress = new StaticText(image);
                if (this.editor.InstallType == InstallType.Freeware)
                {
                    this.workInProgress.SetManualBounds(new Rectangle(140, 50, 250, 20));
                }
                else
                {
                    this.workInProgress.SetManualBounds(new Rectangle(21, 50, 350, 20));
                }
                this.workInProgress.SyncPaint = true;

                this.window.Show();
                PlatformWindow.PumpEvents();

                Common.OpenType.FontIdentityCallback callback =
                    new Common.OpenType.FontIdentityCallback(this.UpdateWorkInProgress);
                Common.Text.TextContext.InitializeFontCollection(callback);

                this.workInProgress.Text = ""; // efface le texte
                this.window.Root.Invalidate();

                this.splashTimer = new Timer();
                this.splashTimer.TimeElapsed += this.HandleSplashTimerTimeElapsed;
                this.splashTimer.Period = 10.0;

                this.window.MakeLayeredWindow();
                PlatformWindow.PumpEvents();
            }
            else
            {
                this.window.Show();
            }
            */
        }

        public void StartTimer()
        {
            //	Démarre le timer pour refermer le dialogue.
            if (this.splashTimer != null)
            {
                this.splashTimer.Start();
            }
        }

        private void UpdateWorkInProgress(Common.OpenType.FontIdentity fid)
        {
            string text = string.Format(
                Res.Strings.Dialog.Splash.WorkInProgress,
                fid.InvariantFaceName
            );
            this.workInProgress.Text = string.Concat(
                "<font size=\"90%\">",
                TextLayout.ConvertToTaggedText(text),
                "</font>"
            );
            this.window.Root.Invalidate();
        }

        private void HandleSplashPaintForeground(object sender, PaintEventArgs e)
        {
            WindowRoot root = sender as WindowRoot;
            double dx = root.Client.Size.Width;
            double dy = root.Client.Size.Height;
            Graphics graphics = e.Graphics;
            graphics.LineWidth = 1;
            graphics.AddRectangle(0.5, 0.5, dx - 1, dy - 1);
            graphics.RenderSolid(Color.FromBrightness(0));
        }

        private void HandleSplashImageClicked(object sender, MessageEventArgs e)
        {
            this.DeleteWindowSplash();
        }

        private void HandleSplashTimerTimeElapsed(object sender)
        {
            this.DeleteWindowSplash();
        }

        protected void DeleteWindowSplash()
        {
            if (this.window == null)
                return;

#if true
            this.window.WindowAnimationEnded += this.HandleWindowAnimationEnded;
            this.window.AnimateHide(Common.Widgets.Animation.FadeOut);

            this.splashTimer.Stop();
            this.splashTimer.TimeElapsed -= this.HandleSplashTimerTimeElapsed;
            this.splashTimer.Dispose();
            this.splashTimer = null;
#else
			this.window.Hide();
			this.window.Dispose();
			this.window = null;
			this.OnClosed();

			this.splashTimer.Stop();
			this.splashTimer.TimeElapsed -= this.HandleSplashTimerTimeElapsed;
			this.splashTimer.Dispose();
			this.splashTimer = null;
#endif
        }

        private void HandleWindowAnimationEnded(object sender)
        {
            if (this.window == null)
                return;

            this.window.Hide();
            this.window.Dispose();
            this.window = null;
            this.OnClosed();
        }

        protected StaticText workInProgress;
        protected Timer splashTimer;
    }
}
