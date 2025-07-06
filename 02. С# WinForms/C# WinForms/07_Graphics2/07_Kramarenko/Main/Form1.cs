using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Main
{
    public partial class Form1 : Form
    {
        bool isPen = true; // выбран ли инструмент "Карандаш"
        bool isMouseDown = false; // нажата ли левая кнопка мыши
        int x = 0, y = 0; // первая точка

        Figures figures = new Figures(); // все фигуры
        Primitive primitive = null; // текущая фигура

        Image image;

        SettingForm settingForm;

        public Form1()
        {
            InitializeComponent();
            image = new Bitmap(board.Width, board.Height);
            settingForm = new SettingForm();
        }



        private void changeColorButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < changeColorButton.Width
                && e.Y > 0 && e.Y < changeColorButton.Height)
            {
                ColorDialog colorDialog = new ColorDialog();

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    changeColorButton.BackColor = colorDialog.Color;
                }
            }
        }
        private void tools_TextChanged(object sender, EventArgs e)
        {
            if (tools.Text == tools.Items[0].ToString())
            {
                if (!isPen) isPen = true;
            }
            else
            {
                if (isPen) isPen = false;
            }
        }



        private void board_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPen && isMouseDown && x != e.X && y != e.Y)
            {
                if (image.Width < e.X || image.Height < e.Y)
                {
                    board_MouseUp(0, e);
                    return;
                }
                
                Graphics gr = board.CreateGraphics();
                Graphics graphics = Graphics.FromImage(image);
                Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                graphics.DrawLine(pen, x, y, e.X, e.Y);
                gr.DrawImage(image, new Point(0, 0));

                (primitive as Pencil).Add(new Line(pen, x, y, e.X, e.Y));
                x = e.X; y = e.Y;
                graphics.Dispose();
                gr.Dispose();
            }
        }
        private void board_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics gr = board.CreateGraphics();
            Graphics graphics = Graphics.FromImage(image);

            if (primitive is Pencil)
            {
                if ((primitive as Pencil).Count == 0)
                {
                    Brush brush = new SolidBrush(changeColorButton.BackColor);
                    int w = (int)pencilWidthNumericUpDown.Value;
                    graphics.FillEllipse(brush, x - w / 2, y - w / 2, w, w);
                    gr.DrawImage(image, new Point(0, 0));
                    primitive = new Ellipse(brush, x, y, w, w, true);
                }
                figures.Add(primitive);
            }
            else if (primitive is Line)
            {
                if (x != e.X || y != e.Y)
                {
                    Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    graphics.DrawLine(pen, x, y, e.X, e.Y);
                    gr.DrawImage(image, new Point(0, 0));
                    (primitive as Line).Fill(pen, x, y, e.X, e.Y);
                    figures.Add(primitive);
                }
            }
            else if (primitive is Rectangle)
            {
                if (x != e.X || y != e.Y)
                {
                    int x1 = e.X, y1 = e.Y;
                    if (x > x1)
                    {
                        int tmp = x;
                        x = x1;
                        x1 = tmp;
                    }
                    if (y > y1)
                    {
                        int tmp = y;
                        y = y1;
                        y1 = tmp;
                    }

                    bool fillFigure = false;
                    foreach (Control control in settingForm.Controls)
                    {
                        if(control.Name == "tablePanel")
                        {
                            foreach (CheckBox item in (control as Panel).Controls)
                            {
                                if (item.Name == "fillFigureCheckBox") fillFigure = item.Checked;
                            }
                        }
                    }

                    if (fillFigure)
                    {
                        Brush brush = new SolidBrush(changeColorButton.BackColor);
                        graphics.FillRectangle(brush, x, y, x1 - x, y1 - y);
                        (primitive as Rectangle).Fill(brush, x, y, x1 - x, y1 - y, fillFigure);
                    }
                    else
                    {
                        Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                        graphics.DrawRectangle(pen, x, y, x1 - x, y1 - y);
                        (primitive as Rectangle).Fill(pen, x, y, x1 - x, y1 - y, fillFigure);
                    }
                    
                    figures.Add(primitive);
                    gr.DrawImage(image, new Point(0, 0));
                }
            }
            else if (primitive is Ellipse)
            {
                if (x != e.X || y != e.Y)
                {
                    int x1 = e.X, y1 = e.Y;
                    if (x > x1)
                    {
                        int tmp = x;
                        x = x1;
                        x1 = tmp;
                    }
                    if (y > y1)
                    {
                        int tmp = y;
                        y = y1;
                        y1 = tmp;
                    }


                    bool fillFigure = false;
                    foreach (Control control in settingForm.Controls)
                    {
                        if (control.Name == "tablePanel")
                        {
                            foreach (CheckBox item in (control as Panel).Controls)
                            {
                                if (item.Name == "fillFigureCheckBox") fillFigure = item.Checked;
                            }
                        }
                    }

                    if (fillFigure)
                    {
                        Brush brush = new SolidBrush(changeColorButton.BackColor);
                        graphics.FillEllipse(brush, x, y, x1 - x, y1 - y);
                        (primitive as Ellipse).Fill(brush, x, y, x1 - x, y1 - y, fillFigure);
                    }
                    else
                    {
                        Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                        graphics.DrawEllipse(pen, x, y, x1 - x, y1 - y);
                        (primitive as Ellipse).Fill(pen, x, y, x1 - x, y1 - y, fillFigure);
                    }

                    figures.Add(primitive);
                    gr.DrawImage(image, new Point(0, 0));




                    //Brush brush = new SolidBrush(changeColorButton.BackColor);
                    //graphics.FillEllipse(brush, x, y, x1 - x, y1 - y);
                    //gr.DrawImage(image, new Point(0, 0));
                    //(primitive as Ellipse).Fill(brush, x, y, x1 - x, y1 - y);
                    //figures.Add(primitive);
                }
            }

            isMouseDown = false;
            graphics.Dispose();
            gr.Dispose();
        }
        private void board_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, new Point(0, 0));
            figures.Print(e.Graphics);
        }
        private void board_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (image.Width < e.X || image.Height < e.Y) return;

                switch (tools.Text)
                {
                    case "Карандаш": primitive = new Pencil(); break;
                    case "Линия": primitive = new Line(); break;
                    case "Прямоугольник": primitive = new Rectangle(); break;
                    case "Эллипс": primitive = new Ellipse(); break;
                    default: return;
                }
                isMouseDown = true;

                x = e.X; y = e.Y;
            }
        }



        private void openToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "BMP, JPG, JPEG, PNG|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = "";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Opening";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Graphics gr = board.CreateGraphics();
                gr.Clear(board.BackColor);

                Bitmap tempBitmap = new Bitmap(openFileDialog.FileName);

                int w = tempBitmap.Width, h = tempBitmap.Height;

                if(w > board.Width)
                {
                    h = (int)(h / ((double)w / board.Width));
                    w = board.Width;
                }
                if(h > board.Height)
                {
                    w = (int)(w / ((double)h / board.Height));
                    h = board.Height;
                }

                image = new Bitmap(Image.FromFile(openFileDialog.FileName), w, h);
                gr.DrawImage(image, new Point(0, 0));
                figures.Clear();

                gr.Dispose();
            }

            openFileDialog.Dispose();
        }
        private void clearToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics gr = board.CreateGraphics();
            Graphics graphics = Graphics.FromImage(image);

            image = new Bitmap(board.Width, board.Height);
            graphics.Clear(board.BackColor);
            gr.DrawImage(image, new Point(0, 0));

            figures.Clear();

            gr.Dispose();
            graphics.Dispose();
        }
        private void settingButton_MouseUp(object sender, MouseEventArgs e)
        {
            settingForm.ShowDialog();
        }
        private void saveToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "BMP|*.bmp|JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";
            saveFileDialog.FilterIndex = 4;
            saveFileDialog.FileName = "NewPicture";
            saveFileDialog.Title = "saving";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.AddExtension = true;

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imgFormat = null;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:         imgFormat = ImageFormat.Bmp;    break;
                    case 2: case 3: imgFormat = ImageFormat.Jpeg;   break;
                    case 4:         imgFormat = ImageFormat.Png;    break;
                    default: {
                            MessageBox.Show("313 стр. wrong filterIndex", "INFO", MessageBoxButtons.OK);
                            saveFileDialog.Dispose();
                            return;
                    }
                }

                image.Save(saveFileDialog.FileName, imgFormat);
            }

            saveFileDialog.Dispose();
        }
    }
}
