using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2783, "MemoryLeak in FrameRenderer", PlatformAffected.Android, NavigationBehavior.PushModalAsync)]
	public class Issue2783 : ContentPage
	{
		public Issue2783()
		{
			Frame frPatientInfo = new Frame
			{
				BorderColor = Colors.Black,
				BackgroundColor = Colors.White,
				HasShadow = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Padding = 5,
				Content = new AbsoluteLayout
				{
					BackgroundColor = Colors.Red,
					HeightRequest = 1000,
					WidthRequest = 2000,
				}
			};

			Content = frPatientInfo;

		}
	}
}
