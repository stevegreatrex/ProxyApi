using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ProxyApi.Tests
{
	/// <summary>
	/// Tests the functionality of the <see cref="ValidateHttpAntiForgeryTokenAttribute"/> class.
	/// </summary>	
	[TestClass]
	public class TestValidateHttpAntiForgeryTokenAttribute : FixtureBase<ValidateHttpAntiForgeryTokenAttribute>
	{
		private Mock<IContextProvider> _contextProvider;

		#region Setup

		/// <summary>
		/// Creates a new instance of <see cref="ValidateHttpAntiForgeryTokenAttribute"/> for each
		/// unit test.
		/// </summary>
		public override ValidateHttpAntiForgeryTokenAttribute CreateTestSubject()
		{
			_contextProvider = this.MockRepository.Create<IContextProvider>();

			return new ValidateHttpAntiForgeryTokenAttribute(_contextProvider.Object);
		}

		#endregion

		#region Tests

		/// <summary>
		/// Ensures that appropriate <see cref="ArgumentNullException"/> are thrown when
		/// null parameters are passed to the constructor.
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Throws_On_Null_ContextProvider()
		{
			new ValidateHttpAntiForgeryTokenAttribute(null);
		}

		/// <summary>
		/// Ensures that OnAuthorization throws an exception when passed a null context
		/// </summary>
		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void OnAuthorization_Throws_On_Null_Context()
		{
			this.TestSubject.OnAuthorization(null);
		}

		/// <summary>
		/// Ensures that ShouldValidateRequest returns true when no exclusions are set
		/// </summary>
		[TestMethod]
		public void ShouldValidateRequest_Returns_True_When_No_Exclusions_Are_Set()
		{
			Assert.IsTrue(this.TestSubject.ShouldValidateRequest(null));
		}

		/// <summary>
		/// Ensures that ShouldValidateRequest returns true if the current identity is not authenticated
		/// </summary>
		[TestMethod]
		public void ShouldValidateRequest_Returns_True_When_Not_Authenticated()
		{
			var identity = new Mock<IIdentity>();
			identity.Setup(i => i.IsAuthenticated).Returns(false);
			_contextProvider.Setup(cp => cp.GetCurrentIdentity()).Returns(identity.Object);

			this.TestSubject.ExcludeAuthenticationTypes = "Basic";

			Assert.IsTrue(this.TestSubject.ShouldValidateRequest(null));
		}

		/// <summary>
		/// Ensures that ShouldValidateRequest returns false when the authentication type is excluded
		/// </summary>
		[TestMethod]
		public void ShouldValidateRequest_Returns_False_For_Excluded_AuthenticationType()
		{
			var identity = new Mock<IIdentity>();
			identity.Setup(i => i.IsAuthenticated).Returns(true);
			_contextProvider.Setup(cp => cp.GetCurrentIdentity()).Returns(identity.Object);

			this.TestSubject.ExcludeAuthenticationTypes = "Basic, Basic-With-Space,Something-Without-Space";
			
			identity.Setup(i => i.AuthenticationType).Returns("Basic");
			Assert.IsFalse(this.TestSubject.ShouldValidateRequest(null));

			identity.Setup(i => i.AuthenticationType).Returns("Basic-With-Space");
			Assert.IsFalse(this.TestSubject.ShouldValidateRequest(null));

			identity.Setup(i => i.AuthenticationType).Returns("Something-Without-Space");
			Assert.IsFalse(this.TestSubject.ShouldValidateRequest(null));

			//now some non-matching examples...
			identity.Setup(i => i.AuthenticationType).Returns("non-matching");
			Assert.IsTrue(this.TestSubject.ShouldValidateRequest(null));

			identity.Setup(i => i.AuthenticationType).Returns("Basic, Basic-With-Space");
			Assert.IsTrue(this.TestSubject.ShouldValidateRequest(null));

			identity.Setup(i => i.AuthenticationType).Returns("");
			Assert.IsTrue(this.TestSubject.ShouldValidateRequest(null));
		}

		#endregion
	}
}