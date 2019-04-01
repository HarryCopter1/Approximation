using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    public class Power : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Power(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            function = (z) => a * Math.Pow(z, b);
        }

        
        private double getA()
        {
            double part1 = (1f / x.Count) * Funcs.sumLn(y);
            double part2 = (b / x.Count) * Funcs.sumLn(x);
            double a = Math.Exp(part1 - part2);
            return a;
        }

        private double getB()
        {
            double part1 = (x.Count * Funcs.sumLn(x, y)) - (Funcs.sumLn(x) * Funcs.sumLn(y));
            double part2 = (x.Count * Funcs.sumPowLn(x,2)) - Math.Pow(Funcs.sumLn(x),2);
            double b = (part1 / part2);
            return b;
        }

    }
}
