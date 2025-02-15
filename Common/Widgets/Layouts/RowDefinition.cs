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


using Epsitec.Common.Support;
using Epsitec.Common.Types;

[assembly: Epsitec.Common.Types.DependencyClass(
    typeof(Epsitec.Common.Widgets.Layouts.RowDefinition)
)]

namespace Epsitec.Common.Widgets.Layouts
{
    /// <summary>
    /// The <c>RowDefinition</c> class defines row-specific properties that apply
    /// to a <see cref="T:GridLayoutEngine"/> object.
    /// </summary>
    public sealed class RowDefinition : DependencyObject
    {
        public RowDefinition() { }

        public RowDefinition(double height, GridUnitType gridUnitType = GridUnitType.Absolute)
            : this(new GridLength(height, gridUnitType)) { }

        public RowDefinition(GridLength height)
        {
            if (this.Height != height)
            {
                this.Height = height;
            }
        }

        public RowDefinition(double minHeight, double maxHeight)
        {
            if (this.MinHeight != minHeight)
            {
                this.MinHeight = minHeight;
            }
            if (this.MaxHeight != maxHeight)
            {
                this.MaxHeight = maxHeight;
            }
        }

        public RowDefinition(GridLength height, double minHeight, double maxHeight)
        {
            if (this.Height != height)
            {
                this.Height = height;
            }
            if (this.MinHeight != minHeight)
            {
                this.MinHeight = minHeight;
            }
            if (this.MaxHeight != maxHeight)
            {
                this.MaxHeight = maxHeight;
            }
        }

        public double MinHeight
        {
            get
            {
                if (this.Visibility)
                {
                    return (double)this.GetValue(RowDefinition.MinHeightProperty);
                }
                else
                {
                    return 0.0;
                }
            }
            set { this.SetValue(RowDefinition.MinHeightProperty, value); }
        }

        public double MaxHeight
        {
            get
            {
                if (this.Visibility)
                {
                    return (double)this.GetValue(RowDefinition.MaxHeightProperty);
                }
                else
                {
                    return 0.0;
                }
            }
            set { this.SetValue(RowDefinition.MaxHeightProperty, value); }
        }

        public GridLength Height
        {
            get
            {
                if (this.Visibility)
                {
                    return (GridLength)this.GetValue(RowDefinition.HeightProperty);
                }
                else
                {
                    return new GridLength(0.0);
                }
            }
            set { this.SetValue(RowDefinition.HeightProperty, value); }
        }

        public double TopBorder
        {
            get
            {
                if (this.Visibility)
                {
                    return (double)this.GetValue(RowDefinition.TopBorderProperty);
                }
                else
                {
                    return 0.0;
                }
            }
            set { this.SetValue(RowDefinition.TopBorderProperty, value); }
        }

        public double BottomBorder
        {
            get
            {
                if (this.Visibility)
                {
                    return (double)this.GetValue(RowDefinition.BottomBorderProperty);
                }
                else
                {
                    return 0.0;
                }
            }
            set { this.SetValue(RowDefinition.BottomBorderProperty, value); }
        }

        public double ActualHeight
        {
            get { return this.actualHeight; }
        }

        public double ActualOffset
        {
            get { return this.actualOffset; }
        }

        public bool Visibility
        {
            get { return (bool)this.GetValue(RowDefinition.VisibilityProperty); }
            set { this.SetValue(RowDefinition.VisibilityProperty, value); }
        }

        internal void DefineActualHeight(double value)
        {
            this.actualHeight = value;
        }

        internal void DefineActualOffset(double value)
        {
            this.actualOffset = value;
        }

        private void OnChanged()
        {
            this.Changed.Raise(this);
        }

        private static void NotifyPropertyInvalidated(
            DependencyObject o,
            object oldValue,
            object newValue
        )
        {
            RowDefinition def = (RowDefinition)o;
            def.OnChanged();
        }

        public event Support.EventHandler Changed;

        public static readonly DependencyProperty MinHeightProperty = DependencyProperty.Register(
            "MinHeight",
            typeof(double),
            typeof(RowDefinition),
            new DependencyPropertyMetadata(0.0, RowDefinition.NotifyPropertyInvalidated)
        );
        public static readonly DependencyProperty MaxHeightProperty = DependencyProperty.Register(
            "MaxHeight",
            typeof(double),
            typeof(RowDefinition),
            new DependencyPropertyMetadata(
                double.PositiveInfinity,
                RowDefinition.NotifyPropertyInvalidated
            )
        );
        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height",
            typeof(GridLength),
            typeof(RowDefinition),
            new DependencyPropertyMetadata(GridLength.Auto, RowDefinition.NotifyPropertyInvalidated)
        );
        public static readonly DependencyProperty TopBorderProperty = DependencyProperty.Register(
            "TopBorder",
            typeof(double),
            typeof(RowDefinition),
            new DependencyPropertyMetadata(0.0, RowDefinition.NotifyPropertyInvalidated)
        );
        public static readonly DependencyProperty BottomBorderProperty =
            DependencyProperty.Register(
                "BottomBorder",
                typeof(double),
                typeof(RowDefinition),
                new DependencyPropertyMetadata(0.0, RowDefinition.NotifyPropertyInvalidated)
            );
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
            "Visibility",
            typeof(bool),
            typeof(RowDefinition),
            new DependencyPropertyMetadata(true, RowDefinition.NotifyPropertyInvalidated)
        );

        private double actualOffset;
        private double actualHeight;
    }
}
