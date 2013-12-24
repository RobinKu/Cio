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
		private IElementResolver resolver;

		public WpfFormBuilder(IElementResolver resolver)
		{
			if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}
			
			this.resolver = resolver;
		}
		
		public event EventHandler<ElementPairAddedEventArgs> ElementPairAdded;

		public object CreateForm()
		{
			ItemsControl panel = new ItemsControl();
			
			FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(ColumnWrapPanel));
            panel.ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);
            
            return panel;
		}
		
		public object Add(object form, object source, FieldBindingInfo bindingInfo)
		{
			if (form == null)
			{
				throw new ArgumentNullException("form");
			}
			else if (bindingInfo == null)
			{
				throw new ArgumentNullException("bindingInfo");
			}
			
			ItemsControl panel = form as ItemsControl;
			
			if (panel == null)
			{
				throw new ArgumentException("form must derive from ItemsControl. Use the CreateForm() method of the formbuilder to create the right type.");
			}
			
			string bindingPath = bindingInfo.BindingPath;
			string rendermode = bindingInfo.Rendermode;
			PropertyInfo property = BindingPathUtility.GetProperty(source, bindingPath);
			
			IElementFactory labelFactory = this.resolver.Resolve<string>(RenderModes.EditorLabel);
			object labelElement = labelFactory.CreateElement(RenderModes.EditorLabel);
			
			IElementFactory editorFactory = this.resolver.Resolve(property.PropertyType, rendermode);
			object editorElement = editorFactory.CreateElement(source, bindingPath, rendermode);
			
			panel.Items.Add(labelElement);
			panel.Items.Add(editorElement);
			
			OnElementPairAdded(labelElement, editorElement, source, bindingInfo);
			
			return editorElement;
		}
		
		private void OnElementPairAdded(object labelElement, object editorElement, object source, FieldBindingInfo bindingInfo)
		{
			EventHandler<ElementPairAddedEventArgs> handler = ElementPairAdded;
			
			if (handler != null)
			{
				ElementPairAddedEventArgs ev = new ElementPairAddedEventArgs(labelElement, editorElement, source, bindingInfo);
				
				handler(this, ev);
			}
		}
	}
}
