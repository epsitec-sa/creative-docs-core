using System.Runtime.InteropServices;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe Win32Api exporte quelques fonctions de l'API Win32 utilis�es
	/// par des couches tr�s bas niveau.
	/// </summary>
	public class Win32Api
	{
		[DllImport ("User32.dll")]	internal extern static int SetWindowLong(System.IntPtr handle, int index, int value);
		[DllImport ("User32.dll")]	internal extern static int GetWindowLong(System.IntPtr handle, int index);
		[DllImport ("User32.dll", SetLastError=true)]	internal extern static bool UpdateLayeredWindow(System.IntPtr handle, System.IntPtr dst_dc, ref Win32Api.Point dst, ref Win32Api.Size size, System.IntPtr src_dc, ref Win32Api.Point src, int color, ref Win32Api.BlendFunction blend, int flags);
		[DllImport ("User32.dll")]	internal extern static System.IntPtr GetDC(System.IntPtr handle);
		[DllImport ("User32.dll")]	internal extern static int ReleaseDC(System.IntPtr handle, System.IntPtr dc);
		[DllImport ("User32.dll")]	internal extern static bool PostMessage(System.IntPtr handle, int msg, System.IntPtr w_param, System.IntPtr l_param);
		[DllImport ("User32.dll")]  internal extern static int GetWindowThreadProcessId(System.IntPtr handle, out int pid);		
		[DllImport ("User32.dll")]	internal extern static System.IntPtr GetWindow(System.IntPtr handle, int direction);
		[DllImport ("User32.dll")]	internal extern static bool IsWindowVisible(System.IntPtr handle);
		[DllImport ("User32.dll")]	internal extern static bool BringWindowToTop(System.IntPtr handle);
		
		[DllImport ("GDI32.dll")]	internal extern static System.IntPtr CreateCompatibleDC(System.IntPtr dc);
		[DllImport ("GDI32.dll")]	internal extern static System.IntPtr SelectObject(System.IntPtr dc, System.IntPtr handle_object);
		[DllImport ("GDI32.dll")]	internal extern static System.IntPtr DeleteObject(System.IntPtr handle_object);
		[DllImport ("GDI32.dll")]	internal extern static bool DeleteDC(System.IntPtr dc);
		
		[StructLayout(LayoutKind.Sequential, Pack=1)] public struct Point
		{
			public int x;
			public int y;
		};
		
		[StructLayout(LayoutKind.Sequential, Pack=1)] public struct Size
		{
			public int cx;
			public int cy;
		};
		
		[StructLayout(LayoutKind.Sequential, Pack=1)] public struct BlendFunction
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;
		};
		
		
		
		internal static int GetWindowExStyle(System.IntPtr handle)
		{
			return Win32Api.GetWindowLong (handle, Win32Const.GWL_EXSTYLE);
		}
		
		internal static int SetWindowExStyle(System.IntPtr handle, int ex_style)
		{
			return Win32Api.SetWindowLong (handle, Win32Const.GWL_EXSTYLE, ex_style);
		}
		
		internal static bool UpdateLayeredWindow(System.IntPtr handle, System.Drawing.Bitmap bitmap, System.Drawing.Rectangle bounds, double alpha)
		{
			Win32Api.Point src_point = new Win32Api.Point ();
			Win32Api.Point dst_point = new Win32Api.Point ();
			Win32Api.Size  new_size  = new Win32Api.Size ();
			
			Win32Api.BlendFunction blend_function = new Win32Api.BlendFunction ();
			
			//	Get the screen DC, then create a memory based DC compatible with the screen DC.
			
			System.IntPtr screen_dc = Win32Api.GetDC (System.IntPtr.Zero);
			System.IntPtr memory_dc = Win32Api.CreateCompatibleDC (screen_dc);
			
			//	Get access to the bitmap handle contained in the Bitmap object, then select it.
			
			System.IntPtr memory_bitmap = bitmap.GetHbitmap (System.Drawing.Color.FromArgb (0));
			System.IntPtr old_bitmap    = Win32Api.SelectObject (memory_dc, memory_bitmap);
			
			new_size.cx = bounds.Width;
			new_size.cy = bounds.Height;
			dst_point.x = bounds.Left;
			dst_point.y = bounds.Top;
			src_point.x = 0;
			src_point.y = 0;
			
			blend_function.BlendOp				= Win32Const.AC_SRC_OVER;
			blend_function.BlendFlags			= 0;
			blend_function.SourceConstantAlpha	= (byte) System.Math.Min (255, System.Math.Max (0, (int) (alpha * 255.9)));
			blend_function.AlphaFormat			= Win32Const.AC_SRC_ALPHA;
			
			int flags = Win32Const.ULW_ALPHA;
			bool res = false;
			
			res = Win32Api.UpdateLayeredWindow (handle,
				/**/							screen_dc,
				/**/							ref dst_point,
				/**/							ref new_size,
				/**/							memory_dc,
				/**/							ref src_point,
				/**/							0,
				/**/							ref blend_function,
				/**/							flags);
			
			if (res == false)
			{
				System.Diagnostics.Debug.WriteLine ("LastError = " + Marshal.GetLastWin32Error ().ToString ());
			}
			
			Win32Api.SelectObject (memory_dc, old_bitmap);
			Win32Api.ReleaseDC (System.IntPtr.Zero, screen_dc);
			Win32Api.DeleteObject (memory_bitmap);
			Win32Api.DeleteDC (memory_dc);
			
			return res;
		}
	}
}
