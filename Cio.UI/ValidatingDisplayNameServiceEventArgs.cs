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

namespace Cio.UI
{
	public class ValidatingDisplayNameServiceEventArgs : EventArgs
	{
		private ICollection<string> errorMessages = new List<string>();
		
		public ValidatingDisplayNameServiceEventArgs(IDisplayNameService displayNameService)
		{
			this.DisplayNameService = displayNameService;
			this.IsValid = true;
		}
		
		public IDisplayNameService DisplayNameService
		{
			get;
			private set;
		}
		
		public bool IsValid
		{
			get;
			set;
		}
		
		public void AddErrorMessage(string message)
		{
			this.errorMessages.Add(message);
		}
		
		public IEnumerable<string> ErrorMessages
		{
			get
			{
				return this.errorMessages;
			}
		}
	}
}
