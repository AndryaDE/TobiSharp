using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Adler {
	internal class Haystack : Picture {

		#region constructor

		internal Haystack(Bitmap bitmap) : base(bitmap) { }

		internal Haystack(string file) : base(file) { }

        #endregion

        #region methods

        internal bool Contains(Needle needle) => Locate(needle).X >= 0;

		internal Point Locate(Needle needle) {
			// Points 0...			// RearPoints 0...			// Sizes 1...

			int firstRow = needle.StrongestPoint.Y * Size.Width;
			int firstRowFirstPixel = firstRow + needle.StrongestPoint.X;

			int lastRearRow = needle.StrongestRearPoint.Y * Size.Width;
			int lastRow = Pixels.Length - lastRearRow;
			int lastRowLastPixel = lastRow - needle.StrongestRearPoint.X;

			Point result = new Point(-1, -1);

			Parallel.For(firstRowFirstPixel, lastRowLastPixel, (index, state) => {

				if(MatchPixel(needle.StrongestPixel, Pixels[index]) == false) {
					return;
                }

				Point point = IndexToPoint( index );
				point.Offset(needle.StrongestPoint.X * -1, needle.StrongestPoint.Y * -1);

				if (MatchLocation(needle, point) == false) {
					return;
                }

				state.Stop();
				result = point;

			});

			return result;
        }

		#endregion

		#region Pixel

		internal static bool MatchPixel(Pixel needle, Pixel haystack) => MatchPixel(needle, haystack, (byte)(-1 ^ needle.A)); // (255 - Alpha)

		internal static bool MatchPixel(Pixel a, Pixel b, byte diff) => (
				(Math.Abs(a.R - b.R) <= diff) &&
				(Math.Abs(a.G - b.G) <= diff) &&
				(Math.Abs(a.B - b.B) <= diff)
		);

		#endregion

		#region picture

		internal bool MatchLocation(Needle needle, Point startPoint) {

			bool result = true;

			Parallel.ForEach(needle.Pixels, (pixel, state, index) => {

				int x = startPoint.X + (int)(index % needle.Size.Width);
				int y = startPoint.Y + (int)(index / needle.Size.Width);

				if (MatchPixel(pixel, GetPixel(x, y))) {
					return;
                }

				state.Stop();
				result = false;

			});

			return result;
        }

		#endregion

		}
	}
}
