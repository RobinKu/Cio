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
	internal class UIElementGrid : List<UIElementPairCollection>
    {
        private ColumnWrapPanel panel;

        public UIElementGrid(ColumnWrapPanel panel)
        {
            if (panel == null)
            {
                throw new ArgumentNullException("panel");
            }

            this.panel = panel;

            this.Add(new UIElementPairCollection(this.panel));
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

        public double Height
        {
            get
            {
                return this.Max(c => c.Height);
            }
        }

        public double Width
        {
            get
            {
                double width = 0;

                if (this.Any())
                {
                    width += this[0].Width;
                    width += this.Skip(1).Sum(c => c.Width + ColumnPadding);
                }

                return width;
            }
        }

        public void Add(UIElementPair elementPair)
        {
            this.Last().Add(elementPair);
        }

        public void MeasureHeight(double height)
        {
            while (Height > height)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Height > height)
                    {
                        UIElementPair pair = this[i].Last();

                        this[i].Remove(pair);
                        if (i + 1 >= this.Count)
                        {
                            this.Add(new UIElementPairCollection(this.panel));
                        }

                        this[i + 1].Insert(0, pair);
                    }
                }
            }
        }

        public void MeasureWidth(double width)
        {
            while (Width > width)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (i + 1 < this.Count)
                    {
                        for (int a = 0; a < i; a++)
                        {
                            if (this[i + 1].Any())
                            {
                                UIElementPair pair = this[i + 1].First();

                                this[i + 1].Remove(pair);
                                this[i].Add(pair);
                            }
                        }
                    }
                }
            }
        }
    }
}
