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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cio.UI.Services
{
	public abstract class BaseEditableService : IEditableService
	{
		private bool editable;
		private string disabledReason;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		public event EventHandler EditableChanged;
		
		public bool Editable
		{
			get
			{
				return this.editable;
			}
			protected set
			{
				if (this.editable != value)
				{
					this.editable = value;
					
					OnEditableChanged();
					OnPropertyChanged();
				}
			}
		}
		
		public string DisabledReason
		{
			get
			{
				return this.disabledReason;
			}
			protected set
			{
				if (this.disabledReason != value)
				{
					this.disabledReason = value;
					
					OnPropertyChanged();
				}
			}
		}
		
		protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void OnEditableChanged()
		{
			EventHandler handler = this.EditableChanged;
			
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
