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
using Cio;
using NUnit.Framework;

namespace Cio.Test
{
	[TestFixture]
	public class TypeExtensionsTests
	{
		private class SomeClass
		{
		}
		
		private class SomeOtherClass : SomeClass
		{
		}
		
		[TestCase]
		public void IsEqualOrDerivedFrom_SomeOtherClassAndSomeClass_True()
		{
			// Arrange
			Type someType = typeof(SomeClass);
			Type someOtherType = typeof(SomeOtherClass);
			
			// Act
			bool result = someOtherType.IsEqualOrDerivedFrom(someType);
			
			// Assert
			Assert.IsTrue(result);
		}
		
		[TestCase]
		public void IsEqualOrDerivedFrom_SomeClassAndSomeOtherClass_False()
		{
			// Arrange
			Type someType = typeof(SomeClass);
			Type someOtherType = typeof(SomeOtherClass);
			
			// Act
			bool result = someType.IsEqualOrDerivedFrom(someOtherType);
			
			// Assert
			Assert.IsFalse(result);
		}
		
		[TestCase]
		public void IsEqualOrDerivedFrom_SomeClassAndSomeClass_True()
		{
			// Arrange
			Type someType = typeof(SomeClass);
			
			// Act
			bool result = someType.IsEqualOrDerivedFrom(someType);
			
			// Assert
			Assert.IsTrue(result);
		}
	}
}
