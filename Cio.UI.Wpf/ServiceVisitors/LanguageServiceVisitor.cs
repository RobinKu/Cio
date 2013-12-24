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
using System.Windows;
using System.Windows.Markup;

namespace Cio.UI.Wpf.ServiceVisitors
{
	public class LanguageServiceVisitor : DefaultSingleServiceVisitor<ILanguageService>
	{
		public LanguageServiceVisitor()
		{
			SetEditorElementType<FrameworkElement>();
		}
		
		protected override void Visit(object labelElement, object editorElement, object source, string bindingPath, string renderMode, ILanguageService service)
		{
			FrameworkElement element = (FrameworkElement)editorElement;
			
			element.Language = XmlLanguage.GetLanguage(service.GetCulture().Name);
		}
	}
}
