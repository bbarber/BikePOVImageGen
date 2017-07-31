﻿using System;
using ImageSharp;
using ImageSharp.Processing;
using ImageSharp.PixelFormats;
using System.IO;
using System.Threading;
using static System.Console;

namespace BikePOVImageGen
{
    class Program
    {
        // Number of lines around the image
        static int RESOLUTION = 360;

        // How many LEDs each strip has
        static int NUMBER_OF_LEDS = 12;

        // How far should we start from the center of the image
        static double LED_CENTER_OFFSET_PERCENT = 0.4;

        static void Main(string[] args)
        {
            // This will eventually be passed in
            var filename = "images/nb.png";


            var fileStream = new FileStream(filename, FileMode.Open);
            var image = Image.Load(fileStream);
            

            var imageWidth = image.Width;
            var imageHeight = image.Height;

            // Pad the edges with black to make it square
            var size = imageWidth < imageHeight ? imageHeight : imageWidth;
            image.Pad(size, size);

            // Used to visualize the output
            var output = new Image<Rgba32>(size, size);

            var startingLength = size / 2 * LED_CENTER_OFFSET_PERCENT / 100;
            var endingLength = size / 2;
            var lengthBetweenLEDs = (endingLength - startingLength) / NUMBER_OF_LEDS;

            for (var degree = 0; degree < 360; degree += 360 / RESOLUTION)
            {
                for (var led = 1; led <= NUMBER_OF_LEDS; led++)
                {
                    var length = led * lengthBetweenLEDs + startingLength;
                    var x = length * Math.Cos(degree);
                    var y = length * Math.Sin(degree);

                    var pixelX = Math.Round(image.Width / 2.0 + x);
                    var pixelY = Math.Round(image.Height / 2.0 + y);

                    output[(int)pixelX, (int)pixelY] = image[(int)pixelX, (int)pixelY];
                }
            }

            output.Save("output.gif");

            WriteLine("Done.");
        }
    }
}
