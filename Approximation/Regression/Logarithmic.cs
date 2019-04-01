using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    class Logarithmic : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Logarithmic(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            function = (z) => a + b * Math.Log(z);
        }

        private double getB()
        {
            double part1 = x.Count * Funcs.sumLnX(x, y) - Funcs.sumLn(x) * Funcs.sum(y);
            double part2 = x.Count * Funcs.sumPowLn(x, 2) - Math.Pow(Funcs.sumLn(x),2);
            double b = part1 / part2;
            return b;
        }

        private double getA()
        {
            double a = 1f / x.Count * Funcs.sum(y) - b / x.Count * Funcs.sumLn(x);
            return a;
        }

    }
}
