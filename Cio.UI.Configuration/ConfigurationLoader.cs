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
using System.Configuration;
using System.Linq;

using Cio.UI;

namespace Cio.UI.Configuration
{
	public static class ConfigurationLoader
	{
		public static CioConfiguration Load()
		{
			return Load("cio/uiSettings");
		}
		
		public static CioConfiguration Load(string sectionName)
		{
			CioConfigurationSection section = (CioConfigurationSection)ConfigurationManager.GetSection(sectionName);
			
			Type elementsConfigType = section.ElementsConfigType;
			
			IElementConfiguration elementConfig = GetElementConfiguration(elementsConfigType);
			
			CioConfiguration config = new CioConfiguration(elementConfig);
			
			foreach (IServiceVisitor visitor in GetServiceVisitors(section.ServiceVistors))
			{
				config.RegisterServiceVisitor(visitor);
			}
			
			foreach (object service in GetServices(section.Services))
			{
				config.RegisterService(service);
			}
			
			return config;
		}
		
		private static IElementConfiguration GetElementConfiguration(Type elementsConfigType)
		{
			try
			{
				return (IElementConfiguration)Activator.CreateInstance(elementsConfigType);
			}
			catch (InvalidCastException ex)
			{
				string exceptionMessage = string.Format("The type {0} in the elementConfiguration attribute is not castable to Cio.UI.IElementConfiguration.", elementsConfigType);
				
				throw new ConfigurationException(exceptionMessage, ex);
			}
		}
		
		private static IEnumerable<IServiceVisitor> GetServiceVisitors(ServiceVisitorsCollection visitorCollection)
		{
			foreach (ServiceVisitorElement visitorElement in visitorCollection)
			{
				Type visitorType = visitorElement.Type;
				
				IServiceVisitor serviceVisitor;
				
				try
				{
					serviceVisitor = (IServiceVisitor)Activator.CreateInstance(visitorType);
				}
				catch (InvalidCastException ex)
				{
					string exceptionMessage = string.Format("The service visitor type {0} is not castable to Cio.UI.IServiceVisitor.", visitorType);
				
					throw new ConfigurationException(exceptionMessage, ex);
				}
				
				yield return serviceVisitor;
			}
		}
		
		private static IEnumerable<object> GetServices(ServiceCollection serviceCollection)
		{
			foreach (ServiceElement serviceElement in serviceCollection)
			{
				Type serviceType = serviceElement.Type;
				
				yield return Activator.CreateInstance(serviceType);
			}
		}
	}
}
