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
using System.Runtime.CompilerServices;

namespace Cio.ComponentModel
{
    public static class BackingFieldSetter
    {
        public static void SetBackingField<T, TProperty>(this T obj, ref TProperty variable, TProperty newValue, [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            else if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (EqualityComparer<TProperty>.Default.Equals(variable, newValue))
            {
                RaisePropertyChangingEvent(obj as IImplementNotifyPropertyChanging, propertyName);

                variable = newValue;

                RaisePropertyChangedEvent(obj as IImplementNotifyPropertyChanged, propertyName);
            }
        }

        private static void RaisePropertyChangingEvent(IImplementNotifyPropertyChanging objToRaiseOn, string propertyName)
        {
            if (objToRaiseOn != null)
            {
                objToRaiseOn.RaisePropertyChangingEvent(propertyName);
            }
        }

        private static void RaisePropertyChangedEvent(IImplementNotifyPropertyChanged objToRaiseOn, string propertyName)
        {
            if (objToRaiseOn != null)
            {
                objToRaiseOn.RaisePropertyChangedEvent(propertyName);
            }
        }
    }
}
