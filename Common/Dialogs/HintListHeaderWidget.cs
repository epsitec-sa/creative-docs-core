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


using Epsitec.Common.Dialogs;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

[assembly: DependencyClass(typeof(HintListController))]

namespace Epsitec.Common.Dialogs
{
    /// <summary>
    /// The <c>HintListHeaderWidget</c> class represents the header of a hint list
    /// (see <see cref="HintListWidget"/> and <see cref="HintListController"/>).
    /// </summary>
    public sealed class HintListHeaderWidget : FrameBox
    {
        public HintListHeaderWidget()
        {
            this.searchWidget = new HintListSearchWidget()
            {
                Embedder = this,
                Dock = DockStyle.Top,
                PreferredHeight = 24,
                Margins = new Drawing.Margins(2, 2, 2, 1),
                Name = "Search"
            };

            this.topHalf = new FrameBox()
            {
                Embedder = this,
                Dock = DockStyle.Top,
                ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
                PreferredHeight = 52,
                Name = "TopHalf"
            };

            this.bottomHalf = new FrameBox()
            {
                Embedder = this,
                Dock = DockStyle.Fill,
                ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
                Name = "BottomHalf"
            };

            this.image = new StaticImage()
            {
                Embedder = this.topHalf,
                Dock = DockStyle.StackBegin,
                VerticalAlignment = VerticalAlignment.Top,
                Margins = new Drawing.Margins(0, 4, 0, 0),
                PreferredSize = new Drawing.Size(64, 48),
                ImageSize = new Drawing.Size(64, 48),
                Name = "Icon"
            };

            this.title = new StaticText()
            {
                Embedder = this.topHalf,
                Dock = DockStyle.StackFill,
                TextBreakMode = Drawing.TextBreakMode.Ellipsis,
                Name = "Title"
            };

            this.toolBar = new HToolBar()
            {
                Embedder = this.bottomHalf,
                Dock = DockStyle.Fill,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                PreferredHeight = 20
            };

            this.IsEnabledChanged += (s, e) => this.RefreshContents();
            this.TextChanged += (s) => this.RefreshContents();
        }

        public HintListSearchWidget SearchWidget
        {
            get { return this.searchWidget; }
        }

        public HintListContentType ContentType
        {
            get { return this.contentType; }
            set
            {
                if (this.contentType != value)
                {
                    this.contentType = value;
                    this.OnContentTypeChanged();
                }
            }
        }

        public AbstractToolBar ToolBar
        {
            get { return this.toolBar; }
        }

        private void OnContentTypeChanged()
        {
            this.RefreshContents();
        }

        private void RefreshContents()
        {
            bool enable = this.IsEnabled;

            switch (this.contentType)
            {
                case HintListContentType.Default:
                    this.image.Visibility = false;
                    this.title.Visibility = enable;
                    break;

                case HintListContentType.Catalog:
                    this.image.Visibility = enable;
                    this.title.Visibility = enable;
                    this.image.ImageName = "manifest:Common/Dialogs/Images/box-64x48.png";
                    break;

                case HintListContentType.Suggestions:
                    this.image.Visibility = enable;
                    this.title.Visibility = enable;
                    this.image.ImageName = "manifest:Common/Dialogs/Images/binocular-64x48.png";
                    break;
            }

            this.title.Text = string.Format(
                this.GetTitleTextFormat().ToString(),
                this.FormattedText.ToString()
            );
        }

        private FormattedText GetTitleTextFormat()
        {
            switch (this.contentType)
            {
                case HintListContentType.Suggestions:
                    return new FormattedText(
                        @"Suggestions pour<br/><font size=""120%""><b>«{0}»</b></font>"
                    );

                case HintListContentType.Catalog:
                    return new FormattedText(
                        @"Liste des fiches<br/><font size=""120%""><b>«{0}»</b></font>"
                    );

                default:
                    return new FormattedText("{0}");
            }
        }

        private readonly Widget topHalf;
        private readonly Widget bottomHalf;
        private readonly StaticImage image;
        private readonly StaticText title;
        private readonly HToolBar toolBar;
        private readonly HintListSearchWidget searchWidget;

        private HintListContentType contentType;
    }
}
