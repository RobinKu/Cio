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
	public abstract class DefaultServiceVisitor<TService> : IServiceVisitor
	{
		private Type labelElementType;
		private Type editorElementType;
		private string renderModeFilter;
		
		protected void SetLabelElementType<T>()
		{
			this.labelElementType = typeof(T);
		}
		
		protected void SetEditorElementType<T>()
		{
			this.editorElementType = typeof(T);
		}
		
		protected void SetRendeModeFilter(string renderModeFilter)
		{
			this.renderModeFilter = renderModeFilter;
		}
		
		public void Visit(object labelElement, object editorElement, object source, string bindingPath, string renderMode, IEnumerable<object> services)
		{
			if (labelElement == null)
			{
				throw new ArgumentNullException("labelElement");
			}
			else if (editorElement == null)
			{
				throw new ArgumentNullException("editorElement");
			}
			else if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			
			if (ValidateElementType(labelElement.GetType(), this.labelElementType) &&
			    ValidateElementType(editorElement.GetType(), this.editorElementType) &&
			    ValidateRenderMode(renderMode))
			{
				IEnumerable<TService> typedServices = services
					.OfType<TService>();
				
				if (typedServices.Any())
				{
					Visit(labelElement, editorElement, source, bindingPath, renderMode, typedServices);
				}
			}
		}
		
		private bool ValidateElementType(Type elementType, Type neededType)
		{
			return neededType == null || neededType.IsAssignableFrom(elementType);
		}
		
		private bool ValidateRenderMode(string renderMode)
		{
			return string.IsNullOrWhiteSpace(this.renderModeFilter) || this.renderModeFilter == renderMode;
		}
		
		protected abstract void Visit(object labelElement, object editorElement, object source, string bindingPath, string renderMode, IEnumerable<TService> services);
	}
}
