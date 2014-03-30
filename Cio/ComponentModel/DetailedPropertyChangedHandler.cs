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
using Cio.Collections.Generic;
using Cio.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cio.ComponentModel
{
    public static class DetailedPropertyChangedHandler
    {
        public static DetailedPropertyChangedHandler<T> Hookup<T>(T value)
            where T : INotifyDetailedPropertyChanged, INotifyPropertyChanging, INotifyPropertyChanged
        {
            return new DetailedPropertyChangedHandler<T>(value);
        }
    }

    public class DetailedPropertyChangedHandler<T> : IDisposable
        where T : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private readonly PropertyChangedEventHandler changedHandler;
        private readonly PropertyChangingEventHandler changingHandler;
        private readonly T value;
        private readonly IDictionary<string, object> oldValues = new Dictionary<string, object>();

        public event EventHandler<DetailedPropertyChangedEventArgs> ValuePropertyChanged;

        internal DetailedPropertyChangedHandler(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.value = value;

            this.changingHandler = new PropertyChangingEventHandler(PropertyChanging);
            this.changedHandler = new PropertyChangedEventHandler(PropertyChanged);

            this.value.PropertyChanging += this.changingHandler;
            this.value.PropertyChanged += this.changedHandler;
        }

        private void PropertyChanging(object sender, PropertyChangingEventArgs ev)
        {
            if (!string.IsNullOrWhiteSpace(ev.PropertyName))
            {
                object oldValue = PropertyUtil.GetPropertyValue(this.value, ev.PropertyName);

                this.oldValues.AddOrEdit(ev.PropertyName, oldValue);
            }
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs ev)
        {
            if (!string.IsNullOrWhiteSpace(ev.PropertyName))
            {
                object oldValue;

                if (this.oldValues.TryGetValue(ev.PropertyName, out oldValue))
                {
                    object newValue = PropertyUtil.GetPropertyValue(this.value, ev.PropertyName);

                    this.RaiseValuePropertyChanged(ev.PropertyName, oldValue, newValue);

                    INotifyDetailedPropertyChanged notifiable = this.value as INotifyDetailedPropertyChanged;
                    if (notifiable != null)
                    {
                        notifiable.OnPropertyChanged(ev.PropertyName, oldValue, newValue);
                    }
                }
            }
        }

        private void RaiseValuePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            EventHandler<DetailedPropertyChangedEventArgs> handler = this.ValuePropertyChanged;

            if (handler != null)
            {
                handler(this, new DetailedPropertyChangedEventArgs(this.value, propertyName, oldValue, newValue));
            }
        }

        public void Dispose()
        {
            this.value.PropertyChanging -= changingHandler;
            this.value.PropertyChanged -= changedHandler;
        }
    }
}
