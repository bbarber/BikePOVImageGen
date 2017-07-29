﻿using System;
using ImageSharp;
using System.IO;

namespace BikePOVImageGen
{
    class Program
    {
        // Number samples around the image
        int RESOLUTION = 360;

        static void Main(string[] args)
        {
            // This will eventually be passed in
            var filename = "foo.gif";


            var fileStream = new FileStream(filename, FileMode.Open);
            var image = Image.Load(fileStream); 

            var imageWidth = image.Width;
            var imageHeight = image.Height;

            var centerPt = new Point(imageWidth / 2, imageHeight / 2);


            var centerPx = image.GetPixelReference(centerPt.X, centerPt.Y);

            Console.WriteLine(centerPx);
            Console.WriteLine("Done.");
        }
    }
}
