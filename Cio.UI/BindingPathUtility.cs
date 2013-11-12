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
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cio.UI
{
	public static class BindingPathUtility
	{
		public static void CheckBindingPath(string bindingPath)
		{
			if(!Regex.IsMatch(bindingPath, @"^[a-zA-Z0-9]+(\.[a-zA-Z0-9]*)*$"))
			{
				throw new InvalidBindingPathException("The provided bindingPath could not be validated. Remember that method are not supported.");
			}
		}
		
		public static IEnumerable<PropertyInfo> GetProperties(Type sourceType, string bindingPath)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			
			CheckBindingPath(bindingPath);
			
			string[] propertyNames = bindingPath.Split('.');
			
			Type currentType = sourceType;
			
			foreach (string propertyName in propertyNames)
			{
				PropertyInfo info = currentType.GetProperty(propertyName);
				
				if (info == null)
				{
					throw new InvalidBindingPathException(string.Format("The method {0} does not exist on type {1}", propertyName, currentType));
				}
				
				yield return info;
			}
		}
		
		public static PropertyInfo GetProperty(Type sourceType, string bindingPath)
		{
			return GetProperties(sourceType, bindingPath).Last();
		}
		
		public static PropertyInfo GetProperty(object source, string bindingPath)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			
			return GetProperty(source.GetType(), bindingPath);
		}
	}
}
