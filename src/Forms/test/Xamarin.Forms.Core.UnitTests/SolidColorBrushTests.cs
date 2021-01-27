using System.Graphics;
using NUnit.Framework;

namespace Xamarin.Forms.Core.UnitTests
{
	public class SolidColorBrushTests : BaseTestFixture
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void TestConstructor()
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			Assert.IsNull(solidColorBrush.Color, "Color");
		}

		[Test]
		public void TestConstructorUsingColor()
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Red);
			Assert.AreEqual("[Color: Red=1, Green=0, Blue=0, Alpha=1]", solidColorBrush.Color.ToString(), "Color");
		}

		[Test]
		public void TestEmptySolidColorBrush()
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			Assert.AreEqual(true, solidColorBrush.IsEmpty, "IsEmpty");

			SolidColorBrush red = Brush.Red;
			Assert.AreEqual(false, red.IsEmpty, "IsEmpty");
		}

		[Test]
		public void TestNullOrEmptySolidColorBrush()
		{
			SolidColorBrush nullSolidColorBrush = null;
			Assert.AreEqual(true, Brush.IsNullOrEmpty(nullSolidColorBrush), "IsNullOrEmpty");

			SolidColorBrush emptySolidColorBrush = new SolidColorBrush();
			Assert.AreEqual(true, Brush.IsNullOrEmpty(emptySolidColorBrush), "IsNullOrEmpty");

			SolidColorBrush solidColorBrush = Brush.Yellow;
			Assert.AreEqual(false, Brush.IsNullOrEmpty(solidColorBrush), "IsNullOrEmpty");
		}

		[Test]
		public void TestSolidColorBrushFromColor()
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Red);
			Assert.IsNotNull(solidColorBrush.Color);
			Assert.AreEqual("#FF0000FF", solidColorBrush.Color.ToHex(true));
		}

		[Test]
		public void TestDefaultBrushes()
		{
			SolidColorBrush black = Brush.Black;
			Assert.IsNotNull(black.Color);
			Assert.AreEqual("#000000FF", black.Color.ToHex(true));

			SolidColorBrush white = Brush.White;
			Assert.IsNotNull(white.Color);
			Assert.AreEqual("#FFFFFFFF", white.Color.ToHex(true));
		}
	}
}