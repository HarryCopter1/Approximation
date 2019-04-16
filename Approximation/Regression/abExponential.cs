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
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = "abExponential";

            function = (z) => a * Math.Pow(b, z);
        }

        private double getA()
        {
            double part1 = Funcs.sumLn(y) / x.Count;
            double part2 = Math.Log(b) / x.Count * Funcs.sum(x);
            double a = Math.Exp(part1 - part2);
            return a;
        }

        private double getB()
        {
            double part1 = x.Count * Funcs.sumLnX(y, x) - Funcs.sum(x) * Funcs.sumLn(y);
            double part2 = x.Count * Funcs.sumPow(x,2) - Math.Pow(Funcs.sum(x), 2);
            double b = Math.Exp(part1 / part2);
            return b;
        }

        //Коефіцієнт Кореляції
        public double getR()
        {
            double r = Math.Sqrt(1f - (sumCor1(y) / sumCor2(y)));
            return r;
        }

        public double sumCor1(List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                //sum += Math.Pow((y[i] - yx(y[i])), 2);
                sum += Math.Pow((y[i] - a * Math.Pow(b, x[i])), 2);
            }
            return sum;
        }

        public double sumCor2(List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow((y[i] - y_(y)), 2);
            }
            return sum;
        }

        //Коефіцієнт детермінації
        public double getDet()
        {
            return Math.Pow(r, 2);
        }

        //Середня помилка апроксимації
        public double getRelativeError()
        {
            double part1 = 1f / x.Count * sumEr() * 100f;
            return part1;
        }

        //Сума
        private double sumEr()
        {
            double sum = 0;

            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Abs((y[i] - yx(x[i])) / y[i]);
            }
            return sum;
        }

        //y*
        private double yx(double X)
        {
            //double yx = a * Math.Pow(X,2) + b * X + c;
            double yx = a * Math.Pow(b,X);
            return yx;
        }

        //y_
        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
        }

    }
}
