using System.Collections.ObjectModel;
using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	[TypeConverter(typeof(PointCollectionConverter))]
	public sealed class PointCollection : ObservableCollection<Point>
	{

	}
}