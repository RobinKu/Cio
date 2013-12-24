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
	public class ElementPairAddedEventArgs : EventArgs
	{
		private object labelElement;
		private object editorElement;
		private object source;
		private FieldBindingInfo bindingInfo;
		
		public ElementPairAddedEventArgs(object labelElement, object editorElement, object source, FieldBindingInfo bindingInfo)
		{
			this.labelElement = labelElement;
			this.editorElement = editorElement;
			this.source = source;
			this.bindingInfo = bindingInfo;
		}
		
		public object LabelElement
		{
			get
			{
				return this.labelElement;
			}
		}
		
		public object EditorElement
		{
			get
			{
				return this.editorElement;
			}
		}
		
		public object Source
		{
			get
			{
				return this.source;
			}
		}
		
		public FieldBindingInfo BindingInfo
		{
			get
			{
				return this.bindingInfo;
			}
		}
	}
}
