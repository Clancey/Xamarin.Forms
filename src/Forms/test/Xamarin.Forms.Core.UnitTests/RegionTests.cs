using NUnit.Framework;


namespace Xamarin.Forms.Core.UnitTests
{
	[TestFixture]
	public class RegionTests : BaseTestFixture
	{
		[Test]
		public void RegionOneLineConstruction()
		{
			double[] lineHeights = { 20 };
			double maxWidth = 200;
			double startX = 90;
			double endX = 180;
			double startY = 80;

			var region = Region.FromLines(lineHeights, maxWidth, startX, endX, startY);

			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(90, 80)));
			Assert.IsTrue(region.Contains(new PointF(90, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(179, 80)));
			Assert.IsTrue(region.Contains(new PointF(179, 99)));

			//** Outside Container **//
			// Top Left start character
			Assert.IsFalse(region.Contains(new PointF(89, 80)));
			Assert.IsFalse(region.Contains(new PointF(89, 99)));

			// Top Right end character
			Assert.IsFalse(region.Contains(new PointF(180, 80)));
			Assert.IsFalse(region.Contains(new PointF(180, 99)));

		}

		[Test]
		public void RegionTwoLineConstruction()
		{
			double[] lineHeights = { 20, 20 };
			double maxWidth = 200;
			double startX = 90;
			double endX = 40;
			double startY = 80;

			var region = Region.FromLines(lineHeights, maxWidth, startX, endX, startY);

			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(90, 80)));
			Assert.IsTrue(region.Contains(new PointF(90, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(199, 80)));
			Assert.IsTrue(region.Contains(new PointF(199, 99)));


			// End Left end character
			Assert.IsTrue(region.Contains(new PointF(0, 100)));
			Assert.IsTrue(region.Contains(new PointF(0, 119)));

			// End Right end character
			Assert.IsTrue(region.Contains(new PointF(39, 100)));
			Assert.IsTrue(region.Contains(new PointF(39, 119)));

			//** Outside Container **//
			// Top Left start character
			Assert.IsFalse(region.Contains(new PointF(89, 80)));
			Assert.IsFalse(region.Contains(new PointF(89, 99)));

			// Top Right end character
			Assert.IsFalse(region.Contains(new PointF(200, 80)));
			Assert.IsFalse(region.Contains(new PointF(200, 99)));

			// End Left end character
			Assert.IsFalse(region.Contains(new PointF(-1, 100)));
			Assert.IsFalse(region.Contains(new PointF(-1, 119)));

			// End Right end character
			Assert.IsFalse(region.Contains(new PointF(40, 100)));
			Assert.IsFalse(region.Contains(new PointF(40, 119)));
		}

		[Test]
		public void RegionThreeLineConstruction()
		{
			double[] lineHeights = { 20, 20, 20 };
			double maxWidth = 200;
			double startX = 90;
			double endX = 40;
			double startY = 80;

			var region = Region.FromLines(lineHeights, maxWidth, startX, endX, startY);

			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(90, 80)));
			Assert.IsTrue(region.Contains(new PointF(90, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(199, 80)));
			Assert.IsTrue(region.Contains(new PointF(199, 99)));

			// Middle Left end character
			Assert.IsTrue(region.Contains(new PointF(0, 100)));
			Assert.IsTrue(region.Contains(new PointF(0, 119)));

			// Middle Right end character
			Assert.IsTrue(region.Contains(new PointF(199, 100)));
			Assert.IsTrue(region.Contains(new PointF(199, 119)));

			// End Left end character
			Assert.IsTrue(region.Contains(new PointF(0, 120)));
			Assert.IsTrue(region.Contains(new PointF(0, 139)));

			// End Right end character
			Assert.IsTrue(region.Contains(new PointF(39, 120)));
			Assert.IsTrue(region.Contains(new PointF(39, 139)));

			//** Outside Container **//
			// Top Left start character
			Assert.IsFalse(region.Contains(new PointF(89, 80)));
			Assert.IsFalse(region.Contains(new PointF(89, 99)));

			// Top Right end character
			Assert.IsFalse(region.Contains(new PointF(200, 80)));
			Assert.IsFalse(region.Contains(new PointF(200, 99)));

			// End Left end character
			Assert.IsFalse(region.Contains(new PointF(-1, 120)));
			Assert.IsFalse(region.Contains(new PointF(-1, 139)));

			// End Right end character
			Assert.IsFalse(region.Contains(new PointF(40, 120)));
			Assert.IsFalse(region.Contains(new PointF(40, 139)));
		}

		[Test]
		public void RegionInflate()
		{
			double[] lineHeights = { 20 };
			double maxWidth = 200;
			double startX = 90;
			double endX = 180;
			double startY = 80;

			var region = Region.FromLines(lineHeights, maxWidth, startX, endX, startY).Inflate(10);

			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(90, 80)));
			Assert.IsTrue(region.Contains(new PointF(90, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(179, 80)));
			Assert.IsTrue(region.Contains(new PointF(179, 99)));

			//** Inflated Container **//
			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(89, 80)));
			Assert.IsTrue(region.Contains(new PointF(89, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(180, 80)));
			Assert.IsTrue(region.Contains(new PointF(180, 99)));

			//** Outside Container **//
			// Top Left start character
			Assert.IsFalse(region.Contains(new PointF(79, 80)));
			Assert.IsFalse(region.Contains(new PointF(79, 99)));

			// Top Right end character
			Assert.IsFalse(region.Contains(new PointF(190, 80)));
			Assert.IsFalse(region.Contains(new PointF(190, 99)));
		}

		[Test]
		public void RegionDeflate()
		{
			double[] lineHeights = { 20 };
			double maxWidth = 200;
			double startX = 90;
			double endX = 180;
			double startY = 80;

			var region = Region.FromLines(lineHeights, maxWidth, startX, endX, startY).Inflate(10).Deflate();

			// Top Left start character
			Assert.IsTrue(region.Contains(new PointF(90, 80)));
			Assert.IsTrue(region.Contains(new PointF(90, 99)));

			// Top Right end character
			Assert.IsTrue(region.Contains(new PointF(179, 80)));
			Assert.IsTrue(region.Contains(new PointF(179, 99)));

			//** Outside Container **//
			// Top Left start character
			Assert.IsFalse(region.Contains(new PointF(89, 80)));
			Assert.IsFalse(region.Contains(new PointF(89, 99)));

			// Top Right end character
			Assert.IsFalse(region.Contains(new PointF(180, 80)));
			Assert.IsFalse(region.Contains(new PointF(180, 99)));

		}
	}
}
