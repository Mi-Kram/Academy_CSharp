using Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Main
{
    public partial class Form : System.Windows.Forms.Form
    {
        Random rand;
        public Form()
        {
            InitializeComponent();
            rand = new Random();

            pictureBox.Image = new Bitmap(Resources.blueSky);
            //pictureBox.Image = new Bitmap(1000, 700);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Graphics graphics = Graphics.FromImage(pictureBox.Image);

                int n = rand.Next(2, 6);
                for (int i = 0; i < n; i++)
                {
                    int w = rand.Next(20, 100);
                    int x = rand.Next(pictureBox.Image.Width - w), y = rand.Next(pictureBox.Image.Height - w);
                    int rotat = rand.Next(90);

                    Bitmap bitmap = new Bitmap(Resources.starsBackground, w, w);

                    //graphics.DrawImage(bitmap, x, y);

                    // =============================================================================

                    int tmp = (int)(w / 5); // отступ от края

                    double a = w * 0.6; // одна сторона
                    double b = w * 0.4; // две другие стороны
                    double p = (a + b + b) / 2; // переменная для формулы

                    int h = (int)((2 * Math.Sqrt(p * (p - a) * (p - b) * (p - b))) / a); // высота

                    // =============================================================================



                    GraphicsPath path1 = new GraphicsPath();
                    path1.AddPolygon(new Point[] { 
                    new Point(x+(w/2), y),
                    new Point(x+(int)(tmp*3.2), y+(tmp*2)),

                    new Point(x+w, y+(tmp*2)),
                    new Point(x+(tmp*3)+(tmp/2), y+(tmp*3)),


                    new Point(x+w-tmp, y+w),
                    new Point(x+(w/2), y+w-h),

                    new Point(x+tmp, y+w),
                    new Point(x+tmp+(tmp/2), y+(tmp*3)),

                    new Point(x, y+tmp+tmp),
                    new Point(x+(int)(tmp*1.8), y+(tmp*2))});

                    Matrix m = new Matrix();
                    m.RotateAt(rotat, new PointF(x+w/2, y+w/2));
                    path1.Transform(m);

                    graphics.ResetClip();
                    graphics.SetClip(path1);


                    #region FAIL with rectangle rotation

                    //GraphicsPath path1 = new GraphicsPath();
                    //path1.AddRectangle(new Rectangle(x, y, w, w));

                    //Matrix m = new Matrix();
                    //m.RotateAt(45, new PointF(x+w/2,y+w/2));
                    //path1.Transform(m);


                    //m = new Matrix();
                    //m.RotateAt(110, new PointF(w-tmp-tmp, h));
                    //m.RotateAt(180, new PointF(w / 2, h));


                    //GraphicsPath path2 = new GraphicsPath();
                    //path2.AddPolygon(new Point[] { 
                    //new Point(x + tmp, y + w),
                    //new Point(x + w - tmp, y + w),
                    //new Point(w / 2, y + w - h)});

                    ////path2.AddLine(x + w - tmp, y + w, w / 2, y + w - h);

                    ////GraphicsPath path3 = (GraphicsPath)path2.Clone();
                    //////path3.Transform(m);

                    ////GraphicsPath path4 = (GraphicsPath)path2.Clone();
                    //////path4.Transform(m);

                    ////GraphicsPath path5 = (GraphicsPath)path2.Clone();
                    //////path5.Transform(m);

                    ////GraphicsPath path6 = (GraphicsPath)path2.Clone();
                    //////path6.Transform(m);


                    //// выключить области отсечения
                    //graphics.ResetClip();

                    //// задать новую область отсечения
                    //graphics.SetClip(path1);

                    //// скомбинировать области отсечения
                    //graphics.SetClip(path2, CombineMode.Xor);
                    ////graphics.SetClip(path3, CombineMode.Xor);
                    ////graphics.SetClip(path4, CombineMode.Xor);
                    ////graphics.SetClip(path5, CombineMode.Xor);
                    ////graphics.SetClip(path6, CombineMode.Xor);

                    //// загрузка второй картинки
                    ////Image image2 = new Bitmap(w,w);
                    ////gr.ScaleTransform(2,2);
                    #endregion


                    // отображение второй картинки
                    graphics.DrawImage(bitmap, x,y);
                }

                pictureBox.Image = pictureBox.Image;
                graphics.Dispose();
            }
            else if(e.KeyCode == Keys.Back)
            {
                pictureBox.Image = new Bitmap(Resources.blueSky);
            }
        }
    }
}
