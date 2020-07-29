using SunBlade;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Adler {
	internal class Needle : Picture {

		//internal Size Size;
		//internal int[] Pixel;
		internal Pixel StrongestPixel;
		internal Point StrongestPoint;
		internal Point StrongestRearPoint;

		#region constructor

		internal Needle(Bitmap bitmap) : base(bitmap) => SetStrongestPixel();

		internal Needle(string file) : base(file) => SetStrongestPixel();

		#endregion

		internal void SetStrongestPixel() {
			object writeLock = new object();
			Pixel bestPixel = new Pixel();
			Point bestPoint = new Point();
			byte bestAlpha = 0;
			byte bestSaturation = 0;

			Parallel.ForEach(Pixels, (pixel, state, index) => {
				byte alpha = pixel.A;
				byte saturation = pixel.Saturation();

				if (alpha < bestAlpha || (alpha == bestAlpha && saturation < bestSaturation)) {
					return;
				}
				lock (writeLock) {
					if (alpha > bestAlpha || (alpha == bestAlpha && saturation > bestSaturation)) {
						bestPixel = pixel;
						bestAlpha = alpha;
						bestSaturation = saturation;
						bestPoint.X = (int)(index % Size.Width);
						bestPoint.Y = (int)(index / Size.Width);
					}
				}
			});

			//@TODO Strongest und Rarest Pixel wäre besser

			StrongestPixel = bestPixel;
			StrongestPoint = bestPoint;
			StrongestRearPoint = new Point( Size.Width - bestPoint.X - 1, Size.Height - bestPoint.Y - 1);
		}

	}
}
