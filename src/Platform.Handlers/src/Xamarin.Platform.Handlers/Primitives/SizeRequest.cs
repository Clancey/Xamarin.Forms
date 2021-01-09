using System.Diagnostics;
using System.Graphics;

namespace Xamarin.Forms
{
	[DebuggerDisplay("Request={Request.Width}x{Request.Height}, Minimum={Minimum.Width}x{Minimum.Height}")]
	public struct SizeRequest
	{
		public SizeF Request { get; set; }

		public SizeF Minimum { get; set; }

		public SizeRequest(SizeF request, SizeF minimum)
		{
			Request = request;
			Minimum = minimum;
		}

		public SizeRequest(SizeF request)
		{
			Request = request;
			Minimum = request;
		}

		public override string ToString()
		{
			return string.Format("{{Request={0} Minimum={1}}}", Request, Minimum);
		}
	}
}