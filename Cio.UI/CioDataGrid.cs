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

namespace Cio.UI
{
	public class CioDataGrid<T> : CioBlock<IDataGridBuilder>
	{
		public CioDataGrid(CioConfiguration config, IDataGridBuilder gridBuilder)
			: base(config, gridBuilder)
		{
		}

		public void Add(string bindingPath, string rendermode = null, params object[] services)
		{
			FieldBindingInfo info = new FieldBindingInfo();
			info.BindingPath = bindingPath;
			info.Rendermode = rendermode;
			info.Services = services;
			
			this.fieldInfos.Add(info);
		}
		
		public void Add<TReturn>(Expression<Func<T, TReturn>> property, string rendermode = null, params object[] services)
		{
			try
			{
				IEnumerable<string> propertyNames = PropertyUtil.GetPropertyNames(property);
				
				string bindingPath = string.Join(".", propertyNames.Reverse());
				
				this.Add(bindingPath, rendermode, services);
			}
			catch (InvalidPropertyExpressionException ex)
			{
				throw new InvalidBindingPathException("Binding paths may only consist of properties.", ex);
			}
		}
	}
}
