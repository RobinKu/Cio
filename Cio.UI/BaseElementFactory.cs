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
	public abstract class BaseElementFactory<TBinding, TElementBase> : IElementFactory<TBinding, TElementBase>
	{
		public abstract TElementBase CreateElement(object source, string bindingPath, string rendermode, IEditableService editableService, IDisplayNameService displayNameService);
		
		object IElementFactory<TBinding>.CreateElement(object source, string bindingPath, string rendermode, IEditableService editableService, IDisplayNameService displayNameService)
		{
			return this.CreateElement(source, bindingPath, rendermode, editableService, displayNameService);
		}
	}
}
