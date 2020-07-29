using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using SunBlade;

namespace Adler {

	[StructLayout(LayoutKind.Explicit, Size = 4)]
	internal struct Pixel {
		[FieldOffset(0)] internal int argb;
		[FieldOffset(3)] internal byte A;
		[FieldOffset(2)] internal byte R;
		[FieldOffset(1)] internal byte G;
		[FieldOffset(0)] internal byte B;
		//public static explicit operator Pixel(int pInt) => new Pixel(pInt);
		internal Pixel(uint pInt) : this() => argb = (int)pInt;
		internal Pixel(int pInt) : this() => argb = pInt;
		internal byte Saturation() => (byte)(Math.Max(Math.Max(R, B), G) - Math.Min(Math.Min(R, B), G));
	}

	internal class Picture {

		internal Size Size;
		internal Pixel[] Pixels;

        #region Constructor

		/// <summary>Erstellt ein leeres Picture mit width * height</summary>
        internal Picture(int width, int height) => Empty(width, height);

		internal Picture(Size size) => Empty(size);

		internal Picture(Bitmap image) => Load(image);

		internal Picture(string file) => File(file);

        #endregion

        #region Empty

        internal void Empty(Size size) {
			Size = size;
			Pixels = new Pixel[(long)Size.Width * Size.Height];
		}
		
		internal void Empty(int width, int height) => Empty(new Size(width, height));

		#endregion

		#region Load

		internal void Load(Bitmap image) {
			Empty(image.Width, image.Height);
			int pixelCount = Size.Width * Size.Height;

			IntPtr intsPointer = image.LockBits(new Rectangle(0, 0, Size.Width, Size.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb).Scan0;
			GCHandle pixelsPointer = GCHandle.Alloc( Pixels, GCHandleType.Pinned );
			try {
				WinApi.CopyMemory(pixelsPointer.AddrOfPinnedObject(), intsPointer, (IntPtr)(pixelCount*4));
			} finally {
				pixelsPointer.Free();
            }
		}

        #endregion

        #region File

        internal void File(string filepath) {
			//@TODO wenn File not exists! return fehler
			using (FileStream stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (Bitmap image = new Bitmap(stream)) {
                    Load(image);
				}
			}
		}

        #endregion


		#region Save

		internal void Save(string file, ImageFormat format) {
			GCHandle ptr = GCHandle.Alloc(Pixels, GCHandleType.Pinned);
			using (Bitmap bitmap = new Bitmap(Size.Width, Size.Height, Size.Width * 4, PixelFormat.Format32bppArgb, ptr.AddrOfPinnedObject())) {
				bitmap.Save(file, format);
			}
			ptr.Free();
		}

        #endregion

        #region Tools

        //internal Pixel GetPixel(int x, int y) => new Pixel(Pixels[(y * Size.Width) + x]);

        internal Pixel GetPixel(int x, int y) => Pixels[(y * Size.Width) + x];

		internal Point IndexToPoint(int index) => IndexToPoint(index, Size.Width);

		internal static Point IndexToPoint(int index, int width) {
			int y = (int)(index / width);
			int x = (int)(index % width);
			return new Point(x, y);
		}

		#endregion
	}
}
