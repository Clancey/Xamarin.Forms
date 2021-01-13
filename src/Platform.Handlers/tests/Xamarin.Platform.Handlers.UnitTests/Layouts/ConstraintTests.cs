using NUnit.Framework;
using Xamarin.Platform.Handlers.Tests;
using Xamarin.Platform.Layouts;

namespace Xamarin.Platform.Handlers.UnitTests.Layouts
{
	[TestFixture(Category = TestCategory.Layout)]
	public class ConstraintTests 
	{
		[Test(Description = "When resolving constraints, external constraints take precedence")]
		[TestCase(100, 200, 100)]
		[TestCase(100, 100, 100)]
		[TestCase(100, -1, 100)]
		public void ExternalWinsOverDesired(float externalConstraint, float desiredLength, float expected) 
		{
			var resolution = LayoutManager.ResolveConstraints(externalConstraint, desiredLength);
			Assert.That(resolution, Is.EqualTo(expected));
		}

		[Test(Description = "When resolving constraints, external constraints take precedence")]
		[TestCase(100, 200, 130, 100)]
		[TestCase(100, -1, 130, 100)]
		public void ExternalWinsOverDesired(float externalConstraint, float desiredLength, float measured, float expected)
		{
			var resolution = LayoutManager.ResolveConstraints(externalConstraint, desiredLength, measured);
			Assert.That(resolution, Is.EqualTo(expected));
		}

		[Test(Description = "If external and request constraints don't apply, constrain to measured value")]
		public void MeasuredWinsIfNothingElseApplies()
		{
			var resolution = LayoutManager.ResolveConstraints(float.PositiveInfinity, -1, 245);
			Assert.That(resolution, Is.EqualTo(245));
		}

		[Test(Description = "If external constraints don't apply, constraing to requested value")]
		public void RequestedTakesPrecedenceOverMeasured()
		{
			var resolution = LayoutManager.ResolveConstraints(float.PositiveInfinity, 90, 245);
			Assert.That(resolution, Is.EqualTo(90));
		}
	}
}
