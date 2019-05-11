using System;
using System.Collections.Generic;
using System.Linq;

namespace Approximation.Regression
{
    class Exponential : Graph
    {

        public Exponential(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = rm.GetString("Exponential");
            funcText = "e" + (char)0x1D43 + (char)0x207A + (char)0x1D47 + (char)0x02E3;

            function = (z) => Math.Pow(Math.E, a + b * z);
        }

        public override double getB()
        {
            double part1 = x.Count * Funcs.sumLnX(y,x) - Funcs.sum(x) * Funcs.sumLn(y);
            double part2 = x.Count * Funcs.sumPow(x,2) - Math.Pow(Funcs.sum(x),2);
            double b = part1 / part2;
            return b;
        }

        public override double getA()
        {
            double a = 1f / x.Count * Funcs.sumLn(y) - b / x.Count * Funcs.sum(x);
            return a;
        }

        //Коефіцієнт Кореляції
        public override double getR()
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
                sum += Math.Pow((y[i] - Math.Pow(Math.E, a + b * x[i])), 2);
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
        public override double getDet()
        {
            return Math.Pow(r, 2);
        }

        //Середня помилка апроксимації
        public override double getRelativeError()
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
            double yx = Math.Pow(Math.E, a + b * X);
            return yx;
        }

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
        }

    }
}
