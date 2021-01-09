using System;
using NUnit.Framework;


namespace Xamarin.Forms.Core.UnitTests
{
	[TestFixture]
	public class PointTests : BaseTestFixture
	{
		[Test]
		public void TestPointEquality()
		{
			Assert.True(new PointF(0, 1) != new PointF(1, 0));
			Assert.True(new PointF(5, 5) == new PointF(5, 5));
		}

		[Test]
		public void TestPointDistance()
		{
			Assert.That(new PointF(2, 2).Distance(new PointF(5, 6)), Is.EqualTo(5).Within(0.001));
		}

		[Test]
		public void TestPointMath()
		{
			var point = new PointF(2, 3) + new Size(3, 2);
			Assert.AreEqual(new PointF(5, 5), point);

			point = new PointF(3, 4) - new Size(2, 3);
			Assert.AreEqual(new PointF(1, 1), point);
		}

		[Test]
		public void TestPointFromSize()
		{
			var point = new PointF(new Size(10, 20));

			Assert.AreEqual(10, point.X);
			Assert.AreEqual(20, point.Y);
		}

		[Test]
		public void TestPointOffset()
		{
			var point = new PointF(2, 2);

			point = point.Offset(10, 20);

			Assert.AreEqual(new PointF(12, 22), point);
		}

		[Test]
		public void TestPointRound()
		{
			var point = new PointF(2.4, 2.7);
			point = point.Round();

			Assert.AreEqual(Math.Round(2.4), point.X);
			Assert.AreEqual(Math.Round(2.7), point.Y);
		}

		[Test]
		public void TestPointEmpty()
		{
			var point = new PointF();

			Assert.True(point.IsEmpty);
		}

		[Test]
		public void TestPointHashCode([Range(3, 6)] double x1, [Range(3, 6)] double y1, [Range(3, 6)] double x2,
									   [Range(3, 6)] double y2)
		{
			bool result = new PointF(x1, y1).GetHashCode() == new PointF(x2, y2).GetHashCode();
			if (x1 == x2 && y1 == y2)
				Assert.True(result);
			else
				Assert.False(result);
		}

		[Test]
		public void TestSizeFromPoint()
		{
			var point = new PointF(2, 4);
			var size = (Size)point;

			Assert.AreEqual(new Size(2, 4), size);
		}

		[Test]
		public void TestPointEquals()
		{
			var point = new PointF(2, 4);

			Assert.True(point.Equals(new PointF(2, 4)));
			Assert.False(point.Equals(new PointF(3, 4)));
			Assert.False(point.Equals("Point"));
		}

		[Test]
		[TestCase(0, 0, ExpectedResult = "{X=0 Y=0}")]
		[TestCase(5, 2, ExpectedResult = "{X=5 Y=2}")]
		public string TestPointToString(double x, double y)
		{
			return new PointF(x, y).ToString();
		}

		[Test]
		public void TestPointTypeConverter()
		{
			var converter = new PointTypeConverter();
			Assert.True(converter.CanConvertFrom(typeof(string)));
			Assert.AreEqual(new PointF(1, 2), converter.ConvertFromInvariantString("1,2"));
			Assert.AreEqual(new PointF(1, 2), converter.ConvertFromInvariantString("1, 2"));
			Assert.AreEqual(new PointF(1, 2), converter.ConvertFromInvariantString(" 1 , 2 "));
			Assert.AreEqual(new PointF(1.1, 2), converter.ConvertFromInvariantString("1.1,2"));
			Assert.Throws<InvalidOperationException>(() => converter.ConvertFromInvariantString(""));
		}

	}
}
