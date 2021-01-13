using System;

namespace Xamarin.Forms
{
	public sealed class AdaptiveTrigger : StateTriggerBase
	{
		public AdaptiveTrigger()
		{
			UpdateState();
		}

		public float MinWindowHeight
		{
			get => (float)GetValue(MinWindowHeightProperty);
			set => SetValue(MinWindowHeightProperty, value);
		}

		public static readonly BindableProperty MinWindowHeightProperty =
			BindableProperty.Create(nameof(MinWindowHeight), typeof(float), typeof(AdaptiveTrigger), -1f,
				propertyChanged: OnMinWindowHeightChanged);

		static void OnMinWindowHeightChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			((AdaptiveTrigger)bindable).UpdateState();
		}

		public float MinWindowWidth
		{
			get => (float)GetValue(MinWindowWidthProperty);
			set => SetValue(MinWindowWidthProperty, value);
		}

		public static readonly BindableProperty MinWindowWidthProperty =
			BindableProperty.Create(nameof(MinWindowWidthProperty), typeof(float), typeof(AdaptiveTrigger), -1f,
				propertyChanged: OnMinWindowWidthChanged);

		static void OnMinWindowWidthChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			((AdaptiveTrigger)bindable).UpdateState();
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			if (!DesignMode.IsDesignModeEnabled)
			{
				UpdateState();
				Application.Current.MainPage.SizeChanged += OnSizeChanged;
			}
		}

		protected override void OnDetached()
		{
			base.OnDetached();

			Application.Current.MainPage.SizeChanged -= OnSizeChanged;
		}

		void OnSizeChanged(object sender, EventArgs e)
		{
			UpdateState();
		}

		void UpdateState()
		{
			var scaledScreenSize = Device.Info.ScaledScreenSize;

			var w = scaledScreenSize.Width;
			var h = scaledScreenSize.Height;
			var mw = MinWindowWidth;
			var mh = MinWindowHeight;

			SetActive(w >= mw && h >= mh);
		}
	}
}