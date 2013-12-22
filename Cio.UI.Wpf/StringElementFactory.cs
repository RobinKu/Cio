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
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Cio.UI.Wpf
{
	public class StringElementFactory : WpfElementFactory
	{
		public override FrameworkElement CreateElement(string renderMode)
		{
			return new TextBlock();
		}
		
		public override FrameworkElement CreateElement(object objectToRender, string rendermode)
		{
			TextBlock txt = (TextBlock)CreateElement(rendermode);
			txt.Text = objectToRender.ToString();
			
			return txt;
		}
		
		public override FrameworkElement CreateElement(object source, string bindingPath, string rendermode)
		{
			Binding binding = new Binding(bindingPath);
			binding.Source = source;
			
			FrameworkElement element = null;
			DependencyProperty dp = null;
			
			switch (rendermode)
			{
				case RenderModes.Readonly:
				case RenderModes.EditorLabel:
					element = new TextBlock();
					dp = TextBlock.TextProperty;
					break;
				case null:
				case RenderModes.Multiline:
					TextBox tb = new TextBox();
					tb.Width = 200;
					
					if (rendermode == RenderModes.Multiline)
					{
						tb.Height = 80;
					}
					
					element = tb;
					dp = TextBox.TextProperty;
					break;
			}
			
			element.SetBinding(dp, binding);
			
			return element;
		}
	}
}