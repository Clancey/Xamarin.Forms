using System;
using System.Collections.Generic;
using System.Graphics;
using System.Linq.Expressions;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public class BoundsConstraint
	{
		Func<RectangleF> _measureFunc;

		BoundsConstraint()
		{
		}

		internal bool CreatedFromExpression { get; set; }
		internal IEnumerable<View> RelativeTo { get; set; }

		public static BoundsConstraint FromExpression(Expression<Func<RectangleF>> expression, IEnumerable<View> parents = null)
		{
			return FromExpression(expression, false, parents);
		}

		internal static BoundsConstraint FromExpression(Expression<Func<RectangleF>> expression, bool fromExpression, IEnumerable<View> parents = null)
		{
			Func<RectangleF> compiled = expression.Compile();
			var result = new BoundsConstraint
			{
				_measureFunc = compiled,
				RelativeTo = parents ?? ExpressionSearch.Default.FindObjects<View>(expression).ToArray(), // make sure we have our own copy
				CreatedFromExpression = fromExpression
			};

			return result;
		}

		internal RectangleF Compute()
		{
			return _measureFunc();
		}
	}
}