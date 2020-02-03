using System;
using DotNetUI.Renderers;

namespace DotNetUI.Views {
	public interface IPropertyMapperView {
		PropertyMapper<IView> GetPropertyMapperOverrides ();
	}
}
