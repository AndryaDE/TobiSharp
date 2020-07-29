using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoItX3Lib;
using AutoIt;
using Adler;
using System.Drawing.Imaging;
using System.Drawing;

/*
assembly.dll und 64x in die system32 rein und die assembly registrieren
 */

namespace TobiSharp
{
    class Program
    {
        //static AutoItX3 _autoit = new AutoItX3();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            //Needle needle = new Needle("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\02needleLeftTopRed.png");
            //Needle needle = new Needle("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\02needleRightBottomYellow.png");
            //Needle needle = new Needle("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\02needleOneOne.png");
            //Needle needle = new Needle("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\02needleFalse.png");
            Needle needle = new Needle("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\04needle40kF.png");

            string cout = "Needle:";
            cout += " a" + needle.StrongestPixel.A;
            cout += " r" + needle.StrongestPixel.R;
            cout += " g" + needle.StrongestPixel.G;
            cout += " b" + needle.StrongestPixel.B;
            cout += " x" + needle.StrongestPoint.X;
            cout += " y" + needle.StrongestPoint.Y;
            cout += " x" + needle.StrongestRearPoint.X;
            cout += " y" + needle.StrongestRearPoint.Y;
            cout += " w" + needle.Size.Width;
            cout += " h" + needle.Size.Height;
            cout += " l" + needle.Pixels.Length;
            Console.WriteLine(cout);

            Haystack haystack = new Haystack("C:\\#GitHub\\TobiSharp\\TobiSharp\\_TestBilder\\04haystackPexelF.png");
            Point test = haystack.Locate(needle);
            Console.WriteLine("x" + test.X + " y" + test.Y);

            Console.ReadLine();
            //_autoit.WinActivate("Untitled - Notepad2");
            //_autoit.Send("Hello World");

        }
    }
}
