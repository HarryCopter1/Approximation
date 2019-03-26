using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    class abExponential : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public abExponential(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            function = (z) => a * Math.Pow(b, z);
        }

        private double getA()
        {
            double part1 = 1f / x.Count * sumLn(y);
            double part2 = Math.Log(b) / x.Count * Funcs.sum(x);
            double a = Math.Exp(part1 - part2);
            return a;
        }

        private double getB()
        {
            double part1 = x.Count * sumLn(x, y) - Funcs.sum(x) * sumLn(y);
            double part2 = x.Count * Funcs.sumPow(x) - Math.Pow(Funcs.sum(x), 2);
            double b = Math.Exp(part1 / part2);
            return b;
        }

        private double sumLn(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Log(y[i]) * x[i];
            }
            return sum;
        }


        private double sumLn(List<double> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Log(x[i]);
            }
            return sum;
        }
    }
}
