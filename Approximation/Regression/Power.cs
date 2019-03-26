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
            double part1 = (1f / x.Count) * sumLn(y);
            double part2 = (b / x.Count) * sumLn(x);
            double a = Math.Exp(part1 - part2);
            return a;
        }

        private double getB()
        {
            double part1 = (x.Count * sumLn(x, y)) - (sumLn(x) * sumLn(y));
            double part2 = (x.Count * sumPowLn(x)) - Math.Pow(sumLn(x),2);
            double b = (part1 / part2);
            return b;
        }

        private double sumPowLn(List<double> x)
        {
            double sum = 0;
            for(int i = 0; i<x.Count;i++)
            {
                sum += Math.Pow(Math.Log(x[i]),2);
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

        private double sumLn(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += (Math.Log(x[i]) * Math.Log(y[i]));
            }
            return sum;
        }
    }
}
