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
using System.Windows;
using System.Windows.Controls;

namespace Cio.UI.Wpf
{
	public class WpfFormBuilder : IFormBuilder
	{
		private CioConfiguration configuration;
		private IElementResolver resolver;
		private IList<IServiceVisitor> serviceVisitors = new List<IServiceVisitor>();
		private IList<object> services = new List<object>();
		
		public WpfFormBuilder(CioConfiguration configuration, IElementResolver resolver)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			else if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}
			
			this.configuration = configuration;
			this.resolver = resolver;
		}
		
		private IEnumerable<IServiceVisitor> ServiceVisitors
		{
			get
			{
				return this.serviceVisitors.Concat(this.configuration.ServiceVisitors);
			}
		}
		
		private IEnumerable<object> Services
		{
			get
			{
				return this.services.Concat(this.configuration.Services);
			}
		}
		
		public void RegisterServiceVisitor(IServiceVisitor serviceVisitor)
		{
			this.serviceVisitors.Add(serviceVisitor);
		}
		
		public void UnregisterServiceVisitor(IServiceVisitor serviceVisitor)
		{
			this.serviceVisitors.Remove(serviceVisitor);
		}
		
		public void RegisterService(object service)
		{
			this.services.Add(service);
		}
		
		public void UnregisterService(object service)
		{
			this.services.Remove(service);
		}
		
		public object CreateForm()
		{
			ItemsControl panel = new ItemsControl();
			
			FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(ColumnWrapPanel));
            panel.ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);
            
            return panel;
		}
		
		public object Add(object form, object source, string bindingPath, string rendermode, params object[] services)
		{
			if (form == null)
			{
				throw new ArgumentNullException("form");
			}
			else if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			
			ItemsControl panel = form as ItemsControl;
			
			if (panel == null)
			{
				throw new ArgumentException("form must derive from ItemsControl. Use the CreateForm() method of the formbuilder to create the right type.");
			}
			
			PropertyInfo property = BindingPathUtility.GetProperty(source, bindingPath);
			
			IElementFactory labelFactory = this.resolver.Resolve<string>(RenderModes.EditorLabel);
			object labelElement = labelFactory.CreateElement(RenderModes.EditorLabel);
			
			IElementFactory editorFactory = this.resolver.Resolve(property.PropertyType, rendermode);
			object editorElement = editorFactory.CreateElement(source, bindingPath, rendermode);
			
			if (services.Length > 0)
			{
				foreach (IServiceVisitor visitor in ServiceVisitors)
				{
					visitor.Visit(labelElement, editorElement, source, bindingPath, rendermode, services.Concat(this.Services));
				}
			}
			
			panel.Items.Add(labelElement);
			panel.Items.Add(editorElement);
			
			return editorElement;
		}
	}
}
