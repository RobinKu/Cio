﻿/*
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
using System.Windows.Data;

namespace Cio.UI.Wpf.ElementFactories
{
	public class CheckBoxFactory : WpfElementFactory<CheckBox>
	{
		public override CheckBox CreateElement(string renderMode)
		{
			return new CheckBox();
		}
		
		public override CheckBox CreateElement(object objectToRender, string rendermode)
		{
			CheckBox cb = this.CreateElement(rendermode);
			cb.Content = objectToRender;
			
			return cb;
		}
		
<<<<<<< HEAD:Cio.UI.Wpf/ElementFactories/CheckBoxFactory.cs
		public override CheckBox CreateElement(object source, string bindingPath, string rendermode)
		{
			CheckBox cb = this.CreateElement(rendermode);
			BindingUtility.AddBinding(cb, CheckBox.IsCheckedProperty, source, bindingPath);
=======
		public override FrameworkElement CreateElement(Type sourceType, string bindingPath, string rendermode)
		{
			Binding binding = new Binding(bindingPath);
			
			FrameworkElement cb = (FrameworkElement)CreateElement(rendermode);
			cb.SetBinding(CheckBox.IsCheckedProperty, binding);
>>>>>>> origin/grid:Cio.UI.Wpf/BooleanElementFactory.cs
			
			return cb;
		}
	}
}
