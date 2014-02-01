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
using System.Reflection;

namespace Cio.UI.Services
{
	public abstract class NestingDisplayNameService : INestingDisplayNameService
	{
		private IDisplayNameService innerDisplayNameService;
		
		protected NestingDisplayNameService(IDisplayNameService innerDisplayNameService)
		{
			this.innerDisplayNameService = innerDisplayNameService;
		}
		
		public IDisplayNameService GetInnerDisplayNameService()
		{
			return this.innerDisplayNameService;
		}
		
		public string GetDisplayName(Type sourceType, string bindingPath)
		{
			string displayName;
			
			if (!this.TryGetDisplayName(sourceType, bindingPath, out displayName))
			{
				displayName = this.innerDisplayNameService.GetDisplayName(sourceType, bindingPath);
			}
			
			return displayName;
		}
		
		protected abstract bool TryGetDisplayName(Type sourceType, string bindingPath, out string displayName);
	}
}
