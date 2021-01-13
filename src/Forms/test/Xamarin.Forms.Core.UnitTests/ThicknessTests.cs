using System;
using System.Globalization;
using System.Graphics;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;


namespace Xamarin.Forms.Core.UnitTests
{
	[TestFixture]
	public class ThicknessTests : BaseTestFixture
	{
		[Test]
		public void Constructor()
		{
			var thickness = new Thickness();

			Assert.AreEqual(0, thickness.Left);
			Assert.AreEqual(0, thickness.Top);
			Assert.AreEqual(0, thickness.Right);
			Assert.AreEqual(0, thickness.Bottom);
			Assert.AreEqual(0, thickness.HorizontalThickness);
			Assert.AreEqual(0, thickness.VerticalThickness);
		}

		[Test]
		public void UniformParameterizedConstructor()
		{
			var thickness = new Thickness(3);

			Assert.AreEqual(3, thickness.Left);
			Assert.AreEqual(3, thickness.Top);
			Assert.AreEqual(3, thickness.Right);
			Assert.AreEqual(3, thickness.Bottom);
			Assert.AreEqual(6, thickness.HorizontalThickness);
			Assert.AreEqual(6, thickness.VerticalThickness);
		}

		[Test]
		public void HorizontalVerticalParameterizedConstructor()
		{
			var thickness = new Thickness(4, 5);

			Assert.AreEqual(4, thickness.Left);
			Assert.AreEqual(5, thickness.Top);
			Assert.AreEqual(4, thickness.Right);
			Assert.AreEqual(5, thickness.Bottom);
			Assert.AreEqual(8, thickness.HorizontalThickness);
			Assert.AreEqual(10, thickness.VerticalThickness);
		}

		[Test]
		public void ParameterizedConstructor()
		{
			var thickness = new Thickness(1, 2, 3, 4);

			Assert.AreEqual(1, thickness.Left);
			Assert.AreEqual(2, thickness.Top);
			Assert.AreEqual(3, thickness.Right);
			Assert.AreEqual(4, thickness.Bottom);
			Assert.AreEqual(4, thickness.HorizontalThickness);
			Assert.AreEqual(6, thickness.VerticalThickness);
		}

		[Test]
		public void ParameterizedConstuctorfloats()
		{
			var thickness = new Thickness(1.2f, 3.3f, 4.2f, 10.66f);
			Assert.AreEqual(1.2f, thickness.Left);
			Assert.AreEqual(3.3f, thickness.Top);
			Assert.AreEqual(4.2f, thickness.Right);
			Assert.AreEqual(10.66f, thickness.Bottom);
			Assert.AreEqual(5.4f, thickness.HorizontalThickness);
			Assert.AreEqual(13.96f, thickness.VerticalThickness);
		}

		[Test]
		public void Equality()
		{
			Assert.False(new Thickness().Equals(null));
			Assert.False(new Thickness().Equals("Thickness"));
			Assert.False(new Thickness().Equals(new Thickness(1, 2, 3, 4)));
			Assert.True(new Thickness().Equals(new Thickness()));

			Assert.True(new Thickness() == new Thickness());
			Assert.True(new Thickness(4, 3, 2, 1) != new Thickness(1, 2, 3, 4));
		}

		[Test]
		public void HashCode([Range(3, 4)] float l1, [Range(3, 4)] float t1, [Range(3, 4)] float r1, [Range(3, 4)] float b1,
							  [Range(3, 4)] float l2, [Range(3, 4)] float t2, [Range(3, 4)] float r2, [Range(3, 4)] float b2)
		{
			bool result = new Thickness(l1, t1, r1, b1).GetHashCode() == new Thickness(l2, t2, r2, b2).GetHashCode();
			if (l1 == l2 && t1 == t2 && r1 == r2 && b1 == b2)
				Assert.True(result);
			else
				Assert.False(result);
		}

		[Test]
		public void ImplicitConversionFromSize()
		{
			Thickness thickness = new Thickness();
			Assert.DoesNotThrow(() => thickness = new SizeF(42, 84));
			Assert.AreEqual(new Thickness(42, 84), thickness);

			Assert.DoesNotThrow(() => thickness = 42);
			Assert.AreEqual(new Thickness(42), thickness);
		}

		[Test]
		public void TestThicknessTypeConverter()
		{
			var converter = new ThicknessTypeConverter();
			Assert.True(converter.CanConvertFrom(typeof(string)));
			Assert.AreEqual(new Thickness(1), converter.ConvertFromInvariantString("1"));
			Assert.AreEqual(new Thickness(1, 2), converter.ConvertFromInvariantString("1, 2"));
			Assert.AreEqual(new Thickness(1, 2, 3, 4), converter.ConvertFromInvariantString("1, 2, 3, 4"));
			Assert.AreEqual(new Thickness(1.1f, 2), converter.ConvertFromInvariantString("1.1,2"));
			Assert.Throws<InvalidOperationException>(() => converter.ConvertFromInvariantString(""));
		}

		[Test]
		public void ThicknessTypeConverterfloats()
		{
			var converter = new ThicknessTypeConverter();
			Assert.AreEqual(new Thickness(1.3f), converter.ConvertFromInvariantString("1.3"));
			Assert.AreEqual(new Thickness(1.4f, 2.8f), converter.ConvertFromInvariantString("1.4, 2.8"));
			Assert.AreEqual(new Thickness(1.6f, 2.1f, 3.8f, 4.2f), converter.ConvertFromInvariantString(" 1.6 , 2.1, 3.8, 4.2"));
			Assert.AreEqual(new Thickness(1.1f, 2), converter.ConvertFromInvariantString("1.1,2"));
		}
	}
}
