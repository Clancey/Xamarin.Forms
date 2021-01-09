using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 1026, "Label cropping", PlatformAffected.iOS | PlatformAffected.WinPhone, NavigationBehavior.PushModalAsync)]
	public class Issue1026 : ContentPage
	{
		public Issue1026()
		{
			var instructions = new Label
			{
				Text = "The label at the bottom of the form should read 'by subscribing," +
				" you accept the general conditions.'; if the label is truncated, this test has failed."
			};

			var scrollView =
				new ScrollView
				{
					BackgroundColor = Colors.FromHex("#dae1eb"),
					Content =
					new StackLayout
					{
						Padding = new Thickness(0, 18),
						Spacing = 10,
						Orientation = StackOrientation.Vertical,
						Children = {
						new Button {
							BackgroundColor = Colors.FromHex ("#006599"),
							TextColor = Colors.White,
							Text = "Subscribe with LinkedIn",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
#pragma warning disable 0618
							BorderRadius = 0,
#pragma warning restore
						},
						new Entry {
							Placeholder = "Professional email",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
							Keyboard = Keyboard.Email,
						},
						new Entry {
							Placeholder = "Firstname",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
						},
						new Entry {
							Placeholder = "Lastname",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
						},
						new Entry {
							Placeholder = "Company",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
						},
						new Entry {
							Placeholder = "Password",
							WidthRequest = 262,
							IsPassword = true,
							HorizontalOptions = LayoutOptions.Center,
						},
						new Entry {
							Placeholder = "Confirm password",
							WidthRequest = 262,
							IsPassword = true,
							HorizontalOptions = LayoutOptions.Center,
						},
						new Button {
							BackgroundColor = Colors.FromHex ("#05addc"),
							TextColor = Colors.White,
							Text = "Create an account",
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
#pragma warning disable 0618
							BorderRadius = 0,
#pragma warning restore
						},
						new Label {
							Text = "by subscribing, you accept the general conditions.",
							TextColor = Colors.White,
#pragma warning disable 618
							XAlign = TextAlignment.Center,
#pragma warning restore 618

#pragma warning disable 618
							Font = Font.SystemFontOfSize (NamedSize.Micro),
#pragma warning restore 618
							WidthRequest = 262,
							HorizontalOptions = LayoutOptions.Center,
						},
					},
					},

				};

			Content = new StackLayout { Children = { instructions, scrollView } };
		}
	}
}
