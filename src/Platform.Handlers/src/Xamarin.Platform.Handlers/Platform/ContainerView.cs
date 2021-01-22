using System;
using Xamarin.Platform.Shapes;

namespace Xamarin.Platform
{
	public partial class ContainerView
	{
		private IShape? _clipShape;
		public IShape? ClipShape
		{
			get => _clipShape;
			set
			{
				if (_clipShape == value)
					return;
				_clipShape = value;
				ClipShapeChanged();
			}
		}

		partial void ClipShapeChanged();
	}
}
