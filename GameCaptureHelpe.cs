using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using static WinAPI.unSafeWindowHandleService;

namespace WinAPI
{
    public class GameCaptureHelper
    {
        
       
        public static Bitmap GetShotCutImage(IntPtr hWnd)
        {
            var hscrdc = GetWindowDC(hWnd);
            RECT windowRect = new RECT();
            GetWindowRect(hWnd, ref windowRect);
            int width = Math.Abs(windowRect.Width - windowRect.X);
            int height = Math.Abs(windowRect.Height - windowRect.Y);
            /* var hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
             var hmemdc = CreateCompatibleDC(hscrdc);
             SelectObject(hmemdc, hbitmap);
             PrintWindow(hWnd, hmemdc, 0);
             var bmp = Image.FromHbitmap(hbitmap);
             DeleteDC(hscrdc);
             DeleteDC(hmemdc);*/
            /* Bitmap QQPic = new Bitmap(windowRect.Width, windowRect.Height);
             Graphics g1 = Graphics.FromImage(QQPic);
             IntPtr hdc1 = GetDC(hWnd);
             IntPtr hdc2 = g1.GetHdc();  //得到Bitmap的DC
             BitBlt(hdc2, 0, 0, windowRect.Width, windowRect.Height, hdc1, 0, 0, 13369376);
             g1.ReleaseHdc(hdc2);  //释放掉Bitmap的DC
             QQPic.Save("c:\\QQpic.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
             return QQPic;*/
            Bitmap bmp = new Bitmap(width,
                height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(windowRect.X, windowRect.Y, 0, 0, new Size(width,height));
            g.Dispose();

            return bmp;
        }
        public static Bitmap GetScreenImageFront(IntPtr hWnd,string path)
        {
           
            RECT windowRect = new RECT();
            GetWindowRect(hWnd, ref windowRect);
            int width = windowRect.Width ;
            int height = windowRect.Height;
            Bitmap bmp = new Bitmap(width,
                height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(windowRect.X, windowRect.Y, 0, 0, new Size(width, height));
            g.Dispose();
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                } catch
                {
                    bmp.Dispose();
                    return null;
                }
                
            }
            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
            return null;
        }
        public static Bitmap GetShotCutImageBack(IntPtr hWnd)
        {
            var hscrdc = GetDC(hWnd);
            RECT windowRect = new RECT();
            GetWindowRect(hWnd, ref windowRect);
            int width = Math.Abs(windowRect.Width - windowRect.X);
            int height = Math.Abs(windowRect.Height - windowRect.Y);
            /* var hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
             var hmemdc = CreateCompatibleDC(hscrdc);
             SelectObject(hmemdc, hbitmap);
             PrintWindow(hWnd, hmemdc, 0);
             var bmp = Image.FromHbitmap(hbitmap);
             DeleteDC(hscrdc);
             DeleteDC(hmemdc);*/
            Bitmap QQPic = new Bitmap(windowRect.Width, windowRect.Height);
             Graphics g1 = Graphics.FromImage(QQPic);
             IntPtr hdc1 = GetDC(hWnd);
             IntPtr hdc2 = g1.GetHdc();  //得到Bitmap的DC
             BitBlt(hdc2, 0, 0, windowRect.Width, windowRect.Height, hdc1, 0, 0, 13369376);
             g1.ReleaseHdc(hdc2);  //释放掉Bitmap的DC
             QQPic.Save("c:\\QQpic.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
             return QQPic;
        }
    }
   
}
