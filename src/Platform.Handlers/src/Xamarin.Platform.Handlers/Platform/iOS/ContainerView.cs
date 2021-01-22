using System;
using System.Graphics;
using System.Graphics.CoreGraphics;
using CoreAnimation;
using CoreGraphics;
using UIKit;
namespace Xamarin.Platform
{
	public partial class ContainerView : UIView
	{
		public ContainerView()
		{
			AutosizesSubviews = true;
		}

		UIView? _mainView;
		public UIView? MainView
		{
			get => _mainView;
			set
			{
				if (_mainView == value)
					return;

				if (_mainView != null)
				{
					//Cleanup!
					_mainView.RemoveFromSuperview();
				}
				_mainView = value;
				if (_mainView == null)
					return;
				this.Frame = _mainView.Frame;
				var oldParent = _mainView.Superview;
				if (oldParent != null)
					oldParent.InsertSubviewAbove(this, _mainView);


				//_size = _mainView.Bounds.Size;
				_mainView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
				_mainView.Frame = Bounds;

				//if (_overlayView != null)
				//	InsertSubviewBelow (_mainView, _overlayView);
				//else
				AddSubview(_mainView);
			}
		}

		public override void SizeToFit()
		{
			if (MainView == null)
				return;
			MainView.SizeToFit();
			this.Bounds = MainView.Bounds;
			base.SizeToFit();
		}

		CAShapeLayer? Mask
		{
			get => MainView?.Layer.Mask as CAShapeLayer;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			set => MainView.Layer.Mask = value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
		}

		partial void ClipShapeChanged()
		{
			lastMaskSize = SizeF.Zero;
			if (Frame == CGRect.Empty)
				return;
		}
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			SetClipShape();
		}

		SizeF lastMaskSize = SizeF.Zero;
		void SetClipShape()
		{
			var mask = Mask;
			if (mask == null && ClipShape == null)
				return;
			mask ??= Mask = new CAShapeLayer();
			var frame = Frame;
			var bounds = new RectangleF(0, 0, (float)frame.Width, (float)frame.Height);
			if (bounds.Size == lastMaskSize)
				return;
			lastMaskSize = bounds.Size;
			var path = _clipShape?.PathForBounds(bounds);
			mask.Path = path?.AsCGPath();
		}

	}
}
