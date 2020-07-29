using System;
using System.Text;

namespace SunBlade {
	public class Wnd {
		private IntPtr _Wnd;

		/// <summary>
		/// Create new Wnd class.
		/// </summary>
		/// <param name="pHandle">window handle to use.</param>
		public Wnd( IntPtr pHandle ) => _Wnd = pHandle;

		public static explicit operator Wnd( IntPtr pWnd ) => new Wnd( pWnd );
		public static explicit operator IntPtr( Wnd pWnd ) => pWnd._Wnd;
		public static explicit operator long( Wnd pWnd ) => pWnd._Wnd.ToInt64();
		public static explicit operator int( Wnd pWnd ) => pWnd._Wnd.ToInt32();

		public override bool Equals( object pObj ) => ( pObj is IntPtr p && _Wnd == p ) || ( pObj is Wnd w && _Wnd == w._Wnd );
		public override int GetHashCode() => _Wnd.GetHashCode();

		public static bool operator ==( Wnd pObj1 , IntPtr pObj2 ) => pObj1._Wnd == pObj2;
		public static bool operator ==( Wnd pObj1 , Wnd pObj2 ) => pObj1._Wnd == pObj2._Wnd;
		public static bool operator ==( IntPtr pObj1 , Wnd pObj2 ) => pObj1 == pObj2._Wnd;
		public static bool operator !=( Wnd pObj1 , IntPtr pObj2 ) => pObj1._Wnd != pObj2;
		public static bool operator !=( Wnd pObj1 , Wnd pObj2 ) => pObj1._Wnd != pObj2._Wnd;
		public static bool operator !=( IntPtr pObj1 , Wnd pObj2 ) => pObj1 != pObj2._Wnd;



		public override string ToString() => _Wnd.ToInt64().ToString( "X" );
		public string ToString( string pFormat ) => _Wnd.ToInt64().ToString( pFormat );



		/// <summary>
		/// check if handle is valid and a window
		/// </summary>
		/// <param name="pWnd">handle to check.</param>
		//public static bool IsValid( IntPtr pWnd ) => pWnd.IsValid() && WinApi.IsWindow( pWnd ); //@NOTE klappt noch nicht so ganz wie gewollt, hab's auskommentiert
		public static bool IsValid(IntPtr pWnd) => WinApi.IsWindow(pWnd);
		/// <summary>
		/// check if opbject is valid and a window
		/// </summary>
		public bool IsValid() => IsValid( _Wnd );



		/// <summary>
		/// check if handle is a window
		/// </summary>
		/// <param name="pWnd">handle to check.</param>
		public static bool IsWindow( IntPtr pWnd ) => WinApi.IsWindow( pWnd );
		/// <summary>
		/// check if opbject is a window
		/// </summary>
		public bool IsWindow() => IsWindow( _Wnd );



		/// <summary>
		/// get handle to currently active window
		/// </summary>
		public static IntPtr GetForegroundWindow() => WinApi.GetForegroundWindow();



		/// <summary>
		/// retrieve Window Title
		/// </summary>
		/// <param name="pWnd">handle to Window.</param>
		public static string GetWindowTitle( IntPtr pWnd ) {
			int len = WinApi.GetWindowTextLength( pWnd );
			if ( len < 1 ) return null;
			StringBuilder r = new StringBuilder( "" , len + 1 );
			len = WinApi.GetWindowText( pWnd , r , len + 1 );
			if ( len < 1 ) return null;
			return r.ToString();
		}
		/// <summary>
		/// retrieve Window Title
		/// </summary>
		public string GetWindowTitle() => GetWindowTitle( _Wnd );


		/// <summary>
		/// retrieve Window Class
		/// </summary>
		/// <param name="pWnd">handle to Window.</param>
		public static string GetClassName( IntPtr pWnd ) {
			StringBuilder r = new StringBuilder( "" , 1000 );
			if ( WinApi.GetClassName( pWnd , r , 1000 ) < 1 ) return null;
			return r.ToString();
		}
		/// <summary>
		/// retrieve Window Class
		/// </summary>
		public string GetClassName() => GetClassName( _Wnd );


		/// <summary>
		/// retrieve Window Class
		/// </summary>
		/// <param name="pWnd">handle to Window.</param>
		public static string GetClassName2( IntPtr pWnd ) {
			StringBuilder r = new StringBuilder( "" , 1000 );
			if ( WinApi.RealGetWindowClass( pWnd , r , 1000 ) < 1 ) return null;
			return r.ToString();
		}
		/// <summary>
		/// retrieve Window Class
		/// </summary>
		public string GetClassName2() => GetClassName2( _Wnd );



		/// <summary>
		/// retrieve Full Window Rect in Screen Coordinates
		/// </summary>
		/// <param name="pWnd">handle to Window.</param>
		public static WinApi.RECT GetWindowRect( IntPtr pWnd ) {
			WinApi.RECT r = new WinApi.RECT();
			GetWindowRect( pWnd , ref r );
			return r;
		}
		/// <summary>
		/// retrieve Full Window Rect in Screen Coordinates
		/// </summary>
		/// <returns>
		/// true if the call was successful.
		/// </returns>
		/// <param name="pWnd">handle to Window.</param>
		/// <param name="pRect">reference to the variable to fill.</param>
		public static bool GetWindowRect( IntPtr pWnd , ref WinApi.RECT pRect ) => WinApi.GetWindowRect( pWnd , ref pRect );
		/// <summary>
		/// retrieve Full Window Rect in Screen Coordinates
		/// </summary>
		public WinApi.RECT GetWindowRect() => GetWindowRect( _Wnd );
		/// <summary>
		/// retrieve Full Window Rect in Screen Coordinates
		/// </summary>
		/// <returns>
		/// true if the call was successful.
		/// </returns>
		/// <param name="pRect">reference to the variable to fill.</param>
		public bool GetWindowRect( ref WinApi.RECT pRect ) => GetWindowRect( _Wnd , ref pRect );



		/// <summary>
		/// retrieve the Windows Client Rect in Screen Coordinates
		/// </summary>
		/// <param name="pWnd">handle to Window.</param>
		public static WinApi.RECT GetClientRect( IntPtr pWnd ) {
			WinApi.RECT r = new WinApi.RECT();
			GetClientRect( pWnd , ref r );
			return r;
		}
		/// <summary>
		/// retrieve the Windows Client Rect in Screen Coordinates
		/// </summary>
		/// <returns>
		/// true if the call was successful.
		/// </returns>
		/// <param name="pWnd">handle to Window.</param>
		/// <param name="pRect">reference to the variable to fill.</param>
		public static bool GetClientRect( IntPtr pWnd , ref WinApi.RECT pRect ) {
			if ( !WinApi.GetClientRect( pWnd , ref pRect ) ) return false;
			if ( !WinApi.ClientToScreen( pWnd , ref pRect ) ) return false;
			pRect.right += pRect.left;
			pRect.bottom += pRect.top;
			return true;
		}
		/// <summary>
		/// retrieve the Windows Client Rect in Screen Coordinates
		/// </summary>
		public WinApi.RECT GetClientRect() => GetClientRect( _Wnd );
		/// <summary>
		/// retrieve the Windows Client Rect in Screen Coordinates
		/// </summary>
		/// <returns>
		/// true if the call was successful.
		/// </returns>
		/// <param name="pRect">reference to the variable to fill.</param>
		public bool GetClientRect( ref WinApi.RECT pRect ) => GetClientRect( _Wnd , ref pRect );
	}
}