using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    class Hyperbolic : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Hyperbolic(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            function = (z) => a + b / z;
        }

        private double getB()
        {
            double part1 = x.Count * divYX(x, y) - div1x(x) * Funcs.sum(y);
            double part2 = x.Count * div1xPow(x) - Math.Pow(div1x(x), 2);
            double b = part1 / part2;
            return b;
        }

        private double getA()
        {
            double a = 1f / x.Count * Funcs.sum(y) - b / x.Count * div1x(x);
            return a;
        }

        private double divYX(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += y[i] / x[i];
            }
            return sum;
        }

        private double div1x(List<double> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += 1f / x[i];
            }
            return sum;
        }

        private double div1xPow(List<double> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += 1f / Math.Pow(x[i], 2);
            }
            return sum;
        }
    }
}
