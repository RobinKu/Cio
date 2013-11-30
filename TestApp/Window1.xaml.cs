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
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Cio.UI;
using Cio.UI.Composition.Default;
using Cio.UI.Wpf;

namespace TestApp
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			
			IElementConfiguration configuration = new ElementConfiguration();
			configuration.RegisterType(typeof(string), typeof(StringElementFactory));
			configuration.RegisterType(typeof(string), typeof(StringElementFactory), RenderModes.EditorLabel);
			configuration.RegisterType(typeof(string), typeof(StringElementFactory), RenderModes.Readonly);
			configuration.RegisterType(typeof(string), typeof(StringElementFactory), RenderModes.Multiline);
			configuration.RegisterType(typeof(bool), typeof(BooleanElementFactory));
			
			IElementResolver resolver = configuration.CreateResolver();
			
			DisplayNameService.Default = new AttributedDisplayNameService(DisplayNameService.Default);
			
			IFormBuilder formBuilder = new WpfFormBuilder(resolver);
			
			CioForm form = new CioForm(formBuilder);
			
			User user = CreateUser();
			form.Add(() => user.Name);
			form.Add(() => user.Password, RenderModes.Readonly);
			form.Add(() => user.Signature, RenderModes.Multiline);
			form.Add(() => user.IsActive);
			form.Add(() => user.Profile.Text);
			
			grid.Children.Add((UIElement)form.RenderForm());
		}
		
		private User CreateUser()
		{
			User user = new User();
			user.Name = "pidon";
			user.Password = "1234567890";
			user.Signature = @"This is a signature
on multiple lines.";
			user.IsActive = true;
			
			return user;
		}
	}
}