using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    class Exponential : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Exponential(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            function = (z) => Math.Pow(Math.E, a + b * z);
        }

        private double getB()
        {
            double part1 = x.Count * Funcs.sumLnX(y,x) - Funcs.sum(x) * Funcs.sumLn(y);
            double part2 = x.Count * Funcs.sumPow(x,2) - Math.Pow(Funcs.sum(x),2);
            double b = part1 / part2;
            return b;
        }

        private double getA()
        {
            double a = 1f / x.Count * Funcs.sumLn(y) - b / x.Count * Funcs.sum(x);
            return a;
        }
    }
}
