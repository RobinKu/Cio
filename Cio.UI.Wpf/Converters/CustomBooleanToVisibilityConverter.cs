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
using System.Windows.Data;

namespace Cio.UI.Wpf.Converters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class CustomBooleanToVisibilityConverter : IValueConverter
	{
		public CustomBooleanToVisibilityConverter()
			: this(false)
		{
		}
		
		public CustomBooleanToVisibilityConverter(bool inverse)
		{
			this.Inverse = inverse;
		}
		
		public bool Inverse
		{
			get;
			set;
		}
		
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool boolValue = System.Convert.ToBoolean(value);
			
			if (Inverse)
			{
				boolValue = !boolValue;
			}
			
			return boolValue ? Visibility.Visible : Visibility.Hidden;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Visibility visibilityValue = (Visibility)value;
			
			bool boolValue = visibilityValue == Visibility.Visible;
			
			if (Inverse)
			{
				boolValue = !boolValue;
			}
			
			return boolValue;
		}
	}
}
