
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using OpenCvSharp;
using System.Drawing.Imaging;
using desktopGraphic2DTool;
using Tesseract;
using Tesseract.Interop;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static System.Windows.Forms.AxHost;
using OpenCvSharp.Dnn;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using NPOI.XWPF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using System.Runtime.Remoting.Contexts;
using SixLabors.ImageSharp.Processing;
using static NPOI.HSSF.Util.HSSFColor;
using NPOI.HSSF.Record.CF;
using NPOI.SS.Formula.Functions;

namespace WebPageOcr
{
    public partial class WebPageOcrForm : Form
    {
        private bool b_leftbuttondown;
        private IntPtr OldWinPtr=IntPtr.Zero;
        public bool bIsExtit =false;
        public bool bPause=false;
        public string scriptFile=string.Empty;
        public string scriptName=string.Empty;
        public string scriptPath=string.Empty;
        public string scriptFolderPath = string.Empty;
        public string browserguid=string.Empty;

        public delegate void UpdateScriptStatusDelegate(string str);
        public delegate void LuaJSClickDelegate(WebPageOcrForm tabctrl, int x, int y, int left);
       
        public delegate void LuaDebugShow( string message);
        public delegate void LuaOpenWebUri(string url, string hostip, int hostport, string scriptfile, string mainfrm, int? insertIndex);
        public delegate void LuaCloseTabPage(int insertIndex);
        public IntPtr targetMessageWnd=IntPtr.Zero;
        public IntPtr targetPictureWin=IntPtr.Zero;
        private Bitmap screenbmp;
        public Mat screenMat = null;
        OpenCvSharp.Point[][] contours;
        private bool b_leftbuttondownInPic2=false;
        OpenCvSharp.Point ptStart;
        OpenCvSharp.Point[] RectPoints= new OpenCvSharp.Point[4];
        private float frateX = 1.0f;
        private float frateY = 1.0f;
        private bool b_customerdownInPic2 = false;
        private List<OpenCvSharp.Point> PointList= new List<OpenCvSharp.Point>(); 
        Pen helppen=null;
        private int iLanguage =0;//english default
        private double froteangle = 0.0f;
        private int iThrodMethod = 0;//english default
        private bool bPointListSorted = false;
        public WebPageOcrForm()
        {
            InitializeComponent();
            OldWinPtr = IntPtr.Zero;
            targetMessageWnd = IntPtr.Zero;
            targetPictureWin = IntPtr.Zero;
            b_leftbuttondown = false;
            browserguid = Guid.NewGuid().ToString();
            this.scriptFolderPath = System.Windows.Forms.Application.StartupPath + "/script/";
            helppen = new Pen(Color.Black, 1);
            helppen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
           
            englishToolStripMenuItem.Checked = true;
            chineseToolStripMenuItem.Checked = false;
            froteangle = 0.0f;
            ThresholdnumericUpDown.Value = 20;
            iThrodMethod = 0;
            thresholdOneToolStripMenuItem.Checked = true;
            thresholdTwoToolStripMenuItem.Checked = false;
        }

