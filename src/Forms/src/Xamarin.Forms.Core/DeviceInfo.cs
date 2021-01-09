using System;
using System.ComponentModel;
using System.Graphics;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.Internals
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class DeviceInfo : INotifyPropertyChanged, IDisposable
	{
		DeviceOrientation _currentOrientation;
		bool _disposed;

		public DeviceOrientation CurrentOrientation
		{
			get { return _currentOrientation; }
			set
			{
				if (Equals(_currentOrientation, value))
					return;
				_currentOrientation = value;
				OnPropertyChanged();
			}
		}

		public virtual float DisplayRound(float value) =>
			(float)Math.Round(value);

		public abstract SizeF PixelScreenSize { get; }

		public abstract SizeF ScaledScreenSize { get; }

		public abstract float ScalingFactor { get; }

		public void Dispose()
		{
			Dispose(true);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;
			_disposed = true;
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}