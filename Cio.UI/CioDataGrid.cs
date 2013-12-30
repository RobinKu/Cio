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
	public class CioDataGrid<T> : IServiceRegistrable, IServiceVisitorRegistrable
	{
		private CioConfiguration config;
		private IDataGridBuilder gridBuilder;
		private ICollection<FieldBindingInfo> fieldInfos = new List<FieldBindingInfo>();
		private IList<IServiceVisitor> serviceVisitors = new List<IServiceVisitor>();
		private IList<object> services = new List<object>();
		
		public CioDataGrid(CioConfiguration config, IDataGridBuilder gridBuilder)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			else if (gridBuilder == null)
			{
				throw new ArgumentNullException("gridBuilder");
			}
			
			this.config = config;
			this.gridBuilder = gridBuilder;
			
			this.gridBuilder.ColumnAdded += new EventHandler<ColumnAddedEventArgs>(ExecuteServiceVisitorsAfterAdd);
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
		
		public object RenderDataGrid(IEnumerable<T> obj)
		{
			object grid = this.gridBuilder.CreateGrid();
			
			foreach (FieldBindingInfo info in this.fieldInfos)
			{
				this.gridBuilder.Add(grid, obj, info);
			}
			
			return grid;
		}
		
		private void ExecuteServiceVisitorsAfterAdd(object sender, ColumnAddedEventArgs ev)
		{
			object labelElement = ev.LabelElement;
			object editorElement = ev.EditorElement;
			object source = ev.Source;
			string bindingPath = ev.BindingInfo.BindingPath;
			string rendermode = ev.BindingInfo.Rendermode;
			IEnumerable<object> services = ev.BindingInfo.Services;
			IEnumerable<object> allServices = services.Concat(GeneralServices);
			
			if (allServices.Any())
			{
				foreach (IServiceVisitor visitor in ServiceVisitors)
				{
					visitor.Visit(labelElement, editorElement, source, bindingPath, rendermode, allServices);
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
