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
using System.Windows;

namespace Cio.UI.Wpf
{
internal struct UIElementPair
    {
        public UIElementPair(UIElement combinedElement)
            : this()
        {
            LeftElement = combinedElement;
            RightElement = null;
        }

        public UIElementPair(UIElement leftElement, UIElement rightElement)
            : this()
        {
            LeftElement = leftElement;
            RightElement = rightElement;
        }

        public UIElement LeftElement
        {
            get;
            private set;
        }

        public UIElement RightElement
        {
            get;
            private set;
        }

        public double Height
        {
            get
            {
                double leftHeight = 0;
                if (this.LeftElement != null)
                {
                    leftHeight = this.LeftElement.DesiredSize.Height;
                }

                double rightHeight = 0;
                if (this.RightElement != null)
                {
                    rightHeight = this.RightElement.DesiredSize.Height;
                }

                double height = leftHeight;

                if (height < rightHeight)
                {
                    height = rightHeight;
                }

                return height;
            }
        }
    }
}
