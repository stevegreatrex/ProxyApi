using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProxyApi.Tests
{
	public abstract class FixtureBase<TTestSubject>
	{
		protected TTestSubject TestSubject { get; private set; }

		[TestInitialize]
		public void Setup()
		{
			this.TestSubject = this.CreateTestSubject();
		}

		public abstract TTestSubject CreateTestSubject();
	}
}
