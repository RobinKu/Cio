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

namespace Cio.UI
{
	public class CioConfiguration
	{
		private IList<IServiceVisitor> visitors = new List<IServiceVisitor>();
		private IList<object> services = new List<object>();
		
		public CioConfiguration()
		{
		}
		
		public CioConfiguration(IElementConfiguration elementConfig)
		{
			if (elementConfig == null)
			{
				throw new ArgumentNullException("elementConfig");
			}
			
			this.Elements = elementConfig;
		}
		
		public IElementConfiguration Elements
		{
			get;
			set;
		}
		
		public void RegisterServiceVisitor(IServiceVisitor visitor)
		{
			this.visitors.Add(visitor);
		}
		
		public void UnregisterServiceVisitor(IServiceVisitor visitor)
		{
			this.visitors.Remove(visitor);
		}
		
		public IEnumerable<IServiceVisitor> ServiceVisitors
		{
			get
			{
				return this.visitors;
			}
		}
		
		public void RegisterService(object service)
		{
			this.services.Add(service);
		}
		
		public void UnregisterService(object service)
		{
			this.services.Remove(service);
		}
		
		public IEnumerable<object> Services
		{
			get
			{
				return this.services;
			}
		}
	}
}
