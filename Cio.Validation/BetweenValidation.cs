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
    public class BetweenValidation<T> : IValidation
        where T : IComparable<T>
    {
        private T lowValue;
        private T highValue;
        private bool lowIncluding;
        private bool highIncluding;

        public BetweenValidation(T lowValue, T highValue)
            : this(lowValue, highValue, true)
        {
        }

        public BetweenValidation(T lowValue, T highValue, bool including)
            : this(lowValue, highValue, including, including)
        {
        }

        public BetweenValidation(T lowValue, T highValue, bool lowIncluding, bool highIncluding)
        {
            if (lowValue.CompareTo(highValue) >= 0)
            {
                throw new ArgumentException("lowValue must be lower than highValue");
            }

            this.lowValue = lowValue;
            this.highValue = highValue;
            this.lowIncluding = lowIncluding;
            this.highIncluding = highIncluding;
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

            bool isLowValid = false;
            bool isHighValid = false;

            int lowCompareResult = comparable.CompareTo(lowValue);
            int highCompareResult = comparable.CompareTo(highValue);

            if (lowCompareResult > 0 || (this.lowIncluding && lowCompareResult == 0))
            {
                isLowValid = true;
            }

            if (highCompareResult < 0 || (this.highIncluding && highCompareResult == 0))
            {
                isHighValid = true;
            }

            result.IsValid = isLowValid && isHighValid;

            return result;
        }
    }
}
