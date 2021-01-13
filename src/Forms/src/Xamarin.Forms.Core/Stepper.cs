using System;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public class Stepper : View, IElementConfiguration<Stepper>
	{
		public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(float), typeof(Stepper), 100f,
			validateValue: (bindable, value) => (float)value > ((Stepper)bindable).Minimum,
			coerceValue: (bindable, value) =>
			{
				var stepper = (Stepper)bindable;
				stepper.Value = stepper.Value.Clamp(stepper.Minimum, (float)value);
				return value;
			});

		public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(float), typeof(Stepper), 0f,
			validateValue: (bindable, value) => (float)value < ((Stepper)bindable).Maximum,
			coerceValue: (bindable, value) =>
			{
				var stepper = (Stepper)bindable;
				stepper.Value = stepper.Value.Clamp((float)value, stepper.Maximum);
				return value;
			});

		public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(float), typeof(Stepper), 0f, BindingMode.TwoWay,
			coerceValue: (bindable, value) =>
			{
				var stepper = (Stepper)bindable;
				return ((float)Math.Round((float)value, stepper.digits)).Clamp(stepper.Minimum, stepper.Maximum);
			},
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var stepper = (Stepper)bindable;
				stepper.ValueChanged?.Invoke(stepper, new ValueChangedEventArgs((float)oldValue, (float)newValue));
			});

		int digits = 4;
		//'-log10(increment) + 4' as rounding digits gives us 4 significant decimal digits after the most significant one.
		//If your increment uses more than 4 significant digits, you're holding it wrong.
		public static readonly BindableProperty IncrementProperty = BindableProperty.Create(nameof(Increment), typeof(float), typeof(Stepper), 1.0f,
			propertyChanged: (b, o, n) => { ((Stepper)b).digits = (int)((float)-Math.Log10((float)n) + 4).Clamp(1, 15); });

		readonly Lazy<PlatformConfigurationRegistry<Stepper>> _platformConfigurationRegistry;

		public Stepper() => _platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<Stepper>>(() => new PlatformConfigurationRegistry<Stepper>(this));

		public Stepper(float min, float max, float val, float increment) : this()
		{
			if (min >= max)
				throw new ArgumentOutOfRangeException(nameof(min));
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

			Increment = increment;
			Value = val.Clamp(min, max);
		}

		public float Increment
		{
			get => (float)GetValue(IncrementProperty);
			set => SetValue(IncrementProperty, value);
		}

		public float Maximum
		{
			get => (float)GetValue(MaximumProperty);
			set => SetValue(MaximumProperty, value);
		}

		public float Minimum
		{
			get => (float)GetValue(MinimumProperty);
			set => SetValue(MinimumProperty, value);
		}

		public float Value
		{
			get => (float)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		public event EventHandler<ValueChangedEventArgs> ValueChanged;

		public IPlatformElementConfiguration<T, Stepper> On<T>() where T : IConfigPlatform => _platformConfigurationRegistry.Value.On<T>();

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("deprecated without replacement in 4.8.0")]
		public int StepperPosition { get; set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("deprecated without replacement in 4.8.0")]
		public static readonly BindableProperty StepperPositionProperty = BindableProperty.Create(nameof(StepperPosition), typeof(int), typeof(Stepper), 0);
	}
}