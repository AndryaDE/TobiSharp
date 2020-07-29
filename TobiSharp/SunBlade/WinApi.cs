using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SunBlade {
#pragma warning disable IDE1006 // Naming Styles
	public static class WinApi {
		[DllImport( "winmm.dll" , EntryPoint = "timeGetTime" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern uint timeGetTime();
		
		[DllImport( "kernel32.dll" , EntryPoint = "CopyMemory" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern void CopyMemory( IntPtr destination , IntPtr source , IntPtr length );



		#region INPUT_HOOKS


		public delegate IntPtr LowLevelHookProc( int nCode , IntPtr wParam , IntPtr lParam );


		[DllImport( "user32.dll" , EntryPoint = "SetWindowsHookEx" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr SetWindowsHookEx( int idHook , LowLevelHookProc lpfn , IntPtr hMod , uint dwThreadId );


		[DllImport( "user32.dll" , EntryPoint = "UnhookWindowsHookEx" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool UnhookWindowsHookEx( IntPtr hhk );


		[DllImport( "user32.dll" , EntryPoint = "CallNextHookEx" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr CallNextHookEx( IntPtr hhk , int nCode , IntPtr wParam , IntPtr lParam );


		#endregion INPUT_HOOKS



		#region INPUT_GENERATOR
		public enum InputType : uint {
			Mouse = 0,
			Keyboard = 1,
			Hardware = 2,
		}
		public enum MouseFlag : uint {
			Absolute = 0x8000,
			HWheel = 0x1000,
			Move = 0x1,
			Move_NoCoalesce = 0x2000,
			LeftDown = 0x2,
			LeftUp = 0x4,
			RightDown = 0x8,
			RightUp = 0x10,
			MiddleDown = 0x20,
			MiddleUp = 0x40,
			VirtualDesk = 0x4000,
			Wheel = 0x800,
			XDown = 0x80,
			XUp = 0x100,
		}
		public enum KeyboardFlag : uint {
			ExtendedKey = 0x1,
			KeyDown = 0,
			KeyUp = 0x2,
			ScanCode = 0x8,
			Unicode = 0x4,
		}
		[StructLayout( LayoutKind.Explicit , CharSet = CharSet.Unicode )]
		public struct INPUT {
			[FieldOffset( 0 )] public InputType type;
			[FieldOffset( 4 )] public MOUSE_INPUT m;
			[FieldOffset( 4 )] public KEYBOARD_INPUT k;
			[FieldOffset( 4 )] public HARDWARE_INPUT h;
		}
		[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Unicode )]
		public struct MOUSE_INPUT {
			public int dx;
			public int dy;
			public int mouseData;
			public MouseFlag dwFlags;
			public uint time;
			public UIntPtr dwExtraInfo;
		}
		[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Unicode )]
		public struct KEYBOARD_INPUT {
			public uint wVk;
			public uint wScan;
			public KeyboardFlag dwFlags;
			public uint time;
			public UIntPtr dwExtraInfo;
		}
		[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Unicode )]
		public struct HARDWARE_INPUT {
			public uint uMsg;
			public ushort wParamL;
			public ushort wParamH;
		}


		[DllImport( "user32.dll" , EntryPoint = "SendInput" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		public static extern uint SendInput( int numberOfInputs , INPUT[] inputs , int sizeOfInputStructure );


		[DllImport( "user32.dll" , EntryPoint = "mouse_event" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		public static extern void MouseEvent( MouseFlag dwFlags , int dx , int dy , int dwData , UIntPtr dwExtraInfo );


		[DllImport( "user32.dll" , EntryPoint = "keybd_event" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		public static extern void KeybdEvent( byte bVk , byte bScan , KeyboardFlag dwFlags , UIntPtr dwExtraInfo );


		[DllImport( "user32.dll" , EntryPoint = "VkKeyScan" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		public static extern ushort VkKeyScan( char ch );


		[DllImport( "user32.dll" , EntryPoint = "MapVirtualKey" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		public static extern ushort MapVirtualKey( int code , uint uMapType );


		#endregion INPUT_GENERATOR



		#region CONSOLE


		[DllImport( "kernel32.dll" , EntryPoint = "GetConsoleWindow" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GetConsoleWindow();


		[DllImport( "kernel32.dll" , EntryPoint = "AttachConsole" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool AttachConsole( int input );


		[DllImport( "kernel32.dll" , EntryPoint = "FreeConsole" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool FreeConsole();


		[DllImport( "kernel32.dll" , EntryPoint = "GetStdHandle" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GetStdHandle( int nStdHandle );


		[DllImport( "kernel32.dll" , EntryPoint = "AllocConsole" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool AllocConsole();


		#endregion CONSOLE



		#region WINDOWS_API
		[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Unicode , Pack = 4 )]
		public struct POINT {
			public int x;
			public int y;
			public static explicit operator Point( POINT pRect ) => new Point( pRect.x , pRect.y );
			public static explicit operator Size( POINT pRect ) => new Size( pRect.x , pRect.y );
		}
		[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Unicode , Pack = 4 )]
		public struct RECT {
			public int left;
			public int top;
			public int right;
			public int bottom;
			public static explicit operator Rectangle( RECT pRect ) => new Rectangle( pRect.left , pRect.top , pRect.right - pRect.left , pRect.bottom - pRect.top );
		}



		[DllImport( "user32.dll" , EntryPoint = "FindWindow" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr FindWindow( string lpClassName , string lpWindowName );


		[DllImport( "user32.dll" , EntryPoint = "GetDesktopWindow" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GetDesktopWindow();


		[DllImport( "user32.dll" , EntryPoint = "GetForegroundWindow" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GetForegroundWindow();


		[DllImport( "user32.dll" , EntryPoint = "IsWindow" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool IsWindow( IntPtr hWnd );


		[DllImport( "user32.dll" , EntryPoint = "GetWindowTextLength" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern int GetWindowTextLength( IntPtr hwnd );


		[DllImport( "user32.dll" , EntryPoint = "GetWindowText" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern int GetWindowText( IntPtr hWnd , StringBuilder lpString , int nMaxCount );


		[DllImport( "user32.dll" , EntryPoint = "GetClassName" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern int GetClassName( IntPtr hwnd , StringBuilder pszType , int cchType );


		[DllImport( "user32.dll" , EntryPoint = "RealGetWindowClass" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern int RealGetWindowClass( IntPtr hwnd , StringBuilder pszType , int cchType );


		[DllImport( "user32.dll" , EntryPoint = "GetWindowRect" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool GetWindowRect( IntPtr hWnd , ref RECT lpRect );


		[DllImport( "user32.dll" , EntryPoint = "GetClientRect" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool GetClientRect( IntPtr hWnd , ref RECT lpRect );


		[DllImport( "user32.dll" , EntryPoint = "ClientToScreen" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool ClientToScreen( IntPtr hWnd , ref POINT lpPoint );
		[DllImport( "user32.dll" , EntryPoint = "ClientToScreen" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool ClientToScreen( IntPtr hWnd , ref RECT lpPoint );


		[DllImport( "user32.dll" , EntryPoint = "ScreenToClient" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool ScreenToClient( IntPtr hWnd , ref POINT lpPoint );
		[DllImport( "user32.dll" , EntryPoint = "ScreenToClient" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool ScreenToClient( IntPtr hWnd , ref RECT lpPoint );


		public enum WindowMessage : uint {
			WM_KEYDOWN = 0x0100,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-keydown
			WM_KEYUP = 0x0101,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-keyup
			WM_CHAR = 0x0102,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-char
			WM_SYSKEYDOWN = 0x0104, // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-syskeydown
			WM_SYSKEYUP = 0x0105, // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-syskeyup
			WM_LBUTTONDOWN = 0x0201,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-lbuttondown
			WM_LBUTTONUP = 0x0202,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-lbuttonup
			WM_RBUTTONDOWN = 0x0204,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-rbuttondown
			WM_RBUTTONUP = 0x0205,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-rbuttonup
			WM_MBUTTONDOWN = 0x0207,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-mbuttondown
			WM_MBUTTONUP = 0x0208,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-mbuttonup
			WM_MOUSEWHEEL = 0x020A, // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-mousewheel
			WM_XBUTTONDOWN = 0x020B,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-xbuttondown
			WM_XBUTTONUP = 0x020C,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-xbuttonup
			WM_MOUSEHWHEEL = 0x020E,  // https://docs.microsoft.com/en-gb/windows/desktop/inputdev/wm-mousehwheel
		}
		[DllImport( "user32.dll" , EntryPoint = "SendMessage" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool SendMessage( IntPtr hWnd , WindowMessage Msg , IntPtr wParam , IntPtr lParam );

		[DllImport( "user32.dll" , EntryPoint = "PostMessage" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool PostMessage( IntPtr hWnd , WindowMessage Msg , IntPtr wParam , IntPtr lParam );


		#endregion WINDOWS_API



		#region FileSystem

		[StructLayout( LayoutKind.Sequential , Pack = 4 )]
		internal struct FILE_ATTRIBUTE_DATA {
			internal uint dwFileAttributes;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
			internal uint nFileSizeHigh;
			internal uint nFileSizeLow;
		}

		[DllImport( "kernel32.dll" , EntryPoint = "GetFileAttributesExW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool GetFileAttributesEx( string path , int level , out FILE_ATTRIBUTE_DATA data );

		[DllImport( "kernel32.dll" , EntryPoint = "SetFileAttributesW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool SetFileAttributes( string lpFileName , uint dwFileAttributes );


		[StructLayout( LayoutKind.Sequential , Pack = 4 , CharSet = CharSet.Unicode )]
		internal struct FIND_STREAM {
			internal ulong nStreamSize;
			[MarshalAs( UnmanagedType.ByValTStr , SizeConst = 296 )]
			internal string sStreamName;
		}
		[DllImport( "kernel32.dll" , EntryPoint = "FindFirstStreamW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr FindFirstStream( string lpFileName , int InfoLevel , out FIND_STREAM lpFindStreamData , int dwFlags );

		[DllImport( "kernel32.dll" , EntryPoint = "FindNextStreamW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool FindNextStream( IntPtr hFindStream , out FIND_STREAM lpFindStreamData );


		[StructLayout( LayoutKind.Sequential , Pack = 4 , CharSet = CharSet.Unicode )]
		internal struct FIND_DATA {
			internal uint nFileAttributes;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
			internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
			internal uint nFileSizeHigh;
			internal uint nFileSizeLow;
			internal uint nReparseTag;
			internal uint dwReserved1;
			[MarshalAs( UnmanagedType.ByValTStr , SizeConst = 260 )]
			internal string sFileName;
			[MarshalAs( UnmanagedType.ByValTStr , SizeConst = 14 )]
			internal string sShortFileName;
			internal uint dwFileType;
			internal uint dwCreatorType;
			internal ushort wFinderFlags;
		}
		[DllImport( "kernel32.dll" , EntryPoint = "FindFirstFileW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr FindFirstFile( string lpFileName , out FIND_DATA lpFindFileData );

		[DllImport( "kernel32.dll" , EntryPoint = "FindNextFileW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool FindNextFile( IntPtr hFindFile , out FIND_DATA lpFindFileData );

		[DllImport( "kernel32.dll" , EntryPoint = "FindClose" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool FindClose( IntPtr hFindFile );



		[DllImport( "kernel32.dll" , EntryPoint = "RemoveDirectoryW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool RemoveDirectory( string path );

		[DllImport( "kernel32.dll" , EntryPoint = "DeleteFileW" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool DeleteFile( string path );

		#endregion FileSystem



		[DllImport( "winmm.dll" , EntryPoint = "timeBeginPeriod" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern uint timeBeginPeriod( uint uPeriod );

		[DllImport( "winmm.dll" , EntryPoint = "timeEndPeriod" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern uint timeEndPeriod( uint uPeriod );


		[DllImport( "user32.dll" , EntryPoint = "GetSystemMetrics" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern int GetSystemMetrics( int index );


		#region Clipboard
		[DllImport( "user32.dll" , EntryPoint = "GetClipboardData" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GetClipboardData( uint uFormat );

		[DllImport( "user32.dll" , EntryPoint = "EmptyClipboard" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool EmptyClipboard();

		[DllImport( "user32.dll" , EntryPoint = "IsClipboardFormatAvailable" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool IsClipboardFormatAvailable( uint format );

		[DllImport( "user32.dll" , EntryPoint = "OpenClipboard" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool OpenClipboard( IntPtr hWndNewOwner );

		[DllImport( "user32.dll" , EntryPoint = "CloseClipboard" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool CloseClipboard();

		[DllImport( "kernel32.dll" , EntryPoint = "GlobalLock" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		internal static extern IntPtr GlobalLock( IntPtr hMem );

		[DllImport( "kernel32.dll" , EntryPoint = "GlobalUnlock" , CharSet = CharSet.Unicode , SetLastError = true , CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool GlobalUnlock( IntPtr hMem );

		#endregion

#pragma warning restore IDE1006 // Naming Styles
	}
}