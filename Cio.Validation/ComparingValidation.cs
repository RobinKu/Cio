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
using System.Text;
using System.Threading.Tasks;

namespace Cio.Validation
{
    public abstract class ComparingValidation<T> : IValidation
        where T : IComparable<T>
    {
        private T otherValue;

        public ComparingValidation(T otherValue)
        {
            this.otherValue = otherValue;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ValidationResult result = new ValidationResult();

            result.ValidationType = GetType();

            T comparable = (T)context.ValidatingValue;

            int compareResult = comparable.CompareTo(this.otherValue);

            result.IsValid = this.CheckCompareResult(compareResult);

            return result;
        }

        protected abstract bool CheckCompareResult(int compareToResult);
    }
}
