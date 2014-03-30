/*
 * Cio
 * Copyright (C) 2014 Robin Kuijstermans
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

namespace Cio.ComponentModel
{
    public class DetailedPropertyChangedEventArgs : EventArgs
    {
        public DetailedPropertyChangedEventArgs(object changedObject, string propertyName, object oldValue, object newValue)
        {
            this.ChangedObject = changedObject;
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public object ChangedObject
        {
            get;
            private set;
        }

        public string PropertyName
        {
            get;
            private set;
        }

        public object OldValue
        {
            get;
            private set;
        }

        public object NewValue
        {
            get;
            private set;
        }
    }
}
