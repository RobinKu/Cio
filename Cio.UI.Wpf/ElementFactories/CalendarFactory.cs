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
using System.Windows.Controls;

namespace Cio.UI.Wpf.ElementFactories
{
	public class CalendarFactory : WpfElementFactory<Calendar>
	{
		public override Calendar CreateElement(string renderMode)
		{
			Calendar cal = new Calendar();
			
			return cal;
		}
		
		public override Calendar CreateElement(object objectToRender, string rendermode)
		{
			Calendar cal = this.CreateElement(rendermode);
			
			cal.DisplayDate = Convert.ToDateTime(objectToRender);
			
			return cal;
		}
		
		public override Calendar CreateElement(object source, string bindingPath, string rendermode)
		{
			Calendar cal = this.CreateElement(rendermode);
			
			BindingUtility.AddBinding(cal, Calendar.DisplayDateProperty, source, bindingPath);
			
			return cal;
		}
	}
}
