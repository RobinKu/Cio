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
using Cio.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cio.Cloning
{
    public class ObjectCloner
    {
        private readonly ObjectCloner<object> cloner;

        public ObjectCloner(Type typeToClone)
            : this(Factory.GetDefault(typeToClone))
        {
        }

        public ObjectCloner(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            IFactory<object> genericFactory = factory as IFactory<object>;

            if (genericFactory == null)
            {
                genericFactory = new FactoryWrapper(factory);
            }

            this.cloner = new ObjectCloner<object>(genericFactory);
        }

        public object Clone(object obj)
        {
            return this.cloner.Clone(obj);
        }
    }

    public class ObjectCloner<T>
        where T : class, new()
    {
        private readonly IFactory<T> factory;

        public ObjectCloner()
            : this(Factory<T>.Default)
        {
        }

        public ObjectCloner(IFactory<T> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            this.factory = factory;
        }

        public T Clone(T obj)
        {
            T copy = null;

            if (obj != null)
            {
                copy = this.factory.Create();

                Type type = copy.GetType();

                IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public)
                    .Where(p => p.GetCustomAttributes<IgnoreCloningAttribute>(false).None());

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(obj);
                    object newValue;

                    if (property.GetCustomAttributes<CloneValueAttribute>(false).Any())
                    {
                        ObjectCloner propertyCloner = new ObjectCloner(value.GetType());

                        newValue = propertyCloner.Clone(value);
                    }
                    else
                    {
                        newValue = value;
                    }

                    property.SetValue(obj, newValue);
                }
            }

            return copy;
        }
    }
}
