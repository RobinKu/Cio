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
using Cio.UI;

namespace TestApp
{
	public class User
	{
		public User()
		{
			Profile = new UserProfile();
		}
		
		[DisplayName("Naam")]
		public string Name
		{
			get;
			set;
		}
		
		[DisplayName("Wachtwoord")]
		public string Password
		{
			get;
			set;
		}
		
		public string Signature
		{
			get;
			set;
		}
		
		[DisplayName("Actief")]
		public bool IsActive
		{
			get;
			set;
		}
		
		public UserProfile Profile
		{
			get;
			private set;
		}
	}
	
	public class UserProfile
	{
		public string Text
		{
			get;
			set;
		}
	}
}
