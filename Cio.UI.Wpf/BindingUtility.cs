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
using System.Windows.Data;

namespace Cio.UI.Wpf
{
	public static class BindingUtility
	{
		public static Binding CreateBinding(object source, string bindingPath)
		{
			Binding binding = new Binding(bindingPath);
			binding.Source = source;
			
			return binding;
		}
		
		public static void AddBinding(FrameworkElement element, DependencyProperty property, string bindingPath)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			else if (property == null)
			{
				throw new ArgumentNullException("property");
			}

            Binding binding = new Binding(bindingPath);
			
			element.SetBinding(property, binding);
		}
	}
}
