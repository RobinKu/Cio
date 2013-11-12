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
	public class CioForm
	{
		private IFormBuilder formBuilder;
		private ICollection<FieldBindingInfo> fieldInfos = new List<FieldBindingInfo>();
		
		public CioForm(IFormBuilder formBuilder)
		{
			if (formBuilder == null)
			{
				throw new ArgumentNullException("formBuilder");
			}
			
			this.formBuilder = formBuilder;
		}
		
		public void Add(object source, string path, string rendermode = null, IEditableService editableService = null, IDisplayNameService displayNameService = null)
		{
			FieldBindingInfo info = new FieldBindingInfo();
			info.Source = source;
			info.Path = path;
			info.Rendermode = rendermode;
			info.EditableService = editableService ?? EditableService.AlwaysEditable;
			info.DisplayNameService = displayNameService ?? DisplayNameService.Default;
			
			this.fieldInfos.Add(info);
		}
		
		public object RenderForm()
		{
			foreach (FieldBindingInfo info in this.fieldInfos)
			{
				this.formBuilder.Add(info.Source, info.Path, info.Rendermode, info.EditableService, info.DisplayNameService);
			}
			
			return this.formBuilder.Form;
		}
	}
}
