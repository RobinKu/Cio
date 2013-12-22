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
using Cio.Collections.Generic;
using Cio.UI;

namespace Cio.UI.Composition.Default
{
	public class ElementConfiguration : IElementConfiguration
	{
		private IDictionary<Tuple<Type, string>, Type> elementFactoryTypes;
		private IList<IServiceVisitor> serviceVisitors = new List<IServiceVisitor>();
		
		public ElementConfiguration()
		{
			this.elementFactoryTypes = new Dictionary<Tuple<Type, string>, Type>();
		}
		
		public IElementResolver CreateResolver()
		{
			return new ElementResolver(this);
		}
		
		public void RegisterType(Type bindingType, Type elementFactoryType)
		{
			this.RegisterType(bindingType, elementFactoryType, null);
		}
		
		public void RegisterType(Type bindingType, Type elementFactoryType, string rendermode)
		{
			if (bindingType == null)
			{
				throw new ArgumentNullException("bindingType");
			}
			else if (elementFactoryType == null)
			{
				throw new ArgumentNullException("elementFactoryType");
			}
			
			this.elementFactoryTypes.AddOrEdit(Tuple.Create(bindingType, rendermode), elementFactoryType);
		}
		
		internal IDictionary<Tuple<Type, string>, Type> ElementFactoryTypes
		{
			get
			{
				return this.elementFactoryTypes;
			}
		}
		
		public void RegisterServiceVisitor(IServiceVisitor serviceVisitor)
		{
			throw new NotImplementedException();
		}
	}
}
