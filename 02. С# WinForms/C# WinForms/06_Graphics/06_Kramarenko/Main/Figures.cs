using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Main
{
    class Figures
    {
        List<Primitive> figures;

        public Figures()
        {
            figures = new List<Primitive>();
        }

        public void Add(Primitive fugure)
        {
            figures.Add(fugure);
        }

        public void Print(Graphics graphics)
        {
            foreach (Primitive figure in figures)
            {
                figure.Print(graphics);
            }
        }
    }

    abstract class Primitive
    {
        public abstract void Print(Graphics graphics);
    }

    class Pencil : Primitive
    {
        List<Line> lst = new List<Line>();
        public int Count 
        {
            get => lst.Count();
        }

        public Pencil()
        {
        }

        public Pencil(List<Line> lst)
        {
            this.lst = lst;
        }

        public void Add(Line line)
        {
            lst.Add(line);
        }

        public override void Print(Graphics graphics)
        {
            foreach (Line line in lst)
            {
                line.Print(graphics);
            }
        }
    }

    class Line : Primitive
    {
        int x1, y1, x2, y2;
        Pen pen;

        public Line()
        {
        }

        public Line(Pen pen, int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;

            this.pen = pen;
        }

        public void Fill(Pen pen, int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;

            this.pen = pen;
        }

        public override void Print(Graphics graphics)
        {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }
    }

    class Rectangle : Primitive
    {
        int x, y;
        int width, height;
        Brush brush;

        public Rectangle()
        {
        }
        public Rectangle(Brush brush, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;

            this.brush = brush;
        }

        public void Fill(Brush brush, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;

            this.brush = brush;
        }

        public override void Print(Graphics graphics)
        {
            graphics.FillRectangle(brush, x, y, width, height);
        }
    }

    class Ellipse : Primitive
    {
        int x, y;
        int width, height;
        Brush brush;

        public Ellipse()
        {
        }
        public Ellipse(Brush brush, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;

            this.brush = brush;
        }

        public void Fill(Brush brush, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;

            this.brush = brush;
        }

        public override void Print(Graphics graphics)
        {
            graphics.FillEllipse(brush, x, y, width, height);
        }
    }
}
