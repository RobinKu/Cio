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
using System.Linq.Expressions;
using System.Reflection;

namespace Cio.Reflection
{
	public static class PropertyUtil
	{
		public static IEnumerable<string> GetPropertyNames<T, TResult>(Expression<Func<T, TResult>> property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			
			ICollection<string> propertyNames = new List<string>();
			
			MemberExpression memExpr = property.Body as MemberExpression;
			
			while (memExpr != null)
			{
				if (memExpr.Member.MemberType != MemberTypes.Property)
				{
					throw new InvalidPropertyExpressionException("Only properties are allowed to be added");
				}
				
				propertyNames.Add(memExpr.Member.Name);
				
				memExpr = memExpr.Expression as MemberExpression;
			}
			
			return propertyNames;
		}
		
		public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> property)
		{
			try
			{
				return GetPropertyNames(property).Single();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidPropertyExpressionException("No property could be found in the provided expression.", ex);
			}
		}
	}
	
	public static class PropertyUtil<T>
	{
		public static IEnumerable<string> GetPropertyNames<TResult>(Expression<Func<T, TResult>> property)
		{
			return PropertyUtil.GetPropertyNames(property);
		}
		
		public static string GetPropertyName<TResult>(Expression<Func<T, TResult>> property)
		{
			return PropertyUtil.GetPropertyName(property);
		}
	}
}
