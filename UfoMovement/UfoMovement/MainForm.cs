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
        double x1 = 100, y1 = 100;
        double x2 = 900, y2 = 700;
        double epsilon = 2;
        double step = 1;
        int numMembers = 50;

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

        // двоичный поиск с разумной точностью,
        // так как простым перебором слишком большая нагрузка
        // из-за большого количества итераций
        // следовательно тяжело ответить на вопрос при каком эпсилон мы теряем попадание
        // график будет показывать, что при уменьшение на чуть-чуть эпсилона
        // мы будем выпадать из окружности
        private void buttonTest_Click(object sender, EventArgs e)
        {
            var lines = new List<string>();
            lines.Add("Epsilon\tFailAtN");
            var points = new List<(double epsilon, int members)>();

            double startX = x1;
            double startY = y1;
            double targetX = x2;
            double targetY = y2;
            double step = 1;

            for (int n = 1; n <= 5; n++)
            {
                double l = 0;
                double r = 100;
                double found = -1;

                for (int i = 0; i < 25; i++)
                {
                    double mid = (l + r) / 2.0;
                    var path = MoveLogic.FuncAngle(startX, startY, targetX, targetY, step, mid, n);

                    if (path.Count == 0)
                    {
                        l = mid;
                        continue;
                    }

                    var last = path.Last();
                    double dx = last.X - targetX;
                    double dy = last.Y - targetY;
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    if (dist <= mid)
                    {
                        found = mid;
                        r = mid;
                    }
                    else
                    {
                        l = mid;
                    }
                }

                if (found != -1)
                {
                    lines.Add($"{found:F6}\t{n}");
                    points.Add((found, n));
                }
            }

            string root = Path.GetFullPath(Path.Combine(Application.StartupPath, "..\\..\\..\\.."));
            string resultDir = Path.Combine(root, "result");
            Directory.CreateDirectory(resultDir);
            File.WriteAllLines(Path.Combine(resultDir, "data.txt"), lines);

            PlotDrawer.DrawPlot(points.OrderBy(p => p.epsilon).ToList(), 1, 5, 0, 100, Path.Combine(resultDir, "plot.png"));

            MessageBox.Show("Готово!");
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
