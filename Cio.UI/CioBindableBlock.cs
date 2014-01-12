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
using Cio.Reflection;

namespace Cio.UI
{
	public abstract class CioBindableBlock<TBuilder, TBindableObject, TBindingBase> : CioBlock<TBuilder>
		where TBuilder : IBindableBlockBuilder
	{
		public CioBindableBlock(CioConfiguration config, TBuilder builder)
			: base(config, builder)
		{
		}
		
		protected abstract BasicBindingInformation CreateBindingInformation(string bindingPath, string rendermode, IEnumerable<object> services);
		
		public void Add(string bindingPath, string rendermode = null, params object[] services)
		{
			BasicBindingInformation info = CreateBindingInformation(bindingPath, rendermode, services);
			info.BindingPath = bindingPath;
			info.RenderMode = rendermode;
			info.Services = services;
			
			this.Add(info);
		}
		
		public void Add<TReturn>(Expression<Func<TBindingBase, TReturn>> property, string rendermode = null, params object[] services)
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
		
		public object Render(TBindableObject obj)
		{
			object block = this.Render();
			
			this.Builder.Bind(block, obj);
			
			return block;
		}
		
		public void Rebind(object block, TBindableObject obj)
		{
			this.Builder.Bind(block, obj);
		}
	}
}
