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
    public static partial class PropertyValidationConfigurationExtensions
    {
        public static PropertyValidationConfiguration<TObject, TProperty> IsRequired<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config)
            where TProperty : class
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new RequiredValidation());

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> Between<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty lowValue, TProperty highValue)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new BetweenValidation<TProperty>(lowValue, highValue));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> Between<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty lowValue, TProperty highValue, bool including)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new BetweenValidation<TProperty>(lowValue, highValue, including));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> Between<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty lowValue, TProperty highValue, bool lowIncluding, bool highIncluding)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new BetweenValidation<TProperty>(lowValue, highValue, lowIncluding, highIncluding));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> GreaterThan<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty otherValue)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new GreaterThanValidation<TProperty>(otherValue));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> GreaterThanOrEqual<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty otherValue)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new GreaterThanOrEqualValidation<TProperty>(otherValue));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> LessThan<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty otherValue)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new LessThanValidation<TProperty>(otherValue));

            return config;
        }

        public static PropertyValidationConfiguration<TObject, TProperty> LessThanOrEqual<TObject, TProperty>(this PropertyValidationConfiguration<TObject, TProperty> config, TProperty otherValue)
            where TProperty : IComparable<TProperty>
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            config.Validations.Add(new LessThanOrEqualValidation<TProperty>(otherValue));

            return config;
        }
    }
}
