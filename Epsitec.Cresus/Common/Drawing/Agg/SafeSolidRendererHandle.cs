﻿//	Copyright © 2008, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace Epsitec.Common.Drawing.Agg
{
	/// <summary>
	/// The <c>SafeSolidRendererHandle</c> class implements a wrapper for the
	/// native AGG solid renderer handle.
	/// </summary>
	[SecurityPermission (SecurityAction.InheritanceDemand, UnmanagedCode=true)]
	[SecurityPermission (SecurityAction.Demand, UnmanagedCode=true)]
	sealed class SafeSolidRendererHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SafeSolidRendererHandle"/> class.
		/// </summary>
		public SafeSolidRendererHandle()
			: base (true)
		{
		}

		/// <summary>
		/// Converts the safe handle to an <c>IntPtr</c>.
		/// </summary>
		/// <param name="handle">The safe handle.</param>
		/// <returns>The unwrapped handle.</returns>
		public static implicit operator System.IntPtr(SafeSolidRendererHandle handle)
		{
			return handle.handle;
		}

		/// <summary>
		/// Creates the AGG solid renderer and attaches a pixmap.
		/// </summary>
		public void Create(System.IntPtr pixmapHandle)
		{
			System.Diagnostics.Debug.Assert (this.handle == System.IntPtr.Zero);
			this.handle = AntiGrain.Renderer.Solid.New (pixmapHandle);
		}

		/// <summary>
		/// Deletes the AGG solid renderer.
		/// </summary>
		public void Delete()
		{
			System.Diagnostics.Debug.Assert (this.handle != System.IntPtr.Zero);
			this.ReleaseHandle ();
		}

		/// <summary>
		/// Executes the code required to free the handle.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the handle is released successfully; otherwise, in
		/// the event of a catastrophic failure, <c>false</c>. In this case,
		/// it generates a <c>ReleaseHandleFailed</c> Managed Debugging Assistant.
		/// </returns>
		[ReliabilityContract (Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			AntiGrain.Renderer.Solid.Delete (this.handle);
			this.handle = System.IntPtr.Zero;
			return true;
		}
	}
}
