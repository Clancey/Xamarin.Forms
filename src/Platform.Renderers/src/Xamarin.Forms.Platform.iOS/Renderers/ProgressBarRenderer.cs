using System.ComponentModel;
using System.Graphics;
using CoreGraphics;
using UIKit;

namespace Xamarin.Forms.Platform.iOS
{
	public class ProgressBarRenderer : ViewRenderer<ProgressBar, UIProgressView>
	{
		[Internals.Preserve(Conditional = true)]
		public ProgressBarRenderer()
		{

		}

		public override CGSize SizeThatFits(CGSize size)
		{
			// progress bar will size itself to be as wide as the request, even if its inifinite
			// we want the minimum need size
			var result = base.SizeThatFits(size);
			return new CGSize(10, result.Height);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
		{
			if (e.NewElement != null)
			{
				if (Control == null)
					SetNativeControl(new UIProgressView(UIProgressViewStyle.Default));

				UpdateProgressColor();
				UpdateProgress();
			}

			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == ProgressBar.ProgressColorProperty.PropertyName)
				UpdateProgressColor();
			else if (e.PropertyName == ProgressBar.ProgressProperty.PropertyName)
				UpdateProgress();
		}

		protected override void SetBackgroundColor(Color color)
		{
			base.SetBackgroundColor(color);

			if (Control == null)
				return;

			Control.TrackTintColor = color != null ? color.ToUIColor() : null;
		}

		void UpdateProgressColor()
		{
			Control.ProgressTintColor = Element.ProgressColor == null ? null : Element.ProgressColor.ToUIColor();
		}

		void UpdateProgress()
		{
			Control.Progress = (float)Element.Progress;
		}
	}
}