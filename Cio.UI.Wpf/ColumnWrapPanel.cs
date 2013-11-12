/*
 * Cio
 * Copyright (C) 2013 Robin Kuijstermans
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see [http://www.gnu.org/licenses/].
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace Cio.UI.Wpf
{
	public class ColumnWrapPanel : Panel
    {
        public static readonly DependencyProperty SpanProperty = DependencyProperty.RegisterAttached("Span", typeof(Boolean), typeof(ColumnWrapPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetSpan(DependencyObject element, bool value)
        {
            element.SetValue(SpanProperty, value);
        }

        public static bool GetSpan(DependencyObject element)
        {
            return (bool)element.GetValue(SpanProperty);
        }

        private UIElementGrid grid;
        private bool remeasure = true;

        public ColumnWrapPanel()
        {
            this.ColumnPadding = 10;
            this.RowPadding = 10;
        }

        public double ColumnPadding
        {
            get;
            set;
        }

        public double RowPadding
        {
            get;
            set;
        }

        public bool RememberMeasure
        {
            get;
            set;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.remeasure || this.grid.Width > availableSize.Width || this.grid.Height > availableSize.Height)
            {
                this.grid = new UIElementGrid(this);

                for (int i = 0; i < InternalChildren.Count; i++)
                {
                    UIElement element = InternalChildren[i];
                    element.Measure(availableSize);

                    UIElementPair elements;

                    if (GetSpan(element) || i + 1 >= InternalChildren.Count)
                    {
                        elements = new UIElementPair(element);
                    }
                    else
                    {
                        UIElement secondElement = InternalChildren[i + 1];
                        secondElement.Measure(availableSize);

                        elements = new UIElementPair(element, secondElement);

                        i++;
                    }

                    this.grid.Add(elements);
                }

                this.grid.MeasureHeight(availableSize.Height);

                if (RememberMeasure)
                {
                    this.remeasure = false;
                }
            }
            
            return new Size(this.grid.Width, this.grid.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double collectionLeft = 0;

            foreach (UIElementPairCollection collection in this.grid)
            {
                double pairTop = 0;

                foreach (UIElementPair pair in collection)
                {
                    pair.LeftElement.Arrange(new Rect(new Point(collectionLeft, pairTop), pair.LeftElement.DesiredSize));

                    if (pair.RightElement != null)
                    {
                        pair.RightElement.Arrange(new Rect(new Point(collectionLeft + collection.LeftColumnWidth + ColumnPadding, pairTop), pair.RightElement.DesiredSize));
                    }

                    pairTop += RowPadding;
                    pairTop += pair.Height;
                }

                collectionLeft += ColumnPadding;
                collectionLeft += collection.Width;
            }

            return finalSize;
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            this.remeasure = true;
            base.OnChildDesiredSizeChanged(child);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            this.remeasure = true;
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.remeasure = true;
            base.OnVisualParentChanged(oldParent);
        }
    }
}