        ///////////////////////////////////////
        OpenCvSharp.Point2f getPointAffinedPos( OpenCvSharp.Point2f src, OpenCvSharp.Point2f center, double angle)
        {

            OpenCvSharp.Point2f dst= new OpenCvSharp.Point2f() ;
            double x = src.X - center.X;
            double y = src.Y - center.Y;

            dst.X = (float)(x* Math.Cos(angle) + y* Math.Sin(angle) + center.X);
            dst.Y = (float)(-x* Math.Sin(angle) + y* Math.Cos(angle) + center.Y);
            return dst;
        }
        private void btnGetOcrString_Click(object sender, EventArgs e)
        {
            if (screenMat == null)
                return;
            if (PointList.Count <= 0)
            {
                MessageBox.Show("Please Select a region !!");
                return;
            }
            
            RectPoints = PointList.ToArray();
            
            Array.Sort(RectPoints, (cs1, cs2) =>
            {
                if (cs1 != null && cs1 != null)
                {
                    if (cs1.Y > cs2.Y) //Y 升序排列
                        return 1;
                    else if (cs1.Y == cs2.Y)
                    {
                        if (cs1.X < cs2.X) //X 降序
                            return 1;
                        else return -1;
                    }
                    else
                        return -1;
                }
                return 0;

            });

            //算法找出的角点: 
            /*
             *      1           0
             *      2           3
             */
            OpenCvSharp.Point2f[] srcPt = new OpenCvSharp.Point2f[4];
            PointList.Clear();
            PointList.AddRange(RectPoints);
            if (RectPoints[0].X> RectPoints[1].X)
            {
                srcPt[0] = RectPoints[0];
                srcPt[1] = RectPoints[1];
                
            }
            else {
                srcPt[0] = RectPoints[1];
                srcPt[1] = RectPoints[0];
                PointList[0] = RectPoints[1];
                PointList[1] = RectPoints[0];
            }

            //Y升序 X降序 (Y[0]~=Y[1]:must sort the X )
            if (RectPoints[2].X > RectPoints[3].X)
            {
                srcPt[2] = RectPoints[3];
                srcPt[3] = RectPoints[2];
                PointList[2] = RectPoints[3];
                PointList[3] = RectPoints[2];
            }
            else
            {
                srcPt[2] = RectPoints[2];
                srcPt[3] = RectPoints[3];
            }
            //scale test
            for(int j=0;j<4;j++)
            {
                srcPt[j].X = (float)Math.Floor(srcPt[j].X * frateX);
                srcPt[j].Y = (float)Math.Floor(srcPt[j].Y * frateY);
            }
          
            
            RotatedRect rect = Cv2.MinAreaRect(srcPt);
            /*
            *      1           0
            *      2           3
            */
            OpenCvSharp.Rect box = rect.BoundingRect();
           
           
            OpenCvSharp.Point2f[] dstPt = new OpenCvSharp.Point2f[4];
            dstPt[0].X = box.Width;
            dstPt[0].Y = 0;

            dstPt[1].X = 0;
            dstPt[1].Y = 0;

            dstPt[2].X = 0;
            dstPt[2].Y =  box.Height;

            dstPt[3].X = box.Width;
            dstPt[3].Y = box.Height;
            Mat src2 = screenMat.Clone();
            Mat final = new Mat();
           
            Mat warpmatrix = Cv2.GetPerspectiveTransform(srcPt, dstPt);
            Cv2.WarpPerspective(src2, final, warpmatrix, new OpenCvSharp.Size(box.Width, box.Height));
                                                                                                      

            //转换为bitmap
            bPointListSorted = true;
            //translate img to strings
            Mat grayMa= ocr_preProcess(final);
           Ocr_reconize(grayMa);
        }
        private Mat ocr_preProcess(Mat src)
        {
            
            if (frateX < 0.1)
            {
                Cv2.Resize(src, src, new OpenCvSharp.Size(), 1.0f/frateX, 1.0/frateY);
            }
           
            
            if (src.Channels() == 3)
            Cv2.CvtColor(src, src, ColorConversionCodes.RGB2GRAY);
            if (src.Channels() > 3)
            Cv2.CvtColor(src, src, ColorConversionCodes.RGBA2GRAY);
            int chn = src.Channels();
           
            double thresh = Cv2.Mean(src).ToDouble();
          
            //if (frateX > 0.8)
            {
                if(iThrodMethod == 0)
                {
                    double dThreshold= (double)ThresholdnumericUpDown.Value;
                    
                    Cv2.AdaptiveThreshold(src, src, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, dThreshold);
                }
                else if(iThrodMethod ==1)
                {
                    double dThreshold = (double)ThresholdnumericUpDown.Value;
                    Cv2.Threshold(src, src, thresh- dThreshold, 255, ThresholdTypes.Binary);
                }
                //
                
            }
            //scale X Y
            try
            {
                float fx = Convert.ToSingle(txbfx.Text);
                float fy = Convert.ToSingle(txbfy.Text);
                Cv2.Resize(src, src, new OpenCvSharp.Size(), fx, fy);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return src;
        }
        private void Ocr_reconize(Mat ma)
        {
            
            if (ma == null)
                return;
            TesseractEngine ocr = null;
            if (iLanguage == 1)
            {
                ocr = new TesseractEngine("./tessdata/", "chi_sim", EngineMode.Default);
            }
            else
            {
                ocr = new TesseractEngine("./tessdata/", "eng", EngineMode.Default);
            }
            pictureBox1.Image = opcvFindPicture.Mat2Bitmap(ma);
            var page = ocr.Process(opcvFindPicture.Mat2Bitmap(ma));
            
            //var page = ocr.Process(Mat2Pix(final));
            this.richTextBox1.Text = page.GetText();
        }
        private void btnAutoFit_Click(object sender, EventArgs e)
        {
            if (screenMat == null)
                return;
            Mat src = screenMat.Clone();
            bPointListSorted = false;
            this.pictureBox2.Invalidate();
            this.pictureBox2.Update();
            if (src.Channels() > 3)
                Cv2.CvtColor(src, src, ColorConversionCodes.RGBA2RGB);
           
            InputArray kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
           
            Cv2.MorphologyEx(src, src, MorphTypes.Open, kernel, new OpenCvSharp.Point(-1, -1), 3);
          
            Cv2.MorphologyEx(src, src, MorphTypes.Close, kernel, new OpenCvSharp.Point(-1, -1), 3);
            
             Cv2.PyrMeanShiftFiltering(src, src, sp: 8, sr: 60);
           
            Cv2.GaussianBlur(src, src, new OpenCvSharp.Size(11, 11), 2, 2);
            

            Mat canny_Image = new Mat();
            Cv2.Canny(src, canny_Image, 10, 30, 3, false);

            HierarchyIndex[] hierarchly;
           
            Cv2.FindContours(canny_Image, out contours, out hierarchly,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple,
                new OpenCvSharp.Point(0, 0));

            if (contours.Length == 0)
            {
                PointList.Clear();
                OpenCvSharp.Point ptrt = new OpenCvSharp.Point();
                ptrt.X = screenMat.Width;
                ptrt.Y = 0;
                PointList.Add(ptrt);
                OpenCvSharp.Point ptlt = new OpenCvSharp.Point();
                ptlt.X = 0;
                ptlt.Y = 0;
                PointList.Add(ptlt);
                OpenCvSharp.Point ptlb = new OpenCvSharp.Point();
                ptlb.X = 0;
                ptlb.Y = screenMat.Height;
                PointList.Add(ptlb);
                OpenCvSharp.Point ptrb = new OpenCvSharp.Point();
                ptrb.X = screenMat.Width;
                ptrb.Y = screenMat.Height;
                PointList.Add(ptrb);
                int count = PointList.Count;
                if (count >= 4)
                {
                   
                    using (Pen pen = new Pen(Color.Red, 3))
                    {
                        using (Graphics g = this.pictureBox2.CreateGraphics())
                        {
                            for (int i = 0; i < count - 1; i++)
                            {
                                g.DrawLine(pen, PointList[i].X, PointList[i].Y, PointList[i + 1].X, PointList[i + 1].Y);
                            }
                            g.DrawLine(pen, PointList[3].X, PointList[3].Y, PointList[0].X, PointList[0].Y);
                        }
                    }
                }
                MessageBox.Show("Auto detect Region Failde,Please Select a region");
                return;
            }
            CannyAutoRect();


        }
        //auto generate a rect
        public void CannyAutoRect()
        {
            if (contours == null)
                return;
            Mat src = screenMat.Clone();
           
            double max_area = 0.0;
            double currentArea = 0.0;
            OpenCvSharp.Point[] max_contour = null;
            for (int i = 0; i < contours.Length; i++)
            {
                currentArea = Cv2.ContourArea(contours[i])+ Cv2.ArcLength(contours[i], true);
                if (currentArea > max_area)
                {
                    max_area = currentArea;
                    max_contour = contours[i];
                }
            }

            
            OpenCvSharp.Point[] hull = Cv2.ConvexHull(max_contour);
            
            double epsilon = 0.02*Cv2.ArcLength(max_contour, true);
            
            OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(hull, epsilon, true);

            Scalar scalar3 = new Scalar(0, 255, 0);
            for (int i=0;i<approx.Length-1;i++)
            {
                Cv2.Line(src, approx[i], approx[i+1], scalar3, 2, LineTypes.Link4);
            }
            if(approx.Length>2)
                Cv2.Line(src, approx[approx.Length-1], approx[0], scalar3, 2, LineTypes.Link4);
            
           
            PointList.Clear();
            if (approx.Length < 4)
            {
                OpenCvSharp.Point ptrt = new OpenCvSharp.Point();
                ptrt.X = pictureBox2.Width;
                ptrt.Y = 0;
                PointList.Add(ptrt);
                OpenCvSharp.Point ptlt = new OpenCvSharp.Point();
                ptlt.X = 0;
                ptlt.Y = 0;
                PointList.Add(ptlt);
                OpenCvSharp.Point ptlb = new OpenCvSharp.Point();
                ptlb.X = 0;
                ptlb.Y = pictureBox2.Height;
                PointList.Add(ptlb);
                OpenCvSharp.Point ptrb = new OpenCvSharp.Point();
                ptrb.X = pictureBox2.Width;
                ptrb.Y = pictureBox2.Height;
                PointList.Add(ptrb);
            }
            else
            {
                float fx = this.pictureBox2.Width * 1.0f / screenbmp.Width;
                float fy = this.pictureBox2.Height * 1.0f / screenbmp.Height;
                for(int i=0;i<4;i++ )
                {
                    OpenCvSharp.Point ptrt = new OpenCvSharp.Point();
                    ptrt=approx[i];
                    ptrt.X = (int)Math.Floor(ptrt.X * fx);
                    ptrt.Y = (int)Math.Floor(ptrt.Y * fy);
                    PointList.Add(ptrt);
                    
                }
            }
           
            int count = PointList.Count;
            if (count >= 4)
            {
                b_customerdownInPic2 = false;
                using (Pen pen = new Pen(Color.Green, 2))
                {
                    using (Graphics g = this.pictureBox2.CreateGraphics())
                    {
                        
                        for (int i = 0; i < count - 1; i++)
                        {
                            g.DrawLine(pen, PointList[i].X , PointList[i].Y , PointList[i + 1].X , PointList[i + 1].Y );
                        }
                        g.DrawLine(pen, PointList[3].X , PointList[3].Y, PointList[0].X, PointList[0].Y );
                        
                    }
                }
            }

        }
        private void btnWriteText_Click(object sender, EventArgs e)
        {
            this.btnWriteText.Enabled = false;
            this.bIsExtit = true;
            this.bPause = false;

            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                this.btnWriteText.Enabled = true;
                return;
            }
           
            string filename = string.Format("{0}/txtfiles/{1}.txt", System.Windows.Forms.Application.StartupPath, browserguid);
            Program.makesuredir(filename);
            System.IO.File.WriteAllText(filename, richTextBox1.Text.Replace("\n", Environment.NewLine));
            outPutDocument(richTextBox1.Text.Replace("\n", Environment.NewLine));
            this.btnWriteText.Enabled = true;
        }
        private void outPutDocument(string text)
        {
            
            var doc = new XWPFDocument();
            doc.Document.body.sectPr = new CT_SectPr();
            CT_SectPr m_SectPr = doc.Document.body.sectPr;
            m_SectPr.pgSz.h = (ulong)16838;
            m_SectPr.pgSz.w = (ulong)11906;
            
            m_SectPr.pgMar.left = (ulong)800;
            m_SectPr.pgMar.right = (ulong)800;
            m_SectPr.pgMar.top = (ulong)850;
            m_SectPr.pgMar.bottom = (ulong)850;
            doc.Document.body.sectPr = m_SectPr;
            
            var paragraph = doc.CreateParagraph();
            paragraph.Alignment = ParagraphAlignment.CENTER; 
            var run = paragraph.CreateRun();
            run.IsBold = true;
            run.SetText(text);
            run.FontSize = 28;
            run.SetFontFamily("黑体", FontCharRange.None); 
            
            paragraph.SpacingBeforeLines = 20;
            paragraph.SpacingAfterLines = 20;
           
           
            paragraph = doc.CreateParagraph();
            paragraph.CreateRun().AddBreak(BreakType.PAGE);
            string filename = string.Format("{0}/docs/{1}.doc", System.Windows.Forms.Application.StartupPath, browserguid);
            Program.makesuredir(filename);
            FileStream Fs = new FileStream(filename, FileMode.OpenOrCreate);

            doc.Write(Fs);
            Fs.Close();
        }




