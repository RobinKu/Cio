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
using System.Collections.Generic;
using System.Linq;

namespace Cio.UI.Wpf
{
	internal class UIElementPairCollection : List<UIElementPair>
    {
        private ColumnWrapPanel panel;

        public UIElementPairCollection(ColumnWrapPanel panel)
        {
            if (panel == null)
            {
                throw new ArgumentNullException("panel");
            }

            this.panel = panel;
        }

        public double ColumnPadding
        {
            get
            {
                return this.panel.ColumnPadding;
            }
        }

        public double RowPadding
        {
            get
            {
                return this.panel.RowPadding;
            }
        }

        public double LeftColumnWidth
        {
            get
            {
                double width = 0;
                IEnumerable<UIElementPair> pairs = this.Where(e => e.RightElement != null);

                if (pairs.Any())
                {
                    width = pairs.Max(e => e.LeftElement.DesiredSize.Width);
                }

                return width;
            }
        }

        public double RightColumnWidth
        {
            get
            {
                double rightColumnWidth = 0;
                IEnumerable<UIElementPair> pairs = this.Where(e => e.RightElement != null);

                if (pairs.Any())
                {
                    rightColumnWidth = pairs.Max(e => e.RightElement.DesiredSize.Width);
                }

                if (this.Any(e => e.RightElement == null))
                {
                    double combinedWidth = this.Where(e => e.RightElement == null).Max(e => e.LeftElement.DesiredSize.Width);
                    double combinedWidthMinusLeftColumn = combinedWidth - LeftColumnWidth;

                    if (rightColumnWidth < combinedWidthMinusLeftColumn)
                    {
                        rightColumnWidth = combinedWidthMinusLeftColumn;
                    }
                }

                return rightColumnWidth;
            }
        }

        public double Width
        {
            get
            {
                return (LeftColumnWidth + ColumnPadding + RightColumnWidth);
            }
        }

        public double Height
        {
            get
            {
                double height = 0;

                if (this.Any())
                {
                    height += this[0].Height;

                    height += this.Skip(1).Sum(e => e.Height + RowPadding);
                }

                return height;
            }
        }
    }
}
