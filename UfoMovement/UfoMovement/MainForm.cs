using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UfoMovement;

namespace UfoMovement
{
    public partial class MainForm : Form
    {
        List<CalcPoint> points = new List<CalcPoint>();
        int index = 0;
        Bitmap bmp;
        Graphics g;
        Timer timer = new Timer();
        double x1 = 23, y1 = 63;
        double x2 = 498, y2 = 359;
        double epsilon = 10;
        double step = 15;
        int numMembers = 3;

        public MainForm()
        {
            InitializeComponent();
        }

        private void DrawStartAndTarget(double epsilon)
        {
            g.FillEllipse(Brushes.Blue, (float)(x1 - 4), (float)(y1 - 4), 8, 8);

            g.FillEllipse(Brushes.Red, (float)(x2 - 4), (float)(y2 - 4), 8, 8);

            Pen penEps = new Pen(Color.Red, 1);
            g.DrawEllipse(penEps, (float)(x2 - epsilon), (float)(y2 - epsilon),
                                     (float)(2 * epsilon), (float)(2 * epsilon));
        }

        private void CountError(CalcPoint p)
        {
            double dx = p.X - x2;
            double dy = p.Y - y2;
            double error = Math.Sqrt(dx * dx + dy * dy);

            labelError.Text = "Ошибка до цели: " + error.ToString("0.00");
        }

        private void buttonLineMove_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(canvasBox.Width, canvasBox.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double epsilon = 30;

            DrawStartAndTarget(epsilon);

            points = MoveLogic.FuncLine(x1, y1, x2, y2);
            index = 0;

            timer.Interval = 1;
            timer.Tick += DrawNextPoint;
            timer.Start();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();
            lines.Add("Members \tEpsilon \tError");

            double startX = x1;
            double startY = y1;
            double targetX = x2;
            double targetY = y2;
            double step = 15;

            for (int members = 1; members <= 20; members++)
            {
                double lastBadEps = 0;
                double lastBadErr = 0;

                for (int eps = 2; eps <= 40; eps += 2)
                {
                    double epsilon = eps;

                    var list = MoveLogic.FuncAngle(startX, startY, targetX, targetY, step, epsilon, members);

                    if (list.Count == 0)
                        continue;

                    var last = list[list.Count - 1];
                    double dx = last.X - targetX;
                    double dy = last.Y - targetY;
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    if (dist > epsilon)
                    {
                        lastBadEps = epsilon;
                        lastBadErr = dist;
                    }
                    else
                    {
                        if (lastBadEps > 0)
                            lines.Add(members + "\t\t\t" + lastBadEps + "\t\t\t" + lastBadErr.ToString("0.00"));
                        break;
                    }
                }
            }

            Directory.CreateDirectory("../../../result");
            File.WriteAllLines("../../../result/data.txt", lines);

            MessageBox.Show("Файл result/data.txt обновлён. Готово!");
        }



        private void DrawNextPoint(object sender, EventArgs e)
        {
            if (index < points.Count)
            {
                var p = points[index];
                g.FillEllipse(Brushes.Black, (float)p.X, (float)p.Y, 3, 3);
                canvasBox.Image = bmp;

                CountError(p);

                index++;
            }
            else
            {
                timer.Stop();
                labelError.Text += " (движение завершено)";
            }
        }

        private void buttonAngelMove_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(canvasBox.Width, canvasBox.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            DrawStartAndTarget(epsilon);

            points = MoveLogic.FuncAngle(x1, y1, x2, y2, step, epsilon, numMembers);
            index = 0;

            timer.Interval = 5;
            timer.Tick += DrawNextPoint;
            timer.Start();
        }
    }
}