        public  void DebugShow(string message)
        {
            this.labelmessage.Text = message;
        }
      
        //--------Source Picture --------
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            bPointListSorted = false;
            if (b_customerdownInPic2)
            {
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
            }
            else
            {
                PointList.Clear();
                b_leftbuttondownInPic2 = true;
                ptStart = new OpenCvSharp.Point();
                ptStart.X = e.X;
                ptStart.Y = e.Y;
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
                
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (b_customerdownInPic2)
            {
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
                int count = PointList.Count;
                if (count >= 1 && count < 4)
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    using(Pen prePen= new Pen(Color.Green, 2))
                    {
                        using (Graphics g = this.pictureBox2.CreateGraphics())
                        {
                            for (int i = 0; i < count - 1; i++)
                            {
                                g.DrawLine(pen, PointList[i].X, PointList[i].Y, PointList[i + 1].X, PointList[i + 1].Y);
                            }
                            g.DrawLine(prePen, PointList[count - 1].X, PointList[count - 1].Y, e.X, e.Y);

                            if (count == 2)
                            {
                                PointF po = GetPointToLineVerticalCross(new PointF(PointList[0].X, PointList[0].Y), new PointF(PointList[1].X, PointList[1].Y), new PointF(e.X, e.Y));
                                g.DrawLine(helppen, po.X, po.Y, e.X, e.Y);
                            }
                            else if (count == 3)
                            {
                                PointF po = GetPointToLineVerticalCross(new PointF(PointList[0].X, PointList[0].Y), new PointF(PointList[1].X, PointList[1].Y), new PointF(e.X, e.Y));
                                g.DrawLine(helppen, po.X, po.Y, e.X, e.Y);
                                PointF po1 = GetPointToLineVerticalCross(new PointF(PointList[1].X, PointList[1].Y), new PointF(PointList[2].X, PointList[2].Y), new PointF(e.X, e.Y));
                                g.DrawLine(helppen, po1.X, po1.Y, e.X, e.Y);
                            }
                            else
                            {
                                //horizonline at(e.x,e.y)
                                g.DrawLine(helppen, 0, e.Y, e.X, e.Y);
                                g.DrawLine(helppen, e.X, 0, e.X, e.Y);
                            }
                        }
                    }
                }
                return;
            }
           if (b_leftbuttondownInPic2)
            {
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
                using (Pen pen = new Pen(Color.Red, 2))
                    {
                        using (Graphics g = this.pictureBox2.CreateGraphics())
                        {
                        int nWidth = 0;
                        int nHeight = 0;
                        Font font = new Font("Courer New", 9, FontStyle.Bold);
                        if (e.X > ptStart.X)//rightside->
                        {
                            if (e.Y > ptStart.Y) //down direction
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = e.Y - ptStart.Y - (int)2;
                                g.DrawRectangle(pen, ptStart.X, ptStart.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, ptStart.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = ptStart.Y-e.Y - (int)2;
                                g.DrawRectangle(pen, ptStart.X,e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, ptStart.X + 1, e.Y + 1);
                            }
                        }
                        else
                        {
                            if (e.Y > ptStart.Y) //down direction
                            {
                                nWidth = ptStart.X-e.X   - (int)2;
                                nHeight = e.Y - ptStart.Y - (int)2;
                                g.DrawRectangle(pen, e.X, ptStart.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, e.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = ptStart.X-e.X - (int)2;
                                nHeight = ptStart.Y - e.Y - (int)2;
                                g.DrawRectangle(pen, e.X, e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, e.X + 1, e.Y + 1);
                            }
                        }
                            
                        }
                    }
                return;
            }
            int count2 = PointList.Count;
            if (count2 >= 4 && bPointListSorted )
            {
                b_customerdownInPic2 = false;
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    using (Graphics g = this.pictureBox2.CreateGraphics())
                    {
                        for (int i = 0; i < count2 - 1; i++)
                        {
                            g.DrawLine(pen, PointList[i].X, PointList[i].Y, PointList[i + 1].X, PointList[i + 1].Y);
                        }
                        g.DrawLine(pen, PointList[3].X, PointList[3].Y, PointList[0].X, PointList[0].Y);
                    }
                }
                return;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (b_customerdownInPic2)
            {
                int count = PointList.Count;
                if(count <4)
                    PointList.Add(new OpenCvSharp.Point(e.X, e.Y));
                count = PointList.Count;
                if (count >= 4)
                {
                    b_customerdownInPic2 = false;
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        using (Graphics g = this.pictureBox2.CreateGraphics())
                        {
                            for (int i = 0; i < count - 1; i++)
                            {
                                g.DrawLine(pen, PointList[i].X, PointList[i].Y, PointList[i + 1].X, PointList[i + 1].Y);
                            }
                            g.DrawLine(pen, PointList[3].X, PointList[3].Y, PointList[0].X, PointList[0].Y);
                        }
                    }
                    return;
                }
                if (count > 1)
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        using (Graphics g = this.pictureBox2.CreateGraphics())
                        {
                            for (int i = 0; i < count - 1; i++)
                            {
                                g.DrawLine(pen, PointList[i].X, PointList[i].Y, PointList[i + 1].X, PointList[i + 1].Y);
                            }
                            //g.DrawLine(pen, PointList[count-2].X, PointList[count-2].Y, PointList[count-1].X, PointList[count-1].Y);
                        }
                    }
                }
            }
            else// simple rect mode
            {
                b_leftbuttondownInPic2 = false;
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
                int nWidth = Math.Abs(e.X - ptStart.X - (int)2);
                int nHeight = Math.Abs(e.Y - ptStart.Y - (int)2);
                if (nWidth < 5 || nHeight < 5)
                {
                    return;
                }
                //construct the rect point :start left right endpoint
                /*
                 * ptstart         ptR
                 * ptBottom        endPoint
                 */
                
                PointList.Add(ptStart);
                OpenCvSharp.Point ptR = new OpenCvSharp.Point();
                ptR.X = e.X;
                ptR.Y = ptStart.Y;
                
                PointList.Add(ptR);
                OpenCvSharp.Point ptb = new OpenCvSharp.Point();
                ptb.X = ptStart.X;
                ptb.Y = e.Y;
                
                PointList.Add(ptb);
                OpenCvSharp.Point ptE = new OpenCvSharp.Point();
                ptE.X = e.X;
                ptE.Y = e.Y;
               
                PointList.Add(ptE);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    using (Graphics g = this.pictureBox2.CreateGraphics())
                    {
                        Font font = new Font("Courer New", 9, FontStyle.Bold);
                        if (e.X > ptStart.X)//rightside->
                        {
                            if (e.Y > ptStart.Y) //down direction
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = e.Y - ptStart.Y - (int)2;
                               
                                g.DrawRectangle(pen, ptStart.X, ptStart.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, ptStart.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = ptStart.Y - e.Y - (int)2;
                                g.DrawRectangle(pen, ptStart.X, e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, ptStart.X + 1, e.Y + 1);
                            }
                        }
                        else
                        {
                            if (e.Y > ptStart.Y) //down direction
                            {
                                nWidth = ptStart.X - e.X - (int)2;
                                nHeight = e.Y - ptStart.Y - (int)2;
                                g.DrawRectangle(pen, e.X, ptStart.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, e.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = ptStart.X - e.X - (int)2;
                                nHeight = ptStart.Y - e.Y - (int)2;
                                g.DrawRectangle(pen, e.X, e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, Brushes.Red, e.X + 1, e.Y + 1);
                            }
                        }
                    }
                   
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!b_customerdownInPic2)
            {
                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
            }
            b_customerdownInPic2 = !b_customerdownInPic2;
            bPointListSorted = false;
            PointList.Clear();
        }

        private void fromScreenCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var screenCaptureWin = new ScreenCaptureForm(this);
            screenCaptureWin.ShowDialog();
            if(screenCaptureWin.screenbmp!=null)
                LoadBitmap(screenCaptureWin.screenbmp);
        }
        /////////
        private void LoadBitmap(Bitmap bitmap)
        {
            screenbmp = bitmap;
             froteangle = 0.0f;
            Mat tmpmat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC4);
            BitmapData bd = screenbmp.LockBits(new System.Drawing.Rectangle(0, 0, screenbmp.Width, screenbmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int srcStep = bd.Stride;
            long dstStep = tmpmat.Step();
            int bufSize = bitmap.Height * bd.Stride;
            unsafe
            {
                byte* sp = (byte*)bd.Scan0;// + bufSize;
                byte* dp = (byte*)tmpmat.Data;
                for (int y = 0; y < bitmap.Height; y++)
                {

                    Buffer.MemoryCopy(sp, dp, dstStep, dstStep);
                    dp += dstStep;
                    sp += srcStep;

                }
            }
            screenbmp.UnlockBits(bd);
            if (screenMat != null)
            {
                screenMat.Dispose();
            }
            //if it is a old object ,if the size not the same ,renew one
            // Copy line bytes from src to dst for each line
            screenMat = tmpmat;
           
            
            int maxsize = bitmap.Height > bitmap.Width ? bitmap.Height : bitmap.Width;
            double mincircle=Math.Sqrt(2)*maxsize/2;
            double maxcircle = mincircle * Math.Sqrt(2);
            double deltaX = maxcircle /2- bitmap.Width / 2;
            double deltaY = maxcircle/2 - bitmap.Height / 2;
            // create the translation matrix using tx and ty
            float[] warp_values = new float[]{ 1.0f, 0.0f, (float)deltaX, 0.0f, 1.0f, (float)deltaY };
            Mat translation_matrix = new Mat(2, 3, MatType.CV_32F, warp_values);
            Mat final2 = new Mat();
            Cv2.WarpAffine(screenMat, final2, translation_matrix, new OpenCvSharp.Size(maxcircle, maxcircle), InterpolationFlags.Cubic);
            if (screenMat != null)
            {
                screenMat.Dispose();
            }
            screenMat = final2;
            screenbmp = opcvFindPicture.Mat2Bitmap(final2);
            pictureBox2.Image = screenbmp;
            //caculate the ratio 
            frateX = screenbmp.Width * 1.0f / this.pictureBox2.Width;
            frateY = screenbmp.Height * 1.0f / this.pictureBox2.Height;
            
            this.txbfx.Text = "1.0";
            this.txbfy.Text = "1.0";

        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "ImageFiles|*.jpg|JPEG|*.jpeg|Bitmap|*.bmp|All|*.*";//Text files (*.txt)|*.txt|All files (*.*)|*.*"”
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.FileName))
                {
                    System.Windows.Forms.MessageBox.Show(this, "cannot be empty", "error");
                    return;
                }
                Bitmap bitmap= new Bitmap(dialog.FileName);
                LoadBitmap(bitmap);
            }
        }

        private void chineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iLanguage = 1;
            englishToolStripMenuItem.Checked = false;
            chineseToolStripMenuItem.Checked = true;
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iLanguage = 0;
            englishToolStripMenuItem.Checked = true;
            chineseToolStripMenuItem.Checked = false;
        }
        private void autoDetectRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAutoFit_Click(this, null);
        }

        private void customerRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!b_customerdownInPic2)
            {

                this.pictureBox2.Invalidate();
                this.pictureBox2.Update();
            }
            b_customerdownInPic2 = !b_customerdownInPic2;
            PointList.Clear();
        }
        /// <summary> /// 根据两点求出垂线过第三点的直线的交点 
        /// </summary> 
        /// <param name="pt1">直线上的点1</param>
        /// <param name="pt2">直线上的点2</param> 
        /// <param name="pt3">垂线上的点（当前鼠标的点）</param> 
        /// <returns>返回点到直线的垂直交点坐标</returns>
        public PointF GetPointToLineVerticalCross(PointF pt1, PointF pt2, PointF pt3) 
        { 
            //垂直线
          if (pt1.X == pt2.X) 
            { 
                return new PointF(pt1.X, pt3.Y); 
            } 
          //水平线
          if (pt1.Y == pt2.Y) 
            { 
                return new PointF(pt3.X, pt1.Y); 
            } 
            float kA = (pt1.Y - pt2.Y) * 1.0f / (pt1.X - pt2.X); // k=y/x;  
           
            float B = (pt1.Y - kA * pt1.X);
            

            float m = pt3.X + kA * pt3.Y; 
            /// 求两直线交点坐标 
            PointF ptCross = new PointF(0, 0); 
            ptCross.X = (m - kA * B) * 1.0f / (kA * kA + 1); 
            ptCross.Y = kA * ptCross.X + B;
            return ptCross; 
        }
        [DllImport("leptonica-1.82.0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "pixReadMem")]
       private static extern unsafe IntPtr pixReadMem(byte* data, int length);
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
        private Pix Mat2Pix(Mat mat)
        {
            if (mat == null)
            {
                return null;
            }
            var dllFile = Path.Combine(Environment.Is64BitProcess ? "x64" : "x86", "leptonica-1.82.0.dll");
            dllFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllFile);
            LoadLibrary(dllFile);
            int cols = mat.Cols;
            int rows = mat.Rows;
            int bufsize = cols * rows * mat.Channels();
            IntPtr intPtr;
            unsafe
            {
                intPtr = pixReadMem(mat.DataPointer, bufsize);
            }
            if (intPtr == IntPtr.Zero)
            {
                throw new IOException("Failed to load image from memory.");
            }
            Pix px = Pix.Create(intPtr);
            return px;
        }

        private void picroteleft_Click(object sender, EventArgs e)
        {
            if (screenMat == null)
                return;
            PointList.Clear();
            bPointListSorted = false;
            froteangle += 1.0f;
            froteangle %= 360;
            int nwith = screenMat.Width;
            int nheight = screenMat.Height;

            float longCircle = nwith > nheight ? nwith : nheight;
            float icircle = longCircle * (float)Math.Sqrt(2) * 0.5f;
            int cx = (int)longCircle / 2;
            int cy = (int)longCircle / 2;
            Scalar color;
            color = new Scalar(0, 0, 255);//BGR sample

            Mat rot = Cv2.GetRotationMatrix2D(new Point2f(cx, cy), /*froteangle*/2.0f, 1.0);
            Mat final2 = new Mat();
            Cv2.WarpAffine(screenMat, final2, rot, new OpenCvSharp.Size(longCircle, longCircle), InterpolationFlags.Cubic);
            screenMat.Dispose();
            screenMat = final2;
            pictureBox2.Image = opcvFindPicture.Mat2Bitmap(final2);
        }

        private void picroteright_Click(object sender, EventArgs e)
        {
            if (screenMat == null)
                return;
            PointList.Clear();
            bPointListSorted = false;
            froteangle -= 1.0f;
            froteangle %=360;
            int nwith = screenMat.Width ;
            int nheight=screenMat.Height;
            
            float longCircle = nwith > nheight ? nwith : nheight;
            float icircle = longCircle* (float)Math.Sqrt(2)*0.5f;
            int cx = (int)longCircle / 2;
            int cy = (int)longCircle / 2;
            Scalar color;
            color = new Scalar(0, 0, 255);//BGR sample
            
            Mat rot = Cv2.GetRotationMatrix2D(new Point2f(cx, cy), /*froteangle*/-2.0f, 1.0);
            Mat final2 = new Mat();
            Cv2.WarpAffine(screenMat, final2, rot, new OpenCvSharp.Size(longCircle, longCircle), InterpolationFlags.Cubic);
           
            screenMat.Dispose();
            screenMat = final2;
            pictureBox2.Image = opcvFindPicture.Mat2Bitmap(final2);
        }

        private void pictureBox2_Resize(object sender, EventArgs e)
        {
            

        }

        private void pictureBox2_SizeChanged(object sender, EventArgs e)
        {
            if (screenMat == null)
                return;
            pictureBox2.Image = opcvFindPicture.Mat2Bitmap(screenMat);
            //caculate the ratio 
            frateX = screenbmp.Width * 1.0f / this.pictureBox2.Width;
            frateY = screenbmp.Height * 1.0f / this.pictureBox2.Height;
            bPointListSorted = false;
        }

        private void txbfx_Validated(object sender, CancelEventArgs e)
        {
            float fx = 1.0f;
            if (!float.TryParse(txbfx.Text, out fx))
                e.Cancel= true;
        }

        private void txbfy_Validating(object sender, CancelEventArgs e)
        {
            float fy = 1.0f;
            if (!float.TryParse(txbfy.Text, out fy))
                e.Cancel = true;
        }

        private void thresholdOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iThrodMethod = 0;
            thresholdOneToolStripMenuItem.Checked = true;
            thresholdTwoToolStripMenuItem.Checked = false;
        }

        private void thresholdTwoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iThrodMethod = 1;
            thresholdOneToolStripMenuItem.Checked = false;
            thresholdTwoToolStripMenuItem.Checked = true;
        }

        private void fromWebCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraCaptureForm camFm = new CameraCaptureForm();
            this.Hide();
            camFm.ShowDialog();
            if( camFm.FrameMat==null || camFm.FrameMat.Empty() )
            {
                this.Show();
                return;
            }
            Bitmap screenbmp = opcvFindPicture.Mat2Bitmap(camFm.FrameMat);
            LoadBitmap(screenbmp);
            this.Show();
        }

       
    }
}
