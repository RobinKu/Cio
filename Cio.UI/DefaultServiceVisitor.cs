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
	public abstract class DefaultServiceVisitor : IServiceVisitor
	{
		private Type serviceType;
		private Type informationType;
		private Type resultType;
		
		public DefaultServiceVisitor()
		{
		}
		
		protected void SetServiceType<TService>()
		{
			this.serviceType = typeof(TService);
		}
		
		protected void SetInformationType<TInformation>()
		{
			this.informationType = typeof(TInformation);
		}
		
		protected void SetResultType<TResult>()
			where TResult : IResult
		{
			this.resultType = typeof(TResult);
		}
		
		public void Visit(AddInformation info, IResult result, IEnumerable<object> services)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			else if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			else if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			
			if ((this.resultType == null || result.GetType().IsEqualOrDerivedFrom(this.resultType)) &&
			    (this.informationType == null || info.GetType().IsEqualOrDerivedFrom(this.informationType)))
			{
				if (this.serviceType != null)
				{
					services = services.Where(s => s.GetType().IsEqualOrDerivedFrom(this.serviceType));
				}
				
				this.VisitInternal(info, result, services);
			}
		}
		
		protected abstract void VisitInternal(AddInformation info, IResult result, IEnumerable<object> services);
	}
}
