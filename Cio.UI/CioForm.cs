﻿/*
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
using System.Linq.Expressions;
using System.Reflection;
using Cio.Reflection;

namespace Cio.UI
{
	public class CioForm<T> : CioBindableBlock<IFormBuilder, T, T>
	{
		public CioForm(CioConfiguration config, IFormBuilder formBuilder)
			: base (config, formBuilder)
		{
		}
		
		public CioForm(CioConfiguration config, IFormBuilder formBuilder, T objectToRender)
			: base(config, formBuilder, objectToRender)
		{
		}
		
		protected override BasicBindingInformation CreateBindingInformation(string bindingPath, string rendermode, IEnumerable<object> services)
		{
			BindingInformation info = new BindingInformation();
			info.Source = this.BindableObject;
			
			return info;
		}
	}
}
