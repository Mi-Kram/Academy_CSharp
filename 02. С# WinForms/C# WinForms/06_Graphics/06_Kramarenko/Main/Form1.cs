using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Main
{
    public partial class Form1 : Form
    {
        bool isPen = true; // выбран ли инструмент "Карандаш"
        bool isMouseDown = false; // нажата ли левая кнопка мыши
        int x = 0, y = 0; // первая точка

        Figures Figures = new Figures(); // все фигуры
        Primitive primitive = null; // текущая фигура

        public Form1()
        {
            InitializeComponent();
        }

        private void changeColorButton_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && e.X > 0 && e.X < changeColorButton.Width 
                &&  e.Y > 0 && e.Y < changeColorButton.Height)
            {
                ColorDialog colorDialog = new ColorDialog();

                if(colorDialog.ShowDialog() == DialogResult.OK)
                {
                    changeColorButton.BackColor = colorDialog.Color;
                }
            }
        }

        private void tools_TextChanged(object sender, EventArgs e)
        {
            if(tools.Text == tools.Items[0].ToString())
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
                Graphics graphics = board.CreateGraphics();
                Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                graphics.DrawLine(pen, x, y, e.X, e.Y);

                (primitive as Pencil).Add(new Line(pen, x, y, e.X, e.Y));
                x = e.X; y = e.Y;
                graphics.Dispose();
            }
        }

        private void board_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics graphics = board.CreateGraphics();

            if (primitive is Pencil)
            {
                if((primitive as Pencil).Count == 0)
                {
                    Brush brush = new SolidBrush(changeColorButton.BackColor);
                    int w = (int)pencilWidthNumericUpDown.Value;
                    graphics.FillEllipse(brush, x - w / 2, y - w / 2, w, w);
                    primitive = new Ellipse(brush, x, y, w, w);
                }
                Figures.Add(primitive);
            }
            else if (primitive is Line)
            {
                if (x != e.X || y != e.Y)
                {
                    Pen pen = new Pen(changeColorButton.BackColor, (int)pencilWidthNumericUpDown.Value);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    graphics.DrawLine(pen, x, y, e.X, e.Y);
                    (primitive as Line).Fill(pen, x, y, e.X, e.Y);
                    Figures.Add(primitive);
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
                    Brush brush = new SolidBrush(changeColorButton.BackColor);
                    graphics.FillRectangle(brush, x, y, x1 - x, y1 - y);
                    (primitive as Rectangle).Fill(brush, x, y, x1 - x, y1 - y);
                    Figures.Add(primitive);
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
                    Brush brush = new SolidBrush(changeColorButton.BackColor);
                    graphics.FillEllipse(brush, x, y, x1 - x, y1 - y);
                    (primitive as Ellipse).Fill(brush, x, y, x1 - x, y1 - y);
                    Figures.Add(primitive);
                }
            }

            isMouseDown = false;
            graphics.Dispose();
        }

        private void board_Paint(object sender, PaintEventArgs e)
        {
            Figures.Print(e.Graphics);
        }

        private void board_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                switch (tools.Text)
                {
                    case "Карандаш":      primitive = new Pencil();     break;
                    case "Линия":         primitive = new Line();       break;
                    case "Прямоугольник": primitive = new Rectangle();  break;
                    case "Эллипс":        primitive = new Ellipse();    break;
                    default: return;
                }
                isMouseDown = true;

                x = e.X; y = e.Y;
            }
        }
    }
}
