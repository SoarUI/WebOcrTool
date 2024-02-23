using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebPageOcr
{
    static class Program
    {
        //https://www.cnblogs.com/HouZhiHouJueBlogs/p/3951815.html
        public delegate Int32 CallBack(ref long a);
      
        [DllImport("kernel32")]
        private static extern Int32 SetUnhandledExceptionFilter(CallBack cb);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            CallBack mycall;
            mycall = new CallBack(MyExceptionfilter);
            SetUnhandledExceptionFilter(mycall);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WebPageOcrForm());
        }
        public static void makesuredir(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
            {
                return;
            }
            string checkpath = path.Replace("//", "/");
            checkpath = checkpath.Replace("\\", "/");
            int curpos = checkpath.IndexOf("/");
            int prepos = 0;
            string abspath = checkpath.Substring(0, curpos);
            while (curpos > 0)
            {
                abspath = checkpath.Substring(0, curpos);
                if (!System.IO.Directory.Exists(abspath))
                {
                    Directory.CreateDirectory(abspath);
                }
                prepos = curpos;
                curpos = checkpath.IndexOf("/", curpos + 1);
            }
            //if total is dir
            if (checkpath.IndexOf(".") != -1)
            {
                return;
            }
            Directory.CreateDirectory(path);
        }
        /*
         * EXCEPTION_EXECUTE_HANDLER == 1 表示我已经处理了异常,可以优雅地结束了
           EXCEPTION_CONTINUE_SEARCH == 0 表示我不处理,其他人来吧,于是windows调用默认的处理程序显示一个错误框,并结束
           EXCEPTION_CONTINUE_EXECUTION e== -1 表示错误已经被修复,请从异常发生处继续执行。
         */
        static Int32 MyExceptionfilter(ref long a)
        {
            return 0;
        }
    }
}
