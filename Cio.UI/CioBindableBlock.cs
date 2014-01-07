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
		private TBindableObject bindableObject;
		
		public CioBindableBlock(CioConfiguration config, TBuilder builder)
			: base(config, builder)
		{
		}
		
		public CioBindableBlock(CioConfiguration config, TBuilder builder, TBindableObject obj)
			: this(config, builder)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			
			this.bindableObject = obj;
		}
		
		protected object BindableObject
		{
			get
			{
				return this.bindableObject;
			}
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
		
		public void SetBindableObject(TBindableObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			
			IEnumerable<BindingInformation> infos = this.GetAddInformation()
				.OfType<BindingInformation>();
			
			foreach (BindingInformation info in infos)
			{
				info.Source = obj;
			}
			
			this.bindableObject = obj;
		}
		
		public override object Render()
		{
			if (this.bindableObject == null)
			{
				throw new NothingToBindException("No object was given to bind to.");
			}
			
			return base.Render();
		}
		
		public object Render(TBindableObject obj)
		{
			this.SetBindableObject(obj);
			
			return this.Render();
		}
	}
}
