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
using Cio.UI.Wpf.ElementFactories;

namespace Cio.UI.Wpf
{
	public static class ElementConfigurationExtensions
	{
		public static IElementConfiguration RegisterDefaultControls(this IElementConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			
			configuration.RegisterType(typeof(string), typeof(TextBoxFactory));
			configuration.RegisterType(typeof(string), typeof(TextBlockFactory), RenderModes.EditorLabel);
			configuration.RegisterType(typeof(string), typeof(TextBlockFactory), RenderModes.Readonly);
			configuration.RegisterType(typeof(string), typeof(TextBoxFactory), RenderModes.Multiline);
			configuration.RegisterType(typeof(bool), typeof(CheckBoxFactory));
			configuration.RegisterType(typeof(DateTime), typeof(CalendarFactory));
			configuration.RegisterType(typeof(DateTime), typeof(CalendarFactory), RenderModes.DateOnly);
			
			return configuration;
		}
	}
}
