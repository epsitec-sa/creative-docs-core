﻿using Epsitec.Common.Support.Extensions;

using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;


namespace Epsitec.Common.Support
{


	/// <summary>
	/// The <see cref="DisposableWrapper"/> class provides an easy way to wrap a call to an
	/// <see cref="Action"/> into a <see cref="IDisposable"/> object that will ensure that it will
	/// be called exactly once if the object is disposed and that an
	/// <see cref="InvalidOperationException"/> will be thrown if the object has not been disposed.
	/// </summary>
	public sealed class DisposableWrapper : IDisposable
	{


		private DisposableWrapper(Action action)
		{
			this.action = action;

			this.actionDone = false;
		}


		~DisposableWrapper()
		{
			if (!this.actionDone)
			{
				throw new InvalidOperationException ("Caller forgot to call Dispose");
			}
		}


		#region IDisposable Members


		public void Dispose()
		{
			if (!this.actionDone)
			{
				this.action ();

				this.actionDone = true;
			}

			GC.SuppressFinalize (this);
		}


		#endregion


		private readonly Action action;


		private bool actionDone;


		/// <summary>
		/// Gets an instance of <see cref="IDisposable"/> that will call <paramref name="action"/>
		/// when disposed for the first time and that will throw an <see cref="InvalidOperationException"/>
		/// if not disposed.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> to execute when disposed.</param>
		/// <returns>The <see cref="IDisposable"/> object</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="action"/> is <c>null</c>.</exception>
		public static IDisposable Get(Action action)
		{
			action.ThrowIfNull ("action");

			return new DisposableWrapper (action);
		}


		/// <summary>
		/// Gets an instance of <see cref="IDisposable"/> that will dispose all objects in
		/// <paramref name="disposables"/> when disposed and that will throw an
		/// <see cref="InvalidOperationException"/> if not disposed.
		/// </summary>
		/// <remarks>
		/// All the given objects will be disposed, even if one of then throws an exception when its
		/// dispose method is called. If a single object throws an exception, it is catch and
		/// thrown after all other objects have been disposed. If more than one object throws an
		/// exception when disposed, they are catch and throw after all other objects have been
		/// disposed, grouped in an instance of GroupedException.
		/// </remarks>
		/// <param name="disposables">The objects to dispose.</param>
		/// <returns>The <see cref="IDisposable"/> object</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="action"/> is <c>null</c>.</exception>
		public static IDisposable Combine(params IDisposable[] disposables)
		{
			disposables.ThrowIfNull ("disposables");
			disposables.ThrowIf (ds => ds.Any (d => d == null), "disposables cannot contain null items");

			Action initializer = () => { };
			
			Action finalizer = () =>
			{
				List<Exception> exceptions = new List<Exception> ();

				foreach (IDisposable disposable in disposables)
				{
					try
					{
						disposable.Dispose ();
					}
					catch (Exception e)
					{
						exceptions.Add (e);
					}
				}

				if (exceptions.Count == 1)
				{
					throw exceptions.Single ();
				}
				else if (exceptions.Count > 1)
				{
					throw new GroupedException (exceptions);
				}
			};

			return DisposableWrapper.Get (finalizer);
		}


	}


}
