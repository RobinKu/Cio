﻿/*
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

namespace Cio
{
    public class Factory
    {
        public static IFactory GetDefault(Type type)
        {
            if(type==null)
            {
                throw new ArgumentNullException("type");
            }

            Type factoryType = typeof(Factory<>).MakeGenericType(type);

            return (IFactory)Activator.CreateInstance(factoryType);
        }
    }

    public class Factory<T> : BaseFactory<T>
        where T : new()
    {
        private static IFactory<T> defaultFactory;

        public static IFactory<T> Default
        {
            get
            {
                if (defaultFactory == null)
                {
                    defaultFactory = new Factory<T>();
                }

                return defaultFactory;
            }
        }

        public override T Create()
        {
            return new T();
        }
    }
}
