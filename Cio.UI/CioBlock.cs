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

namespace Cio.UI
{
	public class CioBlock<TBuilder> : IServiceRegistrable, IServiceVisitorRegistrable
		where TBuilder : IBlockBuilder
	{
		private CioConfiguration config;
		private TBuilder builder;
		private ICollection<IServiceVisitor> serviceVisitors = new List<IServiceVisitor>();
		private ICollection<object> services = new List<object>();
		private ICollection<AddInformation> items = new List<AddInformation>();
		
		public CioBlock(CioConfiguration config, TBuilder builder)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			else if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			
			this.config = config;
			this.builder = builder;
			
			this.builder.Added += new EventHandler<AddedEventArgs>(ExecuteServiceVisitorsAfterAdd);
		}
		
		protected TBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}
		
		private IEnumerable<object> GeneralServices
		{
			get
			{
				return this.services.Concat(this.config.Services);
			}
		}
		
		private IEnumerable<IServiceVisitor> ServiceVisitors
		{
			get
			{
				return this.serviceVisitors.Concat(this.config.ServiceVisitors);
			}
		}
		
		internal IEnumerable<AddInformation> GetAddInformation()
		{
			return this.items;
		}
		
		public void Add(AddInformation info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			
			this.items.Add(info);
		}
		
		public virtual object Render()
		{
			object block = this.builder.CreateBlock();
			
			foreach (AddInformation info in this.items)
			{
				info.AddTo = block;
				
				this.builder.Add(info);
			}
			
			return block;
		}
		
		private void ExecuteServiceVisitorsAfterAdd(object sender, AddedEventArgs ev)
		{
			AddInformation info = ev.AddInformation;
			
			IEnumerable<object> services = info.Services;
			IEnumerable<object> allServices = services.Concat(GeneralServices);
			
			if (allServices.Any())
			{
				foreach (IServiceVisitor visitor in ServiceVisitors)
				{
					visitor.Visit(info, ev.Result, allServices);
				}
			}
		}
		
		public void RegisterService(object service)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			
			this.services.Add(service);
		}
		
		public void UnregisterService(object service)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			
			this.services.Remove(service);
		}
		
		public void RegisterServiceVisitor(IServiceVisitor serviceVisitor)
		{
			if (serviceVisitor == null)
			{
				throw new ArgumentNullException("serviceVisitor");
			}
			
			this.serviceVisitors.Add(serviceVisitor);
		}
		
		public void UnregisterServiceVistor(IServiceVisitor serviceVisitor)
		{
			if (serviceVisitor == null)
			{
				throw new ArgumentNullException("serviceVisitor");
			}
			
			this.serviceVisitors.Remove(serviceVisitor);
		}
	}
}
