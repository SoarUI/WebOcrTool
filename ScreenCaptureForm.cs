using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebPageOcr
{
    public partial class ScreenCaptureForm : Form
    {
        private bool b_leftbuttondown= false;
        Form MainFrm= null;
        Point ptStart;
        Font font;
        SolidBrush brush;
        public Bitmap screenbmp = null;
        public ScreenCaptureForm(Form parent)
        {
            MainFrm = parent;
            InitializeComponent();
            font = new Font("Courer New", 9, FontStyle.Regular);
            brush = new SolidBrush(Color.Red);
        }
       
        private void ScreenCaptureForm_MouseDown(object sender, MouseEventArgs e)
        {
            b_leftbuttondown = true;
            ptStart = new Point();
            ptStart.X = e.X;
            ptStart.Y = e.Y;
            Invalidate();
            Update();
        }

        private void ScreenCaptureForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (b_leftbuttondown)
            {
                Invalidate();
                Update();
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    using (Graphics g = this.CreateGraphics())
                    {
                        int nWidth = 0;
                        int nHeight = 0;
                        if (e.X > ptStart.X)//rightside->
                        {
                            if (e.Y > ptStart.Y) //down direction
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = e.Y - ptStart.Y - (int)2;
                                g.DrawRectangle(pen, ptStart.X, ptStart.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, brush, ptStart.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = e.X - ptStart.X - (int)2;
                                nHeight = ptStart.Y - e.Y - (int)2;
                                g.DrawRectangle(pen, ptStart.X, e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, brush, ptStart.X + 1, e.Y + 1);
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
                                g.DrawString(sSize, font, brush, e.X + 1, ptStart.Y + 1);
                            }
                            else
                            {
                                nWidth = ptStart.X - e.X - (int)2;
                                nHeight = ptStart.Y - e.Y - (int)2;
                                g.DrawRectangle(pen, e.X, e.Y, nWidth, nHeight);
                                string sSize = string.Format("Size:{0}x{1}", nWidth, nHeight);
                                g.DrawString(sSize, font, brush, e.X + 1, e.Y + 1);
                            }
                        }

                    }
                }

            }
        }

        private void ScreenCaptureForm_MouseUp(object sender, MouseEventArgs e)
        {
            b_leftbuttondown = false;
            Invalidate();
            Update();
            int nWidth = Math.Abs(e.X - ptStart.X - (int)2);
            int nHeight = Math.Abs(e.Y - ptStart.Y - (int)2);
            if (nWidth < 5 || nHeight < 5)
            {
                return;
            }
           
            screenbmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(screenbmp);
            Point pst=PointToScreen(ptStart);
            Point pend=PointToScreen(new Point(e.X, e.Y));
            if (e.X > ptStart.X)//rightside->
            {
                if (e.Y > ptStart.Y) //down direction
                {
                    
                    g.CopyFromScreen(pst.X, pst.Y, 0, 0, new System.Drawing.Size(nWidth, nHeight));
                    
                }
                else
                {
                    g.CopyFromScreen(pst.X, pend.Y, 0, 0, new System.Drawing.Size(nWidth, nHeight));
                }
            }
            else
            {
                if (e.Y > ptStart.Y) //down direction
                {
                    g.CopyFromScreen(pend.X, pst.Y, 0, 0, new System.Drawing.Size(nWidth, nHeight));
                }
                else
                {
                    g.CopyFromScreen(pend.X, pend.Y, 0, 0, new System.Drawing.Size(nWidth, nHeight));
                }
            }
            g.Dispose();
            //screenbmp.Save("C:\\123456.jpg");
           
            if(MainFrm!=null)
                MainFrm.Visible = true;
            
            this.Close();
        }

        private void ScreenCaptureForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Escape)
            {
                b_leftbuttondown = false;
                if (MainFrm != null)
                    MainFrm.Visible = true;

                this.Close();
            }
            
        }
    }
}
