using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	[TypeConverter(typeof(ConstraintTypeConverter))]
	public sealed class Constraint
	{
		Func<RelativeLayout, float> _measureFunc;

		Constraint()
		{
		}

		internal IEnumerable<View> RelativeTo { get; set; }

		public static Constraint Constant(float size)
		{
			var result = new Constraint { _measureFunc = parent => size };

			return result;
		}

		public static Constraint FromExpression(Expression<Func<float>> expression)
		{
			Func<float> compiled = expression.Compile();
			var result = new Constraint
			{
				_measureFunc = layout => compiled(),
				RelativeTo = ExpressionSearch.Default.FindObjects<View>(expression).ToArray() // make sure we have our own copy
			};

			return result;
		}

		public static Constraint RelativeToParent(Func<RelativeLayout, float> measure)
		{
			var result = new Constraint { _measureFunc = measure };

			return result;
		}

		public static Constraint RelativeToView(View view, Func<RelativeLayout, View, float> measure)
		{
			var result = new Constraint { _measureFunc = layout => measure(layout, view), RelativeTo = new[] { view } };

			return result;
		}

		internal float Compute(RelativeLayout parent)
		{
			return _measureFunc(parent);
		}
	}
}