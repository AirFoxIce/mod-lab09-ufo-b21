using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoMovement
{
    public struct CalcPoint
    {
        public double X, Y;

        public CalcPoint (double x, double y)

        {
            X = x; Y = y;
        }
    }
    internal class MoveLogic
    {
        // логика для высчитывания точек через обычную функцию
        public static double LineY(double x, double k, double b)
        {
            return k * x + b;
        }

        public static List<CalcPoint> FuncLine(double x1, double y1, double x2, double y2) // выдаем лист точек (линию) от заданных точек начала и конца
        {
            var result = new List<CalcPoint>();

            // если вертикальная линия из-за k->бесконечность
            if (x1 == x2)
            {
                double y = y1;

                double step = 1;
                if (y2 < y1)
                    step = -1;

                while ((step > 0 && y <= y2) || (step < 0 && y >= y2))
                {
                    result.Add(new CalcPoint(x1, y));
                    y += step;
                }

                return result;
            }

            double k = (y2 - y1) / (x2 - x1);
            double b = y1 - k * x1;

            double x = x1;
            double stepX = 1;
            if (x2 < x1)
                stepX = -1;

            while ((stepX > 0 && x <= x2) || (stepX < 0 && x >= x2))
            {
                double y = LineY(x, k, b);
                result.Add(new CalcPoint(x, y));
                x += stepX;
            }

            return result;
        }

        // логика для высчитывания через угол

        public static List<CalcPoint> FuncAngle(double x1, double y1, double x2, double y2,
                                 double step, double errorRadius, int numMembers)
        {
            List<CalcPoint> list = new List<CalcPoint>();

            double a = x1;
            double b = y1;

            double dx = x2 - x1;
            double dy = y2 - y1;

            double angle = Math.Atan2(dy, dx);

            double stepX = step * MyMath.MyCos(angle, numMembers);
            double stepY = step * MyMath.MySin(angle, numMembers);

            int steps = 0;
            int maxSteps = 100000;

            double prevDist = double.MaxValue;

            while (true)
            {
                double distX = x2 - a;
                double distY = y2 - b;
                double dist = Math.Sqrt(distX * distX + distY * distY);

                if (dist <= errorRadius)
                {
                    list.Add(new CalcPoint(a, b));
                    break;
                }

                if (dist > prevDist)
                {
                    break;
                }

                list.Add(new CalcPoint(a, b));
                prevDist = dist;

                a += stepX;
                b += stepY;

                steps++;
                if (steps > maxSteps)
                {
                    break;
                }
            }

            return list;
        }
    }
}
