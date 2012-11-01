using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ProxyApi.Tests
{
	public abstract class FixtureBase<TTestSubject>
	{
		protected TTestSubject TestSubject { get; private set; }

		[TestInitialize]
		public void Setup()
		{
			this.MockRepository = new MockRepository(MockBehavior.Strict);
			this.TestSubject = this.CreateTestSubject();
		}

		[TestCleanup]
		public void Cleanup()
		{
			this.MockRepository.VerifyAll();
		}

		public abstract TTestSubject CreateTestSubject();

		public MockRepository MockRepository { get; private set; }
	}
}
