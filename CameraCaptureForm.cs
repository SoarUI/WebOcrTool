﻿using desktopGraphic2DTool;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WebPageOcr
{

    public partial class CameraCaptureForm : Form
    {
        public delegate void CameraCaptureEventHandler();
        public Mat FrameMat = null;
        private int VideoCapture_id = 0;
        private bool Collimator_flag;
        private bool ROI_flag;
        private bool FlipX_flag;
        private bool FlipY_flag;
        private static bool Vopen_flag; 
        Thread Video_thread; 
        VideoCapture mVideoCapture;
        ManualResetEvent mManualResetEvent = new ManualResetEvent (false);
        public CameraCaptureForm()
        {
            InitializeComponent();
            FrameMat = null;
        }
        
        public void ReadFrameFromWebCam()
        {
            // 加载人脸分类器
           CascadeClassifier faceCascade = new CascadeClassifier();

            //分类器文件下载地址: https://github.com/opencv/opencv/tree/master/data/haarcascades
            //在OpenCV的源码目录下其实也有（opencv\build\etc\haarcascades）。
            faceCascade.Load("./data/haarcascades/haarcascade_frontalface_alt2.xml");
            while (Vopen_flag)
            {    
                Mat frame = new Mat();
                if (mVideoCapture.Read(frame))    // 抓取和解码，返回下一帧
                {
                    if(frame.Empty() )
                    {
                        break;
                    }
                    int sleepTime = (int)Math.Round(1000 / mVideoCapture.Fps);

                    Cv2.WaitKey(sleepTime);
                    if (FlipY_flag) Cv2.Flip(frame, frame, OpenCvSharp.FlipMode.X); //上下翻转
                    if (FlipX_flag) Cv2.Flip(frame, frame, OpenCvSharp.FlipMode.Y); //左右翻转
                    if (Collimator_flag)  //画准星
                    {
                        Cv2.Line(frame, new OpenCvSharp.Point(frame.Cols / 2, frame.Rows / 2 + 35), new OpenCvSharp.Point(frame.Cols / 2, frame.Rows / 2 + 15), new Scalar(0, 0, 255), 2, LineTypes.Link8);
                        Cv2.Line(frame, new OpenCvSharp.Point(frame.Cols / 2, frame.Rows / 2 - 35), new OpenCvSharp.Point(frame.Cols / 2, frame.Rows / 2 - 15), new Scalar(0, 0, 255), 2, LineTypes.Link8);
                        Cv2.Line(frame, new OpenCvSharp.Point(frame.Cols / 2 + 35, frame.Rows / 2), new OpenCvSharp.Point(frame.Cols / 2 + 15, frame.Rows / 2), new Scalar(0, 0, 255), 2, LineTypes.Link8);
                        Cv2.Line(frame, new OpenCvSharp.Point(frame.Cols / 2 - 35, frame.Rows / 2), new OpenCvSharp.Point(frame.Cols / 2 - 15, frame.Rows / 2), new Scalar(0, 0, 255), 2, LineTypes.Link8);
                    }
                    // 对图像进行人脸检测

                    Rect[] faces = faceCascade.DetectMultiScale(frame, 1.08, 2, HaarDetectionTypes.ScaleImage);
                    List<System.Drawing.Rectangle> rfaces = new List<System.Drawing.Rectangle>();
                    foreach (Rect face in faces)
                    {
                        System.Drawing.Rectangle r = new System.Drawing.Rectangle(face.X, face.Y, face.Width, face.Height);
                        //this.GetLandmarks(frame, face, rfaces);
                        OpenCvSharp.Cv2.Rectangle(img: frame, rect: face, color: new Scalar(0, 255, 0), thickness: 1);
                        rfaces.Add(r);
                    }
                    if (ROI_flag)  //画ROI
                    {
                        int V_width = frame.Width;
                        int V_height = frame.Height;
                        int start_x = frame.Width / 4;
                        int start_y = frame.Height / 4;
                        OpenCvSharp.Point strat_point = new OpenCvSharp.Point(frame.Width / 2, frame.Height / 2);
                        Rect ROI_rect = new Rect(start_x, start_y, V_width / 2, V_height / 2);
                        Mat srcImg = new Mat(frame, ROI_rect).Clone();
                        Cv2.Rectangle(frame, ROI_rect, new Scalar(255, 255, 0), 2);

                    }
                    {
                        if(FrameMat !=null)
                        {
                            FrameMat.Release();
                            FrameMat = frame.Clone();
                        }
                        else
                        {
                            FrameMat = frame.Clone();
                        }
                        CameraCaptureEventHandler eventhandler= new CameraCaptureEventHandler (FrameFromWebCam);
                        eventhandler.BeginInvoke(null,null);
                    }
                    
                    frame.Release();//释放内存
                    
                }
                
            }
            mManualResetEvent.Set();
        }
       
        public void FrameFromWebCam( )
        {
            if(FrameMat != null)
            camview.Image = opcvFindPicture.Mat2Bitmap(FrameMat);
        }
        private void btnStartCam_Click(object sender, EventArgs e)
        {
            btnStartCam.Enabled = false;
            if (!Vopen_flag)
            {
                try
                {
                    mVideoCapture = new VideoCapture(VideoCapture_id);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("OpenCV open error：" + ex.Message);
                    btnStartCam.Enabled = true;
                    return;
                }
                if (mVideoCapture.IsOpened())
                {
                    Vopen_flag = true;
                    Video_thread = new Thread(this.ReadFrameFromWebCam);
                    Video_thread.IsBackground = true;
                    Video_thread.Start();
                    camview.Image = null;
                    btnStartCam.Text = "Close Camera";
                }
            }
            else
            {
                Vopen_flag = false;
                mManualResetEvent.WaitOne();
                mManualResetEvent.Reset();
                mVideoCapture.Release();
                btnStartCam.Text = "Open Camera";
            }
            btnStartCam.Enabled = true;
        }

        private void CameraCaptureForm_Load(object sender, EventArgs e)
        {
            CameraInit();
            CameraflagInit();
        }

        private void cbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCameras.SelectedIndex == -1)
                return;
            VideoCapture_id = cbCameras.SelectedIndex;
        }

        private void btnCamRefresh_Click(object sender, EventArgs e)
        {
            CameraInit();
           
        }
        private void CameraInit()
        {
            cbCameras.Items.Clear();
            foreach (var device in EnumCamera.Devices)
            {
                
                cbCameras.Items.Add(device);
            }
            if (cbCameras.Items.Count == 0)
            {
                MessageBox.Show("cannot dound any camera in your PC");
            }
            else
            {
                cbCameras.SelectedIndex = 0;
            }
        }
        private void CameraflagInit()
        {

            
            Collimator_flag = Properties.Settings.Default.Collimator_flag;
            if (Collimator_flag) chkCollimator.Checked = true;
            else chkCollimator.Checked = false;

            ROI_flag = Properties.Settings.Default.ROI_flag;
            if (ROI_flag) chkROI.Checked = true;
            else chkROI.Checked = false;

            FlipX_flag = Properties.Settings.Default.FlipX_flag;
            if (FlipX_flag) chkFlipX.Checked = true;
            else chkFlipX.Checked = false;
          
            FlipY_flag = Properties.Settings.Default.FlipY_flag;
            if (FlipY_flag) chkFlipY.Checked = true;
            else chkFlipY.Checked = false;

        }

        private void btnSaveScreen_Click(object sender, EventArgs e)
        {
           
            Vopen_flag = false;
            mManualResetEvent.WaitOne();
            mVideoCapture.Release();
            this.Close();
        }

        private void chkCollimator_CheckedChanged(object sender, EventArgs e)
        {
            Collimator_flag = chkCollimator.Checked;
        }

        private void chkROI_CheckedChanged(object sender, EventArgs e)
        {
            ROI_flag = chkROI.Checked;
        }

        private void chkFlipX_CheckedChanged(object sender, EventArgs e)
        {
            FlipX_flag = chkFlipX.Checked;
        }

        private void chkFlipY_CheckedChanged(object sender, EventArgs e)
        {
            FlipY_flag = chkFlipY.Checked;
        }
    }
}
