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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cio
{
    public class ResetLazy<T>
    {
        private Func<Lazy<T>> newLazyFunction;
        private Lazy<T> currentLazy;

        public ResetLazy(Func<Lazy<T>> newLazyFunction)
        {
            if (newLazyFunction == null)
            {
                throw new ArgumentNullException("newLazyFunction");
            }

            this.newLazyFunction = newLazyFunction;

            this.Reset();
        }

        public ResetLazy()
            : this(() => new Lazy<T>())
        {
        }

        public ResetLazy(bool isThreadSafe)
            : this(() => new Lazy<T>(isThreadSafe))
        {
        }

        public ResetLazy(LazyThreadSafetyMode mode)
            : this(() => new Lazy<T>(mode))
        {
        }

        public ResetLazy(Func<T> function)
            : this(() => new Lazy<T>(function))
        {
        }

        public ResetLazy(Func<T> function, bool isThreadSafe)
            : this(() => new Lazy<T>(function, isThreadSafe))
        {
        }

        public ResetLazy(Func<T> function, LazyThreadSafetyMode mode)
            : this(() => new Lazy<T>(function, mode))
        {
        }

        public bool IsValueCreated
        {
            get
            {
                return this.currentLazy.IsValueCreated;
            }
        }

        public T Value
        {
            get
            {
                return this.currentLazy.Value;
            }
        }

        public void Reset()
        {
            this.currentLazy = this.newLazyFunction();
        }
    }
}
