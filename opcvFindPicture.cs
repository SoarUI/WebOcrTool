using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;

namespace desktopGraphic2DTool
{
    public class opcvFindPicture
    {
       
        public static Mat BitMap2Mat(Bitmap bmp)
        {
            Mat tmpmat = new Mat(bmp.Height, bmp.Width, MatType.CV_8UC4);
            BitmapData bd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int srcStep = bd.Stride;
            long dstStep = tmpmat.Step();
            int bufSize = bmp.Height * bd.Stride;
            unsafe
            {
                byte* sp = (byte*)bd.Scan0;
                byte* dp = (byte*)tmpmat.Data;
                for (int y = 0; y < bmp.Height; y++)
                {

                    Buffer.MemoryCopy(sp, dp, dstStep, dstStep);
                    dp += dstStep;
                    sp += srcStep;

                }
            }
            bmp.UnlockBits(bd);
            bmp.Dispose();
            return tmpmat;
        }
        public static Bitmap Mat2Bitmap(Mat mat)
        {
           
            int depth = mat.Depth();
            int channels = mat.Channels();
            int width = mat.Cols;
            int height = mat.Rows;
            int lineSize = ((mat.Width * channels) + 3) / 4 * 4;
            
            Bitmap temp = null;
            BitmapData bd = null; 
            switch(channels)
            {
                case 4:
                    {
                        temp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        bd = temp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        break;
                    }
                case 1:
                    {
                        temp= new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        ColorPalette plt = temp.Palette;
                        for (int x = 0; x < 256; x++)
                        {
                            plt.Entries[x] = Color.FromArgb(x, x, x);
                        }
                        temp.Palette = plt;
                        bd = temp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        break;
                    }
                case 3:
                    {
                        temp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        bd = temp.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        break;
                    }
            }
            int dstStep = bd.Stride;
            long srcStep = mat.Step();
            int bufSize = height * bd.Stride;
            unsafe
            {
                byte* dp = (byte*)bd.Scan0;
                byte* sp = (byte*)mat.Data;
                for (int y = 0; y < height; y++)
                {

                    Buffer.MemoryCopy(sp, dp, srcStep, srcStep);
                    dp += dstStep;
                    sp += srcStep;

                }
            }
            temp.UnlockBits(bd);
            return temp;
        }
    }
}
