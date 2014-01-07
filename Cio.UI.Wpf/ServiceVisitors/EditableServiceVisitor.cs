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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Cio.Reflection;
using Cio.UI;
using Cio.UI.Services;
using Cio.UI.Wpf.Converters;

namespace Cio.UI.Wpf.ServiceVisitors
{
	public class EditableServiceVisitor : DefaultSingleFormServiceVisitor
	{
		public EditableServiceVisitor()
		{
			SetServiceType<IEditableService>();
			SetEditorElementType<FrameworkElement>();
		}
		
		protected override void VisitInternal(BindingInformation info, FormResult result, object service)
		{
			IEditableService typedService = (IEditableService)service;
			
			FrameworkElement element = (FrameworkElement)result.EditorElement;
			
			string editablePropertyName = PropertyUtil<IEditableService>.GetPropertyName(s => s.Editable);
			string reasonPropertyName = PropertyUtil<IEditableService>.GetPropertyName(s => s.DisabledReason);
			
			Binding enabledBinding = new Binding(editablePropertyName);
			enabledBinding.Source = service;
			
			element.SetBinding(UIElement.IsEnabledProperty, enabledBinding);
			
			Binding tooltipVisibleBinding = new Binding(editablePropertyName);
			tooltipVisibleBinding.Source = service;
			tooltipVisibleBinding.Converter = new CustomBooleanToVisibilityConverter(true);
			
			Binding tooltipTextBinding = new Binding(reasonPropertyName);
			tooltipTextBinding.Source = service;
			
			ToolTip tooltip = new ToolTip();
			tooltip.SetBinding(UIElement.VisibilityProperty, tooltipVisibleBinding);
			tooltip.SetBinding(ContentControl.ContentProperty, tooltipTextBinding);
			
			element.ToolTip	= tooltip;
		}
	}
}
