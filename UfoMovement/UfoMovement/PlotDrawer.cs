using System;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;

namespace UfoMovement
{
    public class PlotDrawer
    {
        public static void DrawPlot(List<(double epsilon, int members)> data, int memMin, int memMax, int epsMin, int epsMax, string path)
        {
            double[] xs = data.Select(p => p.epsilon).ToArray();
            double[] ys = data.Select(p => (double)p.members).ToArray();

            var plt = new ScottPlot.Plot();

            plt.Add.Scatter(xs, ys);

            plt.Title("Зависимость количества членов ряда от радиуса зоны");
            plt.XLabel("Радиус зоны (epsilon)");
            plt.YLabel("Кол-во членов ряда");

            plt.Axes.SetLimits(epsMin - 1, epsMax + 1, memMin - 1, memMax + 1);

            plt.SavePng(path, 800, 600);
        }
    }
}
