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
		
		public event EventHandler<AddedEventArgs> Added;

		public object CreateBlock()
		{
			ItemsControl panel = new ItemsControl();
			
			FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(ColumnWrapPanel));
            panel.ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);
            
            return panel;
		}
		
		public object Add(AddInformation info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			
			BindingInformation bInfo;
			
			try
			{
				bInfo = (BindingInformation)info;
			}
			catch (InvalidCastException ex)
			{
				throw new ArgumentException("This BlockBuilder can only process info of type BindingInformation.", ex);
			}
			
			return this.Add(bInfo);
		}
		
		public object Add(BindingInformation info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			
			ItemsControl panel = ValidateBlockAsItemsControl(info.AddTo);
			
			Type sourceType = info.SourceType;
			string bindingPath = info.BindingPath;
			string rendermode = info.RenderMode;
			PropertyInfo property = BindingPathUtility.GetProperty(sourceType, bindingPath);
			
			IElementFactory labelFactory = this.resolver.Resolve<string>(RenderModes.EditorLabel);
			object labelElement = labelFactory.CreateElement(RenderModes.EditorLabel);
			
			IElementFactory editorFactory = this.resolver.Resolve(property.PropertyType, rendermode);
			object editorElement = editorFactory.CreateElement(sourceType, bindingPath, rendermode);
			
			panel.Items.Add(labelElement);
			panel.Items.Add(editorElement);
			
			FormResult result = new FormResult(labelElement, editorElement);
			OnAdded(info, result);
			
			return editorElement;
		}
		
		private void OnAdded(AddInformation info, IResult result)
		{
			EventHandler<AddedEventArgs> handler = Added;
			
			if (handler != null)
			{
				AddedEventArgs ev = new AddedEventArgs(info, result);
				
				handler(this, ev);
			}
		}
		
		public void Bind(object block, object bindableOBject)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			
			ItemsControl panel = ValidateBlockAsItemsControl(block);
			
			panel.DataContext = bindableOBject;
		}
		
		private ItemsControl ValidateBlockAsItemsControl(object block)
		{
			ItemsControl panel = block as ItemsControl;
			
			if (panel == null)
			{
				throw new ArgumentException("form must derive from ItemsControl. Use the CreateBlock() method of the formbuilder to create the right type.");
			}
			
			return panel;
		}
	}
}
