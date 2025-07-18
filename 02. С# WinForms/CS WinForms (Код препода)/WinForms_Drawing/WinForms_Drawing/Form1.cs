using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WinForms_Drawing
{
    public partial class Form1 : Form
    {
        Point old = new Point();
        bool flag = false;
        ArrayList all = new ArrayList();
        ArrayList current = new ArrayList();
        Color basecolor=Color.Blue;

        public Form1()
        {
            InitializeComponent();
            old.X = 0;
            old.Y = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(basecolor);

            // закрашенный прямоугольник
            //gr.FillRectangle(brush1, 40, 40, 300, 200);

            // незакрашенный прямоугольник
            //gr.DrawRectangle(Pens.Violet, 45, 45, 300, 200);

            // закрашенный сектор овала
            //gr.FillPie(Brushes.DarkBlue, 45, 45, 300, 200, 30, 90);

            // незакрашенный сектор овала
            //gr.DrawPie(Pens.Black, 45, 45, 300, 200, -30, 36);
            
            // закрашенный полигон
            gr.FillPolygon(brush1, new Point[] {new Point(10,20), new Point(100,20), new Point(100,100)});
            
            gr.Dispose();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.ShowDialog();
            basecolor = dlg.Color;
        }

        private void pieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            // создание узорчатой кисти
            Brush brush1 = new HatchBrush(HatchStyle.DiagonalBrick, basecolor, Color.Orange);

            gr.FillPie(brush1, 45, 45, 300, 200, -90, 90);

            // смещение узора в узорчатой кисти
            gr.RenderingOrigin = new Point(0, 20);

            gr.FillPie(brush1, 45, 45, 300, 200, -30, 60);

            gr.Dispose();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1 = new Bitmap("num1.png");
            Graphics gr = CreateGraphics();

            // текстурная кисть
            TextureBrush brush1 = new TextureBrush(image1, WrapMode.TileFlipXY);

            // афинное преобразование текстурной кисти (поворот на 45 градусов)
            brush1.RotateTransform(45);

            // масштабирование по X, Y
            brush1.ScaleTransform(1.5f, 1.25f);

            // смещение по X, Y
            brush1.TranslateTransform(150, 150);

            // закрашенный эллипс, вписанный в заданный координатами прямоугольник
            gr.FillEllipse(brush1, 50, 50, 400, 300);

            gr.Dispose();
        }

        private void linearGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            // создание градиентной кисти
            //LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(0, 0, 200, 200), Color.Blue, Color.Red, LinearGradientMode.Vertical);
            //LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(0, 0, 50, 50), Color.Blue, Color.Red, 25f);
            LinearGradientBrush brush1 = new LinearGradientBrush(new Point(40, 40), new Point(340, 240), Color.Brown, Color.Pink);
            gr.FillRectangle(brush1, 40, 40, 300, 200);
            gr.Dispose();
        }

        private void pathGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            // массив точек для контура градиента
            Point[] points = { new Point(40, 40), new Point(80, 280), new Point(200, 80) };

            // контурная градиентная кисть
            PathGradientBrush brush1 = new PathGradientBrush(points, WrapMode.TileFlipXY);

            // задание цветов плавного перехода
            brush1.SurroundColors = new Color[] {Color.Red, Color.Green, Color.Blue};

            // задание цвета по центру контура
            brush1.CenterColor = Color.White;

            // смещение центральной точки градиента
            brush1.CenterPoint = new PointF(100, 80);

            // поворот кисти
            brush1.RotateTransform(30);

            // применение кисти
            gr.FillRectangle(brush1, 40, 40, 400, 300);

            gr.Dispose();
        }

        private void createPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // класс для работы с контурами, задающими форму объектов
            GraphicsPath path1 = new GraphicsPath();

            // задание контура примитивами
            /*path1.StartFigure();
            path1.AddLine(40, 40, 100, 100);
            path1.AddLine(100, 100, 300, 50);
            path1.CloseFigure();*/

            path1.AddLines(new Point[] {new Point(10, 10), new Point(300, 300), new Point(200, 100)});

            Graphics gr = CreateGraphics();

            Pen pen = new Pen(ForeColor, 3);
            Brush brush1 = new SolidBrush(basecolor);

            // рисование контура без закрашивания
            gr.DrawPath(pen, path1);

            // рисование закрашенного контура
            gr.FillPath(brush1,path1);

            gr.Dispose();
        }

        private void addStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphicsPath path1 = new GraphicsPath();

            // создание контура на основе строки
            path1.AddString("Hello", new FontFamily("Arial"), (int)FontStyle.Italic, 100, new Point(30, 30), null);
            
            // афинные преобразования контура
            Matrix trans = new Matrix();
            trans.Scale(1.5f, 2);

            // поворот относительно заданной точки
            trans.RotateAt(45, new PointF(100, 100));
            path1.Transform(trans);

            Pen pen2 = new Pen(ForeColor, 10);

            // расширение (оконтуривание контура)
            path1.Widen(pen2);
      
            Graphics gr = CreateGraphics();

            Pen pen = new Pen(ForeColor, 1);
            Brush brush1 = new SolidBrush(basecolor);
            
            gr.FillPath(brush1, path1);
            //gr.DrawPath(pen, path1);

            gr.Dispose();
        }

        private void flattenPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphicsPath path1 = new GraphicsPath();
            path1.AddString("Hello", new FontFamily("Arial"), 3, 100, new Point(30, 30), null);

            Matrix trans1 = new Matrix();
            trans1.Scale(1.5f, 1.5f);
            path1.Transform(trans1);

            // упрощение отображения контура
            path1.Flatten(trans1, 30f);

            Graphics gr = CreateGraphics();

            Pen pen = new Pen(ForeColor, 1);
            Brush brush1 = new SolidBrush(basecolor);

            //gr.FillPath(brush1, path1);
            gr.DrawPath(pen, path1);

            gr.Dispose();
        }

        private void setClipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            // загрузка первой картинки
            Image image1 = new Bitmap("pic1.jpg");

            // отрисовка первой картинки
            gr.DrawImage(image1, new Point(0, 0));

            GraphicsPath path1 = new GraphicsPath();
            //path1.AddLine(20,220,120,80);
            //path1.AddLine(120, 80, 220, 220);
            //path1.AddLine(220, 220, 120, 360);

            path1.AddEllipse(new Rectangle(40, 40, 300, 200));

            Matrix m = new Matrix();
            m.RotateAt(30, new PointF(200, 200));
            m.Scale(1.5f, 1.5f);
            m.Translate(100f, 100f);
            path1.Transform(m);

            GraphicsPath path2 = new GraphicsPath();
            path2.AddRectangle(new Rectangle(90, 190, 300, 250));
            
            // выключить области отсечения
            gr.ResetClip();

            // задать новую область отсечения
            gr.SetClip(path1);

            // скомбинировать области отсечения
            gr.SetClip(path2, CombineMode.Xor);     

            // загрузка второй картинки
            Image image2 = new Bitmap("pic2.jpg");
            //gr.ScaleTransform(2,2);

            // отображение второй картинки
            gr.DrawImage(image2, new Point(0, 0));

            gr.Dispose();
        }

        private void graphicsFromImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создание пустого изображения с размерами
            Image img1 = new Bitmap(400, 300);

            // создание объекта для рисования на картинке
            Graphics imgr = Graphics.FromImage(img1);

            // создание объекта для рисования в окне
            Graphics gr = CreateGraphics();

            Pen pen1 = new Pen(Color.Red, 3);
            imgr.Clear(Color.Blue);
            imgr.DrawRectangle(pen1, new Rectangle(10, 10, 100, 80));
            
            gr.DrawImage(img1, new Point(100, 100));

            /*SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                img1.Save(dlg.FileName, ImageFormat.Jpeg);
            }*/

            gr.Dispose();
            imgr.Dispose();
        }

        private void imageAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Image image1 = new Bitmap("nis1.jpg");
            Image image1 = new Bitmap("num1.png");
            Graphics gr = CreateGraphics();

            // модификация каналов изображения
            ColorMatrix myColorMatrix = new ColorMatrix();
            myColorMatrix.Matrix00 = 0.1f;  // канал R
            myColorMatrix.Matrix11 = 1.0f;  // канал G
            myColorMatrix.Matrix22 = 0.1f;  // канал B
            myColorMatrix.Matrix33 = 1f;    // прозрачность
            myColorMatrix.Matrix44 = 1.00f;

            // свойства (атрибуты) картинки
            ImageAttributes attr = new ImageAttributes();

            // применить матрицу каналов цвета
            //attr.SetColorMatrix(myColorMatrix);

            // удаление красных оотенков с изображения
            //attr.SetColorKey(Color.FromArgb(0, 0, 0), Color.FromArgb(255, 50, 50));

            // удаление белых оотенков с изображения
            attr.SetColorKey(Color.FromArgb(253, 253, 253), Color.FromArgb(255, 255, 255));

            gr.DrawImage(image1, new Rectangle(40, 40, 300, 207), 0, 0, 300, 207, GraphicsUnit.Pixel, attr);
            gr.Dispose();
        }

        private void imageFragmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1 = new Bitmap("num1.png");
            Graphics gr = CreateGraphics();

            // отображение всего изображения
            //gr.DrawImage(image1, new Rectangle(40, 40, 50,200));

            // отображение части изображения
            gr.DrawImage(image1, new Rectangle(30, 30, 300, 300), new Rectangle(0, 0, image1.Width / 2, image1.Height / 2), GraphicsUnit.Pixel);
            
            gr.Dispose();
        }

        private void colorMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img1 = new Bitmap(400, 300);
            Graphics imgr = Graphics.FromImage(img1);
            Graphics gr = CreateGraphics();

            Pen pen1 = new Pen(Color.Red, 3);
            imgr.Clear(Color.Blue);
            imgr.DrawRectangle(pen1, new Rectangle(10, 10, 100, 80));

            // свойства картинки
            ImageAttributes attr = new ImageAttributes();

            // матрица замены цветоа
            ColorMap[] myColorMap = new ColorMap[2];
            myColorMap[0] = new ColorMap();
            myColorMap[0].OldColor = Color.Red;
            myColorMap[0].NewColor = Color.Green;
            myColorMap[1] = new ColorMap();
            myColorMap[1].OldColor = Color.Blue;
            myColorMap[1].NewColor = Color.Beige;
            attr.SetRemapTable(myColorMap);
            
            //gr.DrawImage(img1, new Point(40, 40));

            // отображение картинки с заменой цветов
            gr.DrawImage(img1, new Rectangle(40, 40, 400, 300), 0, 0, 400, 300, GraphicsUnit.Pixel, attr);

            gr.Dispose();
            imgr.Dispose();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1 = new Bitmap("num1.png");

            Graphics gr = CreateGraphics();
            gr.DrawImage(image1, new Rectangle(40, 40, 300,200));
            gr.Dispose();

            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                image1.Save(dlg.FileName, ImageFormat.Jpeg);
            }
        }

        private void createBrushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            Point[] a = { new Point(40, 40), new Point(80, 200), new Point(200, 80), new Point(10, 20), new Point(20, 50) };

            GraphicsPath path1 = new GraphicsPath();
            path1.StartFigure();
            path1.AddPolygon(a);
            path1.CloseFigure();

            // создание градиентной кисти на основе контура
            PathGradientBrush brush1 = new PathGradientBrush(path1);
            brush1.WrapMode = WrapMode.Tile;
            brush1.SurroundColors = new Color[] { Color.Red, Color.Green, Color.Blue };
            brush1.CenterColor = Color.White;
            brush1.CenterPoint = new PointF(100, 80);
            brush1.RotateTransform(30);
            gr.FillRectangle(brush1, 40, 40, 400, 300);
            gr.Dispose();
        }

        private void createRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создание контура
            GraphicsPath path1 = new GraphicsPath();
            path1.StartFigure();
            path1.AddLine(40, 40, 100, 100);
            path1.AddLine(100, 100, 300, 50);
            path1.CloseFigure();

            // создание региона на основе контура
            Region reg1 = new Region(path1);

            Graphics gr = CreateGraphics();

            // отображение региона
            gr.FillRegion(Brushes.Blue, reg1);
            gr.Dispose();
        }

        private void unionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphicsPath path1 = new GraphicsPath();
            path1.StartFigure();
            path1.AddLine(70, 70, 130, 130);
            path1.AddLine(130, 130, 300, 50);
            path1.CloseFigure();

            Region reg1 = new Region(path1);
            //reg1.Union(new Rectangle(100, 80, 150, 100));
            reg1.Xor(new Rectangle(100, 80, 150, 100));
            //reg1.Intersect(new Rectangle(100, 80, 150, 100));
            //reg1.Exclude(new Rectangle(100, 80, 150, 100));
            //reg1.Complement(new Rectangle(100, 80, 150, 100));

            Matrix matrix1 = new Matrix();
            matrix1.RotateAt(45, new Point(100, 50));
            reg1.Transform(matrix1);

            reg1.Translate(20, 20);
                                                
            Graphics gr = CreateGraphics();
            gr.FillRegion(Brushes.Blue, reg1);

            // получить границы региона
            RectangleF rect = reg1.GetBounds(gr);
            gr.DrawRectangle(Pens.DarkRed, rect.Left, rect.Top, rect.Width, rect.Height);
            
            gr.Dispose();
        }

        private void isVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphicsPath path1 = new GraphicsPath();
            path1.StartFigure();
            path1.AddLine(70, 70, 130, 130);
            path1.AddLine(130, 130, 300, 50);
            path1.CloseFigure();

            Region reg1 = new Region(path1);

            // проверка точки на вхождение внутрь региона (контура)
            if (reg1.IsVisible(180, 80)) 
                MessageBox.Show("Yes");
            else MessageBox.Show("No");
        }

        private void lockBitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            Bitmap bmp = new Bitmap("nature.jpg");

            // получить размеры изображения
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            // получить "сырые данные" изображения
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            // получить указатель на начальный байт изображения
            IntPtr ptr = bmpData.Scan0;

            // получить размер изображения в байтах
            int bytes = bmpData.Stride * bmp.Height;

            // выделить память для массива байт изображения
            byte[] rgbValues = new byte[bytes];

            // скопировать память изображения в массив байт
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int len = rgbValues.Length;

            // усиление синего канала изображения на низком уровне
            for (int counter = 0; counter < len; counter += 3)
                rgbValues[counter] = 255;

            // осветление каждого канала изображения
            /*for (int counter = 0; counter < len; counter ++)
            {
                if (rgbValues[counter] + 50 <= 255) 
                    rgbValues[counter] += 50;
            }*/

            // вернуть изменённые байта назад в класс Bitmap
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // разблокировать изображение
            bmp.UnlockBits(bmpData);

            // отрисовать изображение в окне
            gr.DrawImage(bmp, 30,30);
        }

        private void setPixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // использование GetPixel и SetPixel (медленно)
            Graphics gr = CreateGraphics();

            Bitmap bmp = new Bitmap("nature.jpg");

            int w = bmp.Width;
            int h = bmp.Height;

            for (int i = 0; i < w; i++)
            {
                for (int k = 0; k < h; k++)
                {
                    Color cl = bmp.GetPixel(i, k);

                    int newR = cl.R + 150;
                    if(newR < 0) newR = 0;
                    if (newR > 255) newR = 255;

                    int newG = cl.G + 0;
                    if (newG < 0) newG = 0;
                    if (newG > 255) newG = 255;

                    int newB = cl.B + 0;
                    if (newB < 0) newB = 0;
                    if (newB > 255) newB = 255;

                    Color cl2 = Color.FromArgb(newR, newG, newB);
                    bmp.SetPixel(i, k, cl2);
                }
            }

            gr.DrawImage(bmp, 30, 30);
            //pictureBox1.Image = bmp;
        }

        private void setClip2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            Image image1 = new Bitmap("pic1.jpg");
            gr.DrawImage(image1, new Point(0, 0));

            Random r = new Random();

            // цикл по областям отсечения
            for (int i = 0; i < 10; i++)
            {
                GraphicsPath path1 = new GraphicsPath();
                path1.StartFigure();
                path1.AddLine(20, 220, 120, 80);
                path1.AddLine(120, 80, 220, 220);
                path1.AddLine(220, 220, 120, 360);
                path1.CloseFigure();

                int angle = r.Next(0,360);
                float scale = (float)r.NextDouble()+1;
                int translate = r.Next(100,512);

                // афинные преобразования областей отсечения
                Matrix m = new Matrix();
                m.RotateAt(angle, new PointF(700, 512));
                m.Scale(scale, scale);
                m.Translate(translate, translate);
                path1.Transform(m);

                // применение области отсечения
                gr.ResetClip();
                gr.SetClip(path1);

                Image image2 = new Bitmap("pic2.jpg");
                gr.DrawImage(image2, new Point(0, 0));
            }

            gr.Dispose();
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics gr = CreateGraphics();

            Bitmap bmp = new Bitmap("nature.jpg");

            int w = bmp.Width;
            int h = bmp.Height;

            for (int i = 0; i < w/2; i++)
            {
                for (int k = 0; k < h; k++)
                {
                    Color cl = bmp.GetPixel(i, k);
                    Color cl2 = bmp.GetPixel(w-i-1, h-k-1);
                    bmp.SetPixel(w - i - 1, h - k - 1, cl);
                    bmp.SetPixel(i, k, cl2);
                }
            }

            gr.DrawImage(bmp, 30, 30);
        }

    }
}