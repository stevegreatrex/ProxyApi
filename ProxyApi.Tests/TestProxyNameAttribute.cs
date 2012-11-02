using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ProxyNameAttribute"/> class.
	/// </summary>
	[TestClass]
	public class TestProxyNameAttribute
	{
		/// <summary>
		/// Ensures that Constructor sets name property
		/// </summary>
		[TestMethod]
		public void Constructor_Sets_Name_Property()
		{
			var testSubject = new ProxyNameAttribute("name123");
			Assert.AreEqual("name123", testSubject.Name);
		}

		/// <summary>
		/// Ensures that Constructor throws exceptions on invalid names
		/// </summary>
		[TestMethod]
		public void Constructor_Throws_On_Invalid_Names()
		{
			CheckConstructorThrowsFor(null);
			CheckConstructorThrowsFor(string.Empty);
			CheckConstructorThrowsFor(" ");
			CheckConstructorThrowsFor("\t");
			CheckConstructorThrowsFor("  invalid  ");
			CheckConstructorThrowsFor("\r\n");
			CheckConstructorThrowsFor("  invalid");
			CheckConstructorThrowsFor("invalid  ");
			CheckConstructorThrowsFor("1abc");
			CheckSpecialCharacter('.');
			CheckSpecialCharacter('-');
			CheckSpecialCharacter('=');
			CheckSpecialCharacter('+');
			CheckSpecialCharacter('(');
			CheckSpecialCharacter(')');
			CheckSpecialCharacter('{');
			CheckSpecialCharacter('}');
			CheckSpecialCharacter('[');
			CheckSpecialCharacter(']');
			CheckSpecialCharacter(',');

		}

		private void CheckSpecialCharacter(char character)
		{
			CheckConstructorThrowsFor(character.ToString());
			CheckConstructorThrowsFor(string.Format("{0}abc", character));
			CheckConstructorThrowsFor(string.Format("a{0}bc", character));
			CheckConstructorThrowsFor(string.Format("abc{0}", character));
		}

		private void CheckConstructorThrowsFor(string name)
		{
			try
			{
				new ProxyNameAttribute(name);
				Assert.Fail("Constructor should throw exception for '{0}'", name);
			}
			catch (ArgumentException)
			{
			}
		}
	}
}
