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
using System.Linq;

namespace Cio.UI
{
    public abstract class BlockBuilder : IBlockBuilder
    {
        public event EventHandler<AddedEventArgs> Added;

        public abstract IResult Add(AddInformation info);

        public abstract object CreateBlock();

        protected void OnAdded(AddInformation info, IResult result)
        {
            EventHandler<AddedEventArgs> handler = Added;

            if (handler != null)
            {
                AddedEventArgs ev = new AddedEventArgs(info, result);

                handler(this, ev);
            }
        }

        protected T ValidateAsBaseClass<T>(object block)
            where T : class
        {
            T specificBlock = block as T;

            if (specificBlock == null)
            {
                throw new ArgumentException(string.Format("form must derive from {0}. Use the CreateBlock() method of the formbuilder to create the right type.", typeof(T).Name));
            }

            return specificBlock;
        }
    }
}
