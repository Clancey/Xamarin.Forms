﻿using System;
using UIKit;

namespace Xamarin.Platform
{
	public static class HandlerExtensions
	{
		public static UIView ToNative(this IView view)
		{
			_ = view ?? throw new ArgumentNullException(nameof(view));

			var handler = view.Handler;

			if (handler == null)
			{
				//This is how MVU works. It collapses views down
				if (view is IReplaceableView ir)
					view = ir.ReplacedView;
				handler = Registrar.Handlers.GetHandler(view.GetType());
				view.Handler = handler;
			}

			handler.SetVirtualView(view);

			if (!(handler.NativeView is UIView result))
			{
				throw new InvalidOperationException($"Unable to convert {view} to {typeof(UIView)}");
			}

			return result;
		}
	}
}