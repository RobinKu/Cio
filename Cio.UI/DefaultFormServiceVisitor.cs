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
	public abstract class DefaultFormServiceVisitor : DefaultServiceVisitor
	{
		private Type labelElementType;
		private Type editorElementType;
		private string renderModeFilter;
		
		protected DefaultFormServiceVisitor()
		{
			this.SetInformationType<BindingInformation>();
			this.SetResultType<FormResult>();
		}
		
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
		
		protected sealed override void VisitInternal(AddInformation info, IResult result, IEnumerable<object> services)
		{
			BindingInformation bInfo = (BindingInformation)info;
			FormResult res = (FormResult)result;
			
			if (res.LabelElement == null)
			{
				throw new ArgumentNullException("labelElement");
			}
			else if (res.EditorElement == null)
			{
				throw new ArgumentNullException("editorElement");
			}
			
			if (ValidateElementType(res.LabelElement.GetType(), this.labelElementType) &&
			    ValidateElementType(res.EditorElement.GetType(), this.editorElementType) &&
			    ValidateRenderMode(bInfo.RenderMode))
			{
				VisitInternal(bInfo, res, services);
			}
		}
		
		protected abstract void VisitInternal(BindingInformation info, FormResult result, IEnumerable<object> services);
		
		private bool ValidateElementType(Type elementType, Type neededType)
		{
			return (neededType == null || elementType.IsEqualOrDerivedFrom(neededType));
		}
		
		private bool ValidateRenderMode(string renderMode)
		{
			return string.IsNullOrWhiteSpace(this.renderModeFilter) || this.renderModeFilter == renderMode;
		}
	}
}
