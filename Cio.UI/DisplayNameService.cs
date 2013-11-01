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
	public static class DisplayNameService
	{
		private static IDisplayNameService defaultDisplayNameService;
		
		static DisplayNameService()
		{
			defaultDisplayNameService = new PropertyDisplayNameService();
			
			ValidatingDefault += new ValidatingDisplayNameServiceEventHandler(CheckForRecursion);
		}
		
		public static event ValidatingDisplayNameServiceEventHandler ValidatingDefault;
		
		public static IDisplayNameService Default
		{
			get
			{
				return defaultDisplayNameService;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "The default DisplayNameService cannot be null.");
				}
				
				OnValidatingDefault(value);
				
				defaultDisplayNameService = value;
			}
		}
		
		private static void OnValidatingDefault(IDisplayNameService displayNameService)
		{
			ValidatingDisplayNameServiceEventHandler handler = ValidatingDefault;
			ValidatingDisplayNameServiceEventArgs eventArgs = new ValidatingDisplayNameServiceEventArgs(displayNameService);
			
			if (handler != null)
			{
				handler(null, eventArgs);
			}
			
			if (!eventArgs.IsValid)
			{
				throw new DisplayNameServiceValidationException("Could not set the default DisplayNameService because it could not be validated. The following errors were given:", eventArgs.ErrorMessages);
			}
		}
		
		private static void CheckForRecursion(object source, ValidatingDisplayNameServiceEventArgs e)
		{
			INestingDisplayNameService nestingService = e.DisplayNameService as INestingDisplayNameService;
			
			if (nestingService != null)
			{
				INestingDisplayNameService nestedService = null;
				
				do
				{
					nestedService = nestingService.GetInnerDisplayNameService() as INestingDisplayNameService;
					
					if (nestingService == nestedService)
					{
						e.IsValid = false;
						e.AddErrorMessage("One of the inner display name services is refering to the default display name service which is recursive and will cause a StackOverfowException.");
						
						return;
					}
				} while (nestedService != null);
			}
		}
	}
}
