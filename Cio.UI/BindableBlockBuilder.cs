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
using System.Text;
using System.Threading.Tasks;

namespace Cio.UI
{
    public abstract class BindableBlockBuilder : BlockBuilder, IBindableBlockBuilder
    {
        public override IResult Add(AddInformation info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            BindingInformation bInfo = info as BindingInformation;

            if (bInfo != null)
            {
                return Add(bInfo);
            }
            else
            {
                throw new MissingInformationHandlerException(string.Format("This builder cannot handle info of type {0}.", info.GetType()));
            }
        }

        public abstract IResult Add(BindingInformation info);

        public abstract void Bind(object block, object bindableOBject);
    }
}
