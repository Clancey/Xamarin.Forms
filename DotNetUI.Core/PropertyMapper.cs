using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetUI
{
	public class PropertyMapper {
		internal Dictionary<string, Action<IViewRenderer, IView>> genericMap = new Dictionary<string, Action<IViewRenderer, IView>> ();
		protected virtual void UpdateProperty (string key, IViewRenderer viewRenderer, IView virtualView)
		{
			if (genericMap.TryGetValue (key, out var action))
				action?.Invoke (viewRenderer, virtualView);
		}
		public void UpdateProperty (IViewRenderer viewRenderer, IView virtualView, string property)
		{
			if (virtualView == null)
				return;
			UpdateProperty (property, viewRenderer, virtualView);
		}
	}

	public class PropertyMapper<TVirtualView> : PropertyMapper, IEnumerable
		where TVirtualView : IView {
		private PropertyMapper chained;
		public PropertyMapper Chained {
			get => chained;
			set {
				chained = value;
				cachedKeys = null;
			}
		}

		ICollection<string> cachedKeys;
		public ICollection<string> Keys => cachedKeys ??= (Chained?.genericMap.Keys.Union (genericMap.Keys).ToList () as ICollection<string> ?? genericMap.Keys);

		public int Count => Keys.Count;

		public bool IsReadOnly => false;

		public Action<IViewRenderer, TVirtualView> this [string key] {
			set => genericMap [key] = (r, v) => value?.Invoke (r, (TVirtualView)v);
		}

		public PropertyMapper ()
		{
		}

		public PropertyMapper (PropertyMapper chained)
		{
			Chained = chained;
		}

		public void UpdateProperties (IViewRenderer viewRenderer, IView virtualView)
		{
			if (virtualView == null)
				return;
			foreach (var key in Keys) {
				UpdateProperty (key, viewRenderer, virtualView);
			}
		}

		protected override void UpdateProperty (string key, IViewRenderer viewRenderer, IView virtualView)
		{
			if (genericMap.TryGetValue (key, out var action))
				action?.Invoke (viewRenderer, virtualView);
			else
				Chained?.UpdateProperty (viewRenderer, virtualView, key);
		}

		public void Add (string key, Action<IViewRenderer, TVirtualView> action)
			=> this [key] = action;

		IEnumerator IEnumerable.GetEnumerator () => genericMap.GetEnumerator ();

	}
}
