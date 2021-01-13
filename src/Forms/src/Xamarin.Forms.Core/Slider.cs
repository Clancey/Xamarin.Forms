using System;
using System.ComponentModel;
using System.Graphics;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public partial class Slider : View, ISliderController, IElementConfiguration<Slider>
	{
		public static readonly BindableProperty MinimumProperty = BindableProperty.Create("Minimum", typeof(float), typeof(Slider), 0d, validateValue: (bindable, value) =>
		{
			var slider = (Slider)bindable;
			return (float)value < slider.Maximum;
		}, coerceValue: (bindable, value) =>
		{
			var slider = (Slider)bindable;
			slider.Value = slider.Value.Clamp((float)value, slider.Maximum);
			return value;
		});

		public static readonly BindableProperty MaximumProperty = BindableProperty.Create("Maximum", typeof(float), typeof(Slider), 1d, validateValue: (bindable, value) =>
		{
			var slider = (Slider)bindable;
			return (float)value > slider.Minimum;
		}, coerceValue: (bindable, value) =>
		{
			var slider = (Slider)bindable;
			slider.Value = slider.Value.Clamp(slider.Minimum, (float)value);
			return value;
		});

		public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(float), typeof(Slider), 0d, BindingMode.TwoWay, coerceValue: (bindable, value) =>
		{
			var slider = (Slider)bindable;
			return ((float)value).Clamp(slider.Minimum, slider.Maximum);
		}, propertyChanged: (bindable, oldValue, newValue) =>
		{
			var slider = (Slider)bindable;
			slider.ValueChanged?.Invoke(slider, new ValueChangedEventArgs((float)oldValue, (float)newValue));
		});

		public static readonly BindableProperty MinimumTrackColorProperty = BindableProperty.Create(nameof(MinimumTrackColor), typeof(Color), typeof(Slider), null);

		public static readonly BindableProperty MaximumTrackColorProperty = BindableProperty.Create(nameof(MaximumTrackColor), typeof(Color), typeof(Slider), null);

		public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(Slider), null);

		public static readonly BindableProperty ThumbImageSourceProperty = BindableProperty.Create(nameof(ThumbImageSource), typeof(ImageSource), typeof(Slider), default(ImageSource));

		[Obsolete("ThumbImageProperty is obsolete as of 4.0.0. Please use ThumbImageSourceProperty instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly BindableProperty ThumbImageProperty = ThumbImageSourceProperty;

		public static readonly BindableProperty DragStartedCommandProperty = BindableProperty.Create(nameof(DragStartedCommand), typeof(ICommand), typeof(Slider), default(ICommand));

		public static readonly BindableProperty DragCompletedCommandProperty = BindableProperty.Create(nameof(DragCompletedCommand), typeof(ICommand), typeof(Slider), default(ICommand));

		readonly Lazy<PlatformConfigurationRegistry<Slider>> _platformConfigurationRegistry;

		public Slider()
		{
			_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<Slider>>(() => new PlatformConfigurationRegistry<Slider>(this));
		}

		public Slider(float min, float max, float val) : this()
		{
			if (min >= max)
				throw new ArgumentOutOfRangeException("min");

			if (max > Minimum)
			{
				Maximum = max;
				Minimum = min;
			}
			else
			{
				Minimum = min;
				Maximum = max;
			}
			Value = val.Clamp(min, max);
		}

		public Color MinimumTrackColor
		{
			get { return (Color)GetValue(MinimumTrackColorProperty); }
			set { SetValue(MinimumTrackColorProperty, value); }
		}

		public Color MaximumTrackColor
		{
			get { return (Color)GetValue(MaximumTrackColorProperty); }
			set { SetValue(MaximumTrackColorProperty, value); }
		}

		public Color ThumbColor
		{
			get { return (Color)GetValue(ThumbColorProperty); }
			set { SetValue(ThumbColorProperty, value); }
		}

		public ImageSource ThumbImageSource
		{
			get { return (ImageSource)GetValue(ThumbImageSourceProperty); }
			set { SetValue(ThumbImageSourceProperty, value); }
		}

		[Obsolete("ThumbImage is obsolete as of 4.0.0. Please use ThumbImageSource instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public FileImageSource ThumbImage
		{
			get { return GetValue(ThumbImageProperty) as FileImageSource; }
			set { SetValue(ThumbImageProperty, value); }
		}

		public ICommand DragStartedCommand
		{
			get { return (ICommand)GetValue(DragStartedCommandProperty); }
			set { SetValue(DragStartedCommandProperty, value); }
		}

		public ICommand DragCompletedCommand
		{
			get { return (ICommand)GetValue(DragCompletedCommandProperty); }
			set { SetValue(DragCompletedCommandProperty, value); }
		}

		public float Maximum
		{
			get { return (float)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		public float Minimum
		{
			get { return (float)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		public float Value
		{
			get { return (float)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public event EventHandler<ValueChangedEventArgs> ValueChanged;
		public event EventHandler DragStarted;
		public event EventHandler DragCompleted;

		void ISliderController.SendDragStarted()
		{
			if (IsEnabled)
			{
				DragStartedCommand?.Execute(null);
				DragStarted?.Invoke(this, null);
			}
		}

		void ISliderController.SendDragCompleted()
		{
			if (IsEnabled)
			{
				DragCompletedCommand?.Execute(null);
				DragCompleted?.Invoke(this, null);
			}
		}

		public IPlatformElementConfiguration<T, Slider> On<T>() where T : IConfigPlatform
		{
			return _platformConfigurationRegistry.Value.On<T>();
		}
	}
}