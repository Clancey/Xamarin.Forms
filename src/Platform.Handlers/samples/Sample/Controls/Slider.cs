using System;
using System.Graphics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class Slider : Xamarin.Forms.View, ISlider
	{
		float _value;

		public Slider()
		{

		}

		public Slider(float min, float max, float val)
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

		public float Minimum { get; set; }
		public float Maximum { get; set; } = 1;

		public float Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (_value == value)
					return;

				_value = value;
				ValueChanged?.Invoke(value);
			}
		}

		public Color MinimumTrackColor { get; set; }
		public Color MaximumTrackColor { get; set; }
		public Color ThumbColor { get; set; }

		public Action<float> ValueChanged { get; set; }
		public Action DragStarted { get; set; }
		public Action DragCompleted { get; set; }

		public ICommand DragStartedCommand { get; set; }
		public ICommand DragCompletedCommand { get; set; }

		void ISlider.DragStarted()
		{
			if (IsEnabled)
			{
				DragStartedCommand?.Execute(null);
				DragStarted?.Invoke();
			}
		}

		void ISlider.DragCompleted()
		{
			if (IsEnabled)
			{
				DragCompletedCommand?.Execute(null);
				DragCompleted?.Invoke();
			}
		}
	}
}