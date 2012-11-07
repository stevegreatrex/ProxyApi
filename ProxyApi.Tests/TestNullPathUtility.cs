using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	[TestClass]
	public class TestNullPathUtility
	{
		/// <summary>
		/// Ensures that ToAbsolute returns an empty string
		/// </summary>
		[TestMethod]
		public void ToAbsolute_Returns_Empty_String()
		{
			Assert.AreEqual(string.Empty, new NullPathUtility().ToAbsolute(null));
		}

		/// <summary>
		/// Ensures that GetVirtualPath returns an empty string
		/// </summary>
		[TestMethod]
		public void GetVirtualPath_Returns_Empty_String()
		{
			Assert.AreEqual(string.Empty, new NullPathUtility().GetVirtualPath(null));
		}
	}
}
