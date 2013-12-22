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
using System.Configuration;

namespace Cio.UI.Configuration
{
	public class CioConfigurationSection : ConfigurationSection
	{
		private const string elementsConfigTypeAttributeName = "elementsConfigType";
		private const string serviceVistorsAttributeName = "serviceVisitors";
		private const string serviceAttributeName = "services";
		
		public Type ElementsConfigType
		{
			get
			{
				return Type.GetType(ElementsConfigTypeName);
			}
		}
		
		[ConfigurationProperty(elementsConfigTypeAttributeName, Options = ConfigurationPropertyOptions.IsRequired)]
		public string ElementsConfigTypeName
		{
			get
			{
				return (string)this[elementsConfigTypeAttributeName];
			}
			set
			{
				this[elementsConfigTypeAttributeName] = value;
			}
		}
		
		[ConfigurationProperty(serviceVistorsAttributeName)]
	    public ServiceVisitorsCollection ServiceVistors
	    {
	        get
	        {
	            return (ServiceVisitorsCollection)this[serviceVistorsAttributeName];
	        }
	    }
	    
	    [ConfigurationProperty(serviceAttributeName)]
	    public ServiceCollection Services
	    {
	    	get
	    	{
	    		return (ServiceCollection)this[serviceAttributeName];
	    	}
	    }
	}
}
