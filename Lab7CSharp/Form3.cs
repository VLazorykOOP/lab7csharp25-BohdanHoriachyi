using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public class Form3 : Form
    {
        private PictureBox pictureBox;
        private Button btnDraw;
        private Random rnd = new Random();

        
        private Figure[] figures;

        public Form3()
        {
            this.Text = "Завдання 3: Малювання фігур";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            pictureBox = new PictureBox()
            {
                Location = new Point(10, 10),
                Size = new Size(560, 350),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            btnDraw = new Button()
            {
                Text = "Намалювати",
                Location = new Point(10, 370),
                Size = new Size(100, 30)
            };
            btnDraw.Click += BtnDraw_Click;

            this.Controls.Add(pictureBox);
            this.Controls.Add(btnDraw);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            int n = 20; 

            figures = new Figure[n];
            int w = pictureBox.Width;
            int h = pictureBox.Height;

            for (int i = 0; i < n; i++)
            {
                int type = rnd.Next(4); // 0 - круг, 1 - сектор, 2 - прямокутник, 3 - зірка
                int x = rnd.Next(w);
                int y = rnd.Next(h);
                int size = rnd.Next(20, 70);
                Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                switch (type)
                {
                    case 0:
                        figures[i] = new Circle(new Point(x, y), size, color);
                        break;
                    case 1:
                        figures[i] = new Sector(new Point(x, y), size, color, 45, 90);
                        break;
                    case 2:
                        figures[i] = new FilledRectangle(new Point(x, y), size, size, color);
                        break;
                    case 3:
                        figures[i] = new Star(new Point(x, y), size, color);
                        break;
                }
            }

            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                foreach (var f in figures)
                {
                    f.Draw(g);
                }
            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = bmp;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();


            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }

    
    public abstract class Figure
    {
        public Point Position { get; set; }
        public Color Color { get; set; }

        public Figure(Point position, Color color)
        {
            Position = position;
            Color = color;
        }

        public abstract void Draw(Graphics g);

        
        public virtual void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }

    public class Circle : Figure
    {
        public int Radius { get; set; }
        public Circle(Point position, int radius, Color color) : base(position, color)
        {
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Brush b = new SolidBrush(Color))
            {
                g.FillEllipse(b, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }

    public class Sector : Figure
    {
        public int Radius { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }

        public Sector(Point position, int radius, Color color, float startAngle, float sweepAngle) : base(position, color)
        {
            Radius = radius;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        public override void Draw(Graphics g)
        {
            using (Brush b = new SolidBrush(Color))
            {
                Rectangle rect = new Rectangle(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
                g.FillPie(b, rect, StartAngle, SweepAngle);
            }
        }
    }

    public class FilledRectangle : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public FilledRectangle(Point position, int width, int height, Color color) : base(position, color)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics g)
        {
            using (Brush b = new SolidBrush(Color))
            {
                g.FillRectangle(b, Position.X, Position.Y, Width, Height);
            }
        }
    }

    public class Star : Figure
    {
        public int Size { get; set; }

        public Star(Point position, int size, Color color) : base(position, color)
        {
            Size = size;
        }

        public override void Draw(Graphics g)
        {
            using (Brush b = new SolidBrush(Color))
            {
                PointF[] points = CreateStarPoints(Position, Size);
                g.FillPolygon(b, points);
            }
        }

        private PointF[] CreateStarPoints(Point center, int size)
        {
            PointF[] pts = new PointF[10];
            double angle = -Math.PI / 2;
            double delta = Math.PI / 5;
            for (int i = 0; i < 10; i++)
            {
                double r = (i % 2 == 0) ? size : size / 2.5;
                pts[i] = new PointF(
                    center.X + (float)(r * Math.Cos(angle)),
                    center.Y + (float)(r * Math.Sin(angle))
                );
                angle += delta;
            }
            return pts;
        }
    }
}
