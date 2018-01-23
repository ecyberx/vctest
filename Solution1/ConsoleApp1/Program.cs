using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Reflection;

namespace ConsoleTestApp
{

    class MyApplication
    {
        //private Form form;

        Bitmap memoryImage;
  

        public void captureScreen()
        {
            Size s = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            memoryImage = new Bitmap(s.Width, s.Height);
            Graphics myGraphics = Graphics.FromImage(memoryImage);
            myGraphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), memoryImage.Size);
            myGraphics.Dispose();
        }

        public void captureScreens()
        {
            Size sz = new Size(0, 0);

            //すべてのディスプレイを列挙する
            foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens)
            {
                sz.Width += s.Bounds.Width;
                sz.Height = Math.Max(sz.Height, s.Bounds.Height);
            }

            memoryImage = new Bitmap(sz.Width, sz.Height);
            Graphics myGraphics = Graphics.FromImage(memoryImage);

            Point pt = new Point(0, 0);

            foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens)
            {
                myGraphics.CopyFromScreen(new Point(s.Bounds.X, s.Bounds.Y), pt, new Size(s.Bounds.Width,s.Bounds.Height));
                pt.X += s.Bounds.Width;
            }
            myGraphics.Dispose();


        }

        public void saveScreen(String str)
        {
            memoryImage.Save(str);
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Capture screen tool");

            String strFile;
            DateTime dt = DateTime.Now;

            if( args.Length > 0 )
            {
                strFile = args[0];
            } else
            {
                strFile = "clip_" + dt.ToString("yyyymmddhhmmss")+".bmp";
            }

            Console.WriteLine("screen captured: " + strFile);

            MyApplication app = new MyApplication();

            app.captureScreens();

            //app.captureScreen();
            app.saveScreen(strFile);

            Console.WriteLine("complete.");
        
           }
    }
}
