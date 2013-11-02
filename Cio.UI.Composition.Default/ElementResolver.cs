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
using System.Reflection;
using Cio.UI;

namespace Cio.UI.Composition.Default
{
	public class ElementResolver : IElementResolver
	{
		private ElementConfiguration configuration;
		
		public ElementResolver(ElementConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			
			this.configuration = configuration;
		}
		
		public IElementFactory<T> Resolve<T>()
		{
			return Resolve<T>(null);
		}
		
		public IElementFactory<T> Resolve<T>(string rendermode)
		{
			Type factoryType;
			
			if (!this.configuration.ElementFactoryTypes.TryGetValue(Tuple.Create(typeof(T), rendermode), out factoryType))
			{
				factoryType = this.configuration.DefaultElementFactoryType.MakeGenericType(typeof(T));
			}
			
			try
			{
				object factory = Activator.CreateInstance(factoryType);
				
				return (IElementFactory<T>)factory;
			}
			catch (MissingMethodException ex)
			{
				throw new ResolveException(string.Format("The type {0} does not contain a public parameterless constructor.", factoryType), ex);
			}
			catch (TargetInvocationException ex)
			{
				throw new ResolveException(string.Format("The constructor of type {0} threw an exception.", factoryType), ex);
			}
			catch(InvalidCastException ex)
			{
				throw new ResolveException(string.Format("The configured element factory (of type {0}) does not implement {1}.", factoryType, typeof(IElementFactory<T>)), ex);
			}
			catch (Exception ex)
			{
				throw new ResolveException(string.Format("Something went wrong while creating an instance of type {0}. Review the inner exception to determine what went wrong.", factoryType), ex);
			}
		}
	}
}
