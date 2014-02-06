//#define use_config_file
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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using Cio.UI;
using Cio.UI.Composition.Default;
using Cio.UI.Configuration;
using Cio.UI.Services;
using Cio.UI.Wpf;
using Cio.UI.Wpf.ServiceVisitors;

namespace TestApp
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
			
			InitializeComponent();
			
#if use_config_file
			CioConfiguration config = ConfigurationLoader.Load();
#else
			CioConfiguration config = new CioConfiguration();
			config.RegisterServiceVisitor(new DisplayNameServiceVisitor());
			config.RegisterServiceVisitor(new EditableServiceVisitor());
			config.RegisterService(CreateDisplayNameService());
			
			config.Elements = new ElementConfiguration();
#endif
			config.Elements.RegisterDefaultControls();
			
			IElementResolver resolver = config.Elements.CreateResolver();
			
			IFormBuilder formBuilder = new WpfFormBuilder(resolver);
			
			var form = new UserForm(config, formBuilder);
			
			User user = CreateUser();
			
			grid.Children.Add((UIElement)form.Render(user));
		}
		
		private User CreateUser()
		{
			User user = new User();
			user.Name = "pidon";
			user.Password = "1234567890";
			user.Signature = @"This is a signature
on multiple lines.";
			user.IsActive = true;
			user.DateOfBirth = new DateTime(1986, 5, 1);
			
			return user;
		}
		
		private IDisplayNameService CreateDisplayNameService()
		{
			IDisplayNameService propertyNameDisplayService = new PropertyDisplayNameService();
			
			return new AttributedDisplayNameService(propertyNameDisplayService);
		}
	}
}