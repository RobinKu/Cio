using Cio.Reflection;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cio.Validation
{
    public abstract class Validator<T>
    {
        private IDictionary<string, PropertyValidationConfiguration<T>> configurations = new Dictionary<string, PropertyValidationConfiguration<T>>();

        public PropertyValidationConfiguration<T, TProperty> Validate<TProperty>(Expression<Func<T, TProperty>> propertyToValidate)
        {
            string propertyName = PropertyUtil.GetPropertyName(propertyToValidate);
            PropertyValidationConfiguration<T> config;

            if(!this.configurations.TryGetValue(propertyName, out config))
            {
                config = new PropertyValidationConfiguration<T, TProperty>(propertyName);
            }

            return (PropertyValidationConfiguration<T, TProperty>)config;
        }

        public IList<ValidationResult> Validate(T value)
        {
            return Validate(value, this.configurations.Values);
        }

        public IList<ValidationResult> Validate(T value, string propertyName)
        {
            IList<ValidationResult> results;
            PropertyValidationConfiguration<T> config;

            if(this.configurations.TryGetValue(propertyName, out config))
            {
                results = Validate(value, new PropertyValidationConfiguration<T>[] { config });
            }
            else
            {
                results = new List<ValidationResult>();
            }

            return results;
        }

        private IList<ValidationResult> Validate(T value, IEnumerable<PropertyValidationConfiguration<T>> configurations)
        {
            IList<ValidationResult> results = new List<ValidationResult>();

            foreach (PropertyValidationConfiguration<T> config in configurations)
            {
                foreach(IValidation validation in config.Validations)
                {
                    ValidationContext context = new ValidationContext();
                    context.ValidatingObject = value;
                    context.PropertyName = config.PropertyName;

                    results.Add(validation.Validate(context));
                }
            }

            return results;
        }
    }
}
