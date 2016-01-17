using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        const int NumGradients = 10;
        double rMin, rMax, iMin, iMax;
        int width, height, iterations;
        List<Color> randomColors;
        

        // lightweight pixel class
        class Pixel
        {
            public byte r, g, b;
            public Pixel(byte r, byte g, byte b) { this.r = r; this.g = g; this.b = b; }
            public void FromColor(Color c) { this.r = c.R; this.g = c.G; this.b = c.B; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            // complex plane viewing window
            rMin = double.Parse(tbRealMin.Text);
            rMax = double.Parse(tbRealMax.Text);
            iMin = double.Parse(tbImagineMin.Text);
            iMax = double.Parse(tbImagineMax.Text);

            width = int.Parse(tbWidth.Text);
            height = int.Parse(tbHeight.Text);
            iterations = int.Parse(tbIterations.Text);

            // generate random color set
            randomColors = GenerateRandomColorPalette(100);

            // timers
            Stopwatch timer = new Stopwatch();

            // sequential
            timer.Start();
            Pixel[,] seqMandel = GenerateMandelbrot(width, height, rMin, rMax, iMin, iMax, iterations, randomColors, runParallel: false);
            timer.Stop();
            TimeSpan elapsed1 = timer.Elapsed;
            timer.Reset();

            // parallel
            timer.Start();
            Pixel[,] parMandel = GenerateMandelbrot(width, height, rMin, rMax, iMin, iMax, iterations, randomColors, runParallel: true);
            timer.Stop();
            TimeSpan elapsed2 = timer.Elapsed;

            Bitmap bmpSeqMandel = PixelsAsBitmap(seqMandel);
            Bitmap bmpParMandel = PixelsAsBitmap(parMandel);

            ShowMandelbrot(bmpSeqMandel, String.Format("Sequential - {0:f2} ms", elapsed1.TotalMilliseconds));
            ShowMandelbrot(bmpParMandel, String.Format("Parallel - {0:f2} ms", elapsed2.TotalMilliseconds));
        }

        private void ShowMandelbrot(Bitmap bmp, string title)
        {
            Form form = new Form();
            PictureBox picture = new PictureBox();
            picture.Width = bmp.Width;
            picture.Height = bmp.Height;
            picture.Image = bmp;

            double realMin, realMax, imagMin, imagMax;
            realMin = rMin; realMax = rMax; imagMin = iMin; imagMax = iMax;


            // Zoom Capability
            // Double Click for Zoom In
            // Right-Click for Zoom Out
            picture.MouseDoubleClick += new MouseEventHandler(
                (object sender, MouseEventArgs e) =>
                {
                    // must translate the mouse (x,y) to complex (r,i)
                    double origin_r = e.X * ((realMax - realMin) / width) + realMin;
                    double origin_i = e.Y * ((imagMax - imagMin) / height) + imagMin;
                    ZoomView(0.8, origin_r, origin_i, ref realMin, ref realMax, ref imagMin, ref imagMax);
                    Pixel[,] pixMandel = GenerateMandelbrot(width, height, realMin, realMax, imagMin, imagMax, iterations,randomColors,  runParallel: true);
                    Bitmap bmpMandel = PixelsAsBitmap(pixMandel);
                    picture.Image = bmpMandel;
                });
            picture.MouseClick += new MouseEventHandler(
                (object sender, MouseEventArgs e) =>
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        // must translate the mouse (x,y) to complex (r,i)
                        double origin_r = e.X * ((realMax - realMin) / width) + realMin;
                        double origin_i = e.Y * ((imagMax - imagMin) / height) + imagMin;   
                        ZoomView(1.25, origin_r, origin_i, ref realMin, ref realMax, ref imagMin, ref imagMax);
                        Pixel[,] pixMandel = GenerateMandelbrot(width, height, realMin, realMax, imagMin, imagMax, iterations, randomColors, runParallel: true);
                        Bitmap bmpMandel = PixelsAsBitmap(pixMandel);
                        picture.Image = bmpMandel;
                    }
                });

            form.Text = title;
            form.Width = bmp.Width;
            form.Height = bmp.Height+10;
            form.Controls.Add(picture);

            Thread t = new Thread(() => form.ShowDialog());
            t.Start();
        }

        private void ZoomView(double scale, double centerX, double centerY, ref double viewXMin, ref double viewXMax, ref double viewYMin, ref double viewYMax)
        {
            // scale factor < 0 => zoom in
            // scale factor > 0 => zoom out
            double xRange = scale * ((viewXMax - viewXMin) / 2);
            double yRange = scale * ((viewYMax - viewYMin) / 2);
            viewXMin = centerX - xRange;
            viewXMax = centerX + xRange;
            viewYMin = centerY - yRange;
            viewYMax = centerY + yRange;
        }
 

        /*
         * This is an unsafe method call that locks the bitmap data
         * and uses low-level pointers to perform the updates. This
         * provides a crucial performance boost, and is generally
         * the suggested method for doing large image updates.
         */
        unsafe private Bitmap PixelsAsBitmap(Pixel[,] image)
        {
            int width = image.GetUpperBound(0)+1;
            int height = image.GetUpperBound(1)+1;

            // 24 bits per pixel Bitmap (8 bytes per color - R,G,B)
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Point to the beginning of our bitmap
            byte* origin = (byte*) bmpData.Scan0;

            // why not parallelize this too!
            Parallel.For(0, width,
            (xPixel) =>
            {
                for (int yPixel = 0; yPixel < height; yPixel++)
                {
                    // this probably looks very strange. But, think of it this way,
                    // we generally work with images that are 2-dimensional arrays
                    // where we can access a pixel by image[x,y], however, we are only
                    // using one pointer to access every byte, so we have to translate.
                    // Rows have image width * 3 bytes in them, so each row is spaced (width *3)
                    // bytes apart, therefore, to access a certain row (or y) we add
                    // (yPixel * width * 3) to the origin, and then access the column (or x)
                    // by adding (xPixel * 3).
                    byte* pixel = origin + (yPixel * width * 3) + (xPixel * 3);
                    pixel[0] = image[xPixel, yPixel].r;
                    pixel[1] = image[xPixel, yPixel].g;
                    pixel[2] = image[xPixel, yPixel].b;
                }
            });

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        private Pixel[,] GenerateMandelbrot(int width, int height, double realMin, double realMax, double imagMin, double imagMax, int iterations, List<Color> colorSet, bool runParallel = false)
        {
            // info on mandelbrot and fractals
            // https://classes.yale.edu/fractals/MandelSet/welcome.html

            Pixel[,] image = new Pixel[width, height];

            // how much we move each pixel in the complex plane
            double realScale = (realMax - realMin) / width;
            double imagScale = (imagMax - imagMin) / height;

            if (runParallel)
            {
                // Parallel Version
                Parallel.For(0, width,
                (xPixel) =>
                {
                    for (int yPixel = 0; yPixel < height; yPixel++)
                    {
                        // complex plane is similar to cartesian, 
                        // so translation just requires scaling and an offset
                        // (x, y) => (real, imag) = (realScale * x + realMin, imagScale * y + imagMin)
                        Complex c = new Complex(realScale * xPixel + realMin, imagScale * yPixel + imagMin);
                        Complex z = new Complex(0, 0);

                        // black is the default
                        // assumes point will be in the mandelbrot set
                        // iterations will determine if it is not
                        image[xPixel, yPixel] = new Pixel(0, 0, 0);
                        for (int iterIdx = 0; iterIdx < iterations; iterIdx++)
                        {
                            // the basic iteration rule
                            // more complex rules with multiple critical points
                            // can have significant affects on the results
                            // i.e. f(z) = z^3/3 + z^2/2 + c
                            z = z * z + c;

                            // does it tend to infinity?
                            // yes, it seems strange, but there is a proof
                            // for this (https://classes.yale.edu/fractals/MandelSet/JuliaSets/InfinityProof.html)
                            // Essentially, if the distance of z from the origin (magnitude), is greater than 2
                            // then the iteration will go to infinity, which means it is NOT in the mandelbrot
                            // set
                            if (z.Magnitude > 2.0)
                            {
                                double percentage = (double)iterIdx / (double)iterations;
                                Color chosen = colorSet[(int)(percentage * colorSet.Count)];
                                image[xPixel, yPixel].FromColor(chosen);
                                break;
                            }

                        }
                    }
                });
            } else
            {
                // Sequential Version
                for (int xPixel = 0; xPixel < width; xPixel++)
                {
                    for (int yPixel = 0; yPixel < height; yPixel++)
                    {
                        // complex plane is similar to cartesian, 
                        // so translation just requires scaling and an offset
                        // (x, y) => (real, imag) = (realScale * x + realMin, imagScale * y + imagMin)
                        Complex c = new Complex(realScale * xPixel + realMin, imagScale * yPixel + imagMin);
                        Complex z = new Complex(0, 0);

                        // black is the default
                        // assumes point will be in the mandelbrot set
                        // iterations will determine if it is not
                        image[xPixel, yPixel] = new Pixel(0, 0, 0);
                        for (int iterIdx = 0; iterIdx < iterations; iterIdx++)
                        {
                            // the basic iteration rule
                            // more complex rules with multiple critical points
                            // can have significant affects on the results
                            // i.e. try: f(z) = z^3/3 + z^2/2 + c
                            z = z * z + c;

                            // does it tend to infinity?
                            // yes, it seems strange, but there is a proof
                            // for this (https://classes.yale.edu/fractals/MandelSet/JuliaSets/InfinityProof.html)
                            // Essentially, if the distance of z from the origin (magnitude), is greater than 2
                            // then the iteration will go to infinity, which means it is NOT in the mandelbrot
                            // set
                            if (z.Magnitude > 2.0)
                            {
                                // base the color of the pixel on the number of iterations
                                // it made it to
                                double percentage = (double)iterIdx / (double)iterations;
                                Color chosen = colorSet[(int)(percentage * colorSet.Count)];
                                image[xPixel, yPixel].FromColor(chosen);
                                break;
                            }

                        }
                    }
                }
            }

            return image;
        }

        private List<Color> GenerateRandomColorPalette(int numColors)
        {
            List<Color> colorPalette = new List<Color>();
            
            // Get all non-system colors known by name (i.e. Color.Azure)
            KnownColor[] allKnownColors = (KnownColor[]) Enum.GetValues(typeof(KnownColor));
            List<Color> normalColors = new List<Color>();
            foreach (KnownColor currColor in allKnownColors)
            {
                Color color = Color.FromKnownColor(currColor);
                if (!color.IsSystemColor)
                    normalColors.Add(color);
            }
            normalColors.Remove(Color.Transparent); //obviously don't want that as a color

            // Set random gradients
            Color[] randGradients = new Color[NumGradients];
            Random rand = new Random();
            for (int index = 0; index < NumGradients; index++)
                randGradients[index] = normalColors[rand.Next(0, normalColors.Count)];

            // my randomness algorith
            // can be replaced by anything, static values would work too
            // this works by selecting random colors from the
            // normal colors set, and making a gradient across
            // the color palette for each color
            int gradientSpread = numColors / NumGradients;
            int gradientIdx = 0;
            for (int colorIdx = 0; colorIdx < numColors; colorIdx++)
            {
                gradientIdx = (colorIdx > (gradientSpread * (gradientIdx+1))) ? gradientIdx+1 : gradientIdx;
                colorPalette.Add(Color.FromArgb(
                    (randGradients[gradientIdx].R + colorIdx) % 255,
                    (randGradients[gradientIdx].G + colorIdx) % 255,
                    (randGradients[gradientIdx].B + colorIdx) % 255));
            }

            return colorPalette;
        }

    }
}
