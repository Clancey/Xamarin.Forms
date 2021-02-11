﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Platform.Core;

namespace Xamarin.Platform.HotReload
{
	public static class HotReloadHelper
	{
		public static void AddActiveView(IHotReloadableView view) => ActiveViews.Add(view);
		public static void Reset()
		{
			replacedViews.Clear();
		}
		public static bool IsEnabled { get; set; } = Debugger.IsAttached;

		public static void Register(IHotReloadableView view, params object[] parameters)
		{
			if (!IsEnabled)
				return;
			currentViews[view] = parameters;
		}

		public static void UnRegister(IHotReloadableView view)
		{
			if (!IsEnabled)
				return;
			currentViews.Remove(view);
		}
		public static bool IsReplacedView(IHotReloadableView view, IView newView)
		{
			if (!IsEnabled)
				return false;
			if (view == null || newView == null)
				return false;

			if (!replacedViews.TryGetValue(view.GetType().FullName, out var newViewType))
				return false;
			return newView.GetType() == newViewType;
		}
		public static IView GetReplacedView(IHotReloadableView view)
		{
			if (!IsEnabled)
				return view;
			if (!replacedViews.TryGetValue(view.GetType().FullName, out var newViewType))
				return view;

			currentViews.TryGetValue(view, out var parameters);
			try
			{
				//TODO: Add in a way to use IoC and DI
				var newView = (IView)(parameters?.Length > 0 ? Activator.CreateInstance(newViewType, args: parameters) : Activator.CreateInstance(newViewType));
				TransferState(view, newView);
				return newView;
			}
			catch (MissingMethodException ex)
			{
				Debug.WriteLine("You are using Comet.Reload on a view that requires Parameters. Please call `HotReloadHelper.Register(this, params);` in the constructor;");
				throw ex;
			}

		}

		static void TransferState(IHotReloadableView oldView, IView newView)
		{

			oldView.TransferState(newView);
		}

		internal static readonly WeakList<IHotReloadableView> ActiveViews = new WeakList<IHotReloadableView>();
		static Dictionary<string, Type> replacedViews = new Dictionary<string, Type>();
		static Dictionary<IHotReloadableView, object[]> currentViews = new Dictionary<IHotReloadableView, object[]>();
		static Dictionary<string, List<KeyValuePair<Type, Type>>> replacedHandlers = new Dictionary<string, List<KeyValuePair<Type, Type>>>();
		public static void RegisterReplacedView(string oldViewType, Type newViewType)
		{
			if (!IsEnabled || oldViewType == newViewType.FullName)
				return;

			Console.WriteLine($"{oldViewType} - {newViewType}");

			if (typeof(IHotReloadableView).IsAssignableFrom(newViewType))
				replacedViews[oldViewType] = newViewType;

			if (typeof(IViewHandler).IsAssignableFrom(newViewType))
			{
				if (replacedHandlers.TryGetValue(oldViewType, out var vTypes))
				{
					foreach (var vType in vTypes)
						RegisterHandler(vType, newViewType);
					return;
				}

				var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
				var t = assemblies.Select(x => x.GetType(oldViewType)).FirstOrDefault(x => x != null);
				var views = Registrar.Handlers.GetViewType(t);
				if (views.Count == 0)
				{
					views = Registrar.Handlers.GetViewType(t);
				}
				replacedHandlers[oldViewType] = views;
				foreach (var h in views)
				{
					RegisterHandler(h, newViewType);
				}
			}
			//Call static init if it exists on new classes!
			var staticInit = newViewType.GetMethod("Init", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			staticInit?.Invoke(null, null);
		}

		static void RegisterHandler(KeyValuePair<Type, Type> pair, Type newHandler)
		{
			var view = pair.Key;
			var newType = newHandler;
			if (pair.Value.IsGenericType)
				newType = pair.Value.GetGenericTypeDefinition().MakeGenericType(newHandler);
			Registrar.Handlers.Register(view, newType);
		}

		public static void TriggerReload()
		{
			List<IHotReloadableView>? roots = null;
			while (roots == null)
			{
				try
				{
					//roots = View.ActiveViews.Where(x => (x.Parent is CometApp) || (x.Parent == null && !(x is CometApp) )).ToList();
					roots = ActiveViews.Where(x => x.Parent == null).ToList();
				}
				catch
				{
					//Sometimes we get list changed exception.
				}
			}

			foreach (var view in roots)
			{
				view.Reload();
			}
		}
	}
}