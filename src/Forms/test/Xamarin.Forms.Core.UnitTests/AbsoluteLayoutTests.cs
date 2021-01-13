using System;
using System.Graphics;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Xamarin.Forms.Core.UnitTests
{
	[TestFixture]
	public class AbsoluteLayoutTests : BaseTestFixture
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
			var mockDeviceInfo = new TestDeviceInfo();
			Device.Info = mockDeviceInfo;
		}


		[Test]
		public void Constructor()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			Assert.That(abs.Children, Is.Empty);

			var sizeReq = abs.GetSizeRequest(float.PositiveInfinity, float.PositiveInfinity);
			Assert.AreEqual(SizeF.Zero, sizeReq.Request);
			Assert.AreEqual(SizeF.Zero, sizeReq.Minimum);
		}

		[Test]
		public void AbsolutePositionAndSizeUsingRectangle()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View { IsPlatformEnabled = true };

			abs.Children.Add(child, new RectangleF(10, 20, 30, 40));

			abs.Layout(new RectangleF(0, 0, 100, 100));

			Assert.AreEqual(new RectangleF(10, 20, 30, 40), child.Bounds);
		}

		[Test]
		public void AbsolutePositionAndSizeUsingRect()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View { IsPlatformEnabled = true };

			abs.Children.Add(child, new RectangleF(10, 20, 30, 40));

			abs.Layout(new RectangleF(0, 0, 100, 100));

			Assert.AreEqual(new RectangleF(10, 20, 30, 40), child.Bounds);
		}

		[Test]
		public void AbsolutePositionRelativeSize()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View { IsPlatformEnabled = true };


			abs.Children.Add(child, new RectangleF(10, 20, 0.4f, 0.5f), AbsoluteLayoutFlags.SizeProportional);

			abs.Layout(new RectangleF(0, 0, 100, 100));

			Assert.That(child.X, Is.EqualTo(10));
			Assert.That(child.Y, Is.EqualTo(20));
			Assert.That(child.Width, Is.EqualTo(40).Within(0.0001));
			Assert.That(child.Height, Is.EqualTo(50).Within(0.0001));
		}

		[TestCase(30, 40, 0.2f, 0.3f)]
		[TestCase(35, 45, 0.5f, 0.5f)]
		[TestCase(35, 45, 0, 0)]
		[TestCase(35, 45, 1, 1)]
		public void RelativePositionAbsoluteSize(float width, float height, float relX, float relY)
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View { IsPlatformEnabled = true };

			abs.Children.Add(child, new RectangleF(relX, relY, width, height), AbsoluteLayoutFlags.PositionProportional);

			abs.Layout(new RectangleF(0, 0, 100, 100));

			float expectedX = (float)Math.Round((100 - width) * relX);
			float expectedY = (float)Math.Round((100 - height) * relY);
			Assert.That(child.X, Is.EqualTo(expectedX).Within(0.0001));
			Assert.That(child.Y, Is.EqualTo(expectedY).Within(0.0001));
			Assert.That(child.Width, Is.EqualTo(width));
			Assert.That(child.Height, Is.EqualTo(height));
		}

		[Test]
		public void RelativePositionRelativeSize([Values(0, 0.2f, 0.5f, 1)] float relX, [Values(0, 0.2f, 0.5f, 1)] float relY, [Values(0, 0.2f, 0.5f, 1)] float relHeight, [Values(0, 0.2f, 0.5f, 1)] float relWidth)
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View
			{
				IsPlatformEnabled = true
			};
			abs.Children.Add(child, new RectangleF(relX, relY, relWidth, relHeight), AbsoluteLayoutFlags.All);
			abs.Layout(new RectangleF(0, 0, 100, 100));

			float expectedWidth = (float)Math.Round(100 * relWidth);
			float expectedHeight = (float)Math.Round(100 * relHeight);
			float expectedX = (float)Math.Round((100 - expectedWidth) * relX);
			float expectedY = (float)Math.Round((100 - expectedHeight) * relY);
			Assert.That(child.X, Is.EqualTo(expectedX).Within(0.0001));
			Assert.That(child.Y, Is.EqualTo(expectedY).Within(0.0001));
			Assert.That(child.Width, Is.EqualTo(expectedWidth).Within(0.0001));
			Assert.That(child.Height, Is.EqualTo(expectedHeight).Within(0.0001));
		}

		[Test]
		public void SizeRequestWithNormalChild()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View();

			// ChildSizeReq == 100x20
			abs.Children.Add(child, new RectangleF(10, 20, 30, 40));

			var sizeReq = abs.GetSizeRequest(float.PositiveInfinity, float.PositiveInfinity);

			Assert.AreEqual(new SizeF(40, 60), sizeReq.Request);
			Assert.AreEqual(new SizeF(40, 60), sizeReq.Minimum);
		}

		[Test]
		public void SizeRequestWithRelativePositionChild()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View();

			// ChildSizeReq == 100x20
			abs.Children.Add(child, new RectangleF(0.5f, 0.5f, 30, 40), AbsoluteLayoutFlags.PositionProportional);

			var sizeReq = abs.GetSizeRequest(float.PositiveInfinity, float.PositiveInfinity);

			Assert.AreEqual(new SizeF(30, 40), sizeReq.Request);
			Assert.AreEqual(new SizeF(30, 40), sizeReq.Minimum);
		}

		[Test]
		public void SizeRequestWithRelativeChild()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View
			{
				IsPlatformEnabled = true
			};

			// ChildSizeReq == 100x20
			abs.Children.Add(child, new RectangleF(0.5f, 0.5f, 0.5f, 0.5f), AbsoluteLayoutFlags.All);

			var sizeReq = abs.GetSizeRequest(float.PositiveInfinity, float.PositiveInfinity);

			Assert.AreEqual(new SizeF(200, 40), sizeReq.Request);
			Assert.AreEqual(new SizeF(0, 0), sizeReq.Minimum);
		}

		[Test]
		public void SizeRequestWithRelativeSizeChild()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View
			{
				IsPlatformEnabled = true
			};

			// ChildSizeReq == 100x20
			abs.Children.Add(child, new RectangleF(10, 20, 0.5f, 0.5f), AbsoluteLayoutFlags.SizeProportional);

			var sizeReq = abs.GetSizeRequest(float.PositiveInfinity, float.PositiveInfinity);

			Assert.AreEqual(new SizeF(210, 60), sizeReq.Request);
			Assert.AreEqual(new SizeF(10, 20), sizeReq.Minimum);
		}

		[Test]
		public void MeasureInvalidatedFiresWhenFlagsChanged()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View
			{
				IsPlatformEnabled = true
			};

			abs.Children.Add(child, new RectangleF(1, 1, 100, 100));

			bool fired = false;
			abs.MeasureInvalidated += (sender, args) => fired = true;

			AbsoluteLayout.SetLayoutFlags(child, AbsoluteLayoutFlags.PositionProportional);

			Assert.True(fired);
		}

		[Test]
		public void MeasureInvalidatedFiresWhenBoundsChanged()
		{
			var abs = new AbsoluteLayout
			{
				IsPlatformEnabled = true
			};

			var child = new View
			{
				IsPlatformEnabled = true
			};

			abs.Children.Add(child, new RectangleF(1, 1, 100, 100));

			bool fired = false;
			abs.MeasureInvalidated += (sender, args) => fired = true;

			AbsoluteLayout.SetLayoutBounds(child, new RectangleF(2, 2, 200, 200));

			Assert.True(fired);
		}

		[TestCase("en-US"), TestCase("tr-TR")]
		public void TestBoundsTypeConverter(string culture)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

			var converter = new BoundsTypeConverter();

			Assert.IsTrue(converter.CanConvertFrom(typeof(string)));
			Assert.AreEqual(new RectangleF(3, 4, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), converter.ConvertFromInvariantString("3, 4"));
			Assert.AreEqual(new RectangleF(3, 4, 20, 30), converter.ConvertFromInvariantString("3, 4, 20, 30"));
			Assert.AreEqual(new RectangleF(3, 4, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), converter.ConvertFromInvariantString("3, 4, AutoSize, AutoSize"));
			Assert.AreEqual(new RectangleF(3, 4, AbsoluteLayout.AutoSize, 30), converter.ConvertFromInvariantString("3, 4, AutoSize, 30"));
			Assert.AreEqual(new RectangleF(3, 4, 20, AbsoluteLayout.AutoSize), converter.ConvertFromInvariantString("3, 4, 20, AutoSize"));

			var autoSize = "AutoSize";
			Assert.AreEqual(new RectangleF(3.3f, 4.4f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), converter.ConvertFromInvariantString("3.3, 4.4, " + autoSize + ", AutoSize"));
			Assert.AreEqual(new RectangleF(3.3f, 4.4f, 5.5f, 6.6f), converter.ConvertFromInvariantString("3.3, 4.4, 5.5, 6.6"));
		}
	}
}
