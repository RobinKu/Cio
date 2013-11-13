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
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Cio.UI.Wpf
{
	public class WpfFormBuilder : IFormBuilder
	{
		private ItemsControl panel = new ItemsControl();
		private IElementResolver resolver;
		
		public WpfFormBuilder(IElementResolver resolver)
		{
			if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}
			
			this.resolver = resolver;
			
			FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(ColumnWrapPanel));
            this.panel.ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);
		}
		
		public object Form
		{
			get
			{
				return this.panel;
			}
		}
		
		public object Add(object source, string bindingPath, string rendermode, IEditableService editableService, IDisplayNameService displayNameService)
		{
			string displayName = displayNameService.GetDisplayName(source, bindingPath);
			PropertyInfo property = BindingPathUtility.GetProperty(source, bindingPath);
			
			IElementFactory labelFactory = this.resolver.Resolve<string>(RenderModes.EditorLabel);
			
			object labelElement = labelFactory.CreateElement(displayName, RenderModes.EditorLabel);
			this.panel.Items.Add(labelElement);
			
			IElementFactory editorFactory = this.resolver.Resolve(property.PropertyType, rendermode);
			
			object editorElement = editorFactory.CreateElement(source, bindingPath, rendermode, editableService);
			this.panel.Items.Add(editorElement);
			
			return editorElement;
		}
	}
}
