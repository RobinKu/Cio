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
using System.Threading;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Cio.UI.Wpf.ElementFactories
{
	public class TextBoxFactory : WpfElementFactory<TextBox>
	{
		public override TextBox CreateElement(string renderMode)
		{
			TextBox tb = new TextBox();
			tb.Width = 200;
			
			switch (renderMode)
			{
				case RenderModes.Multiline:
					tb.Height = 80;
					tb.AcceptsReturn = true;
					break;
				case RenderModes.Readonly:
					tb.IsReadOnly = true;
					break;
			}
			
			return tb;
		}
		
		public override TextBox CreateElement(object objectToRender, string rendermode)
		{
			TextBox tb = CreateElement(rendermode);
			tb.Text = Convert.ToString(objectToRender);
			
			return tb;
		}
		
		public override TextBox CreateElement(object source, string bindingPath, string rendermode)
		{
			TextBox tb = CreateElement(rendermode);
			
			BindingUtility.AddBinding(tb, TextBox.TextProperty, source, bindingPath);
			
			return tb;
		}
	}
}
