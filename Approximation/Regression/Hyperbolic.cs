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
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = "Hyperbolic";


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
                sum += Math.Pow((y[i] - a - b / x[i]), 2);
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
            double yx = a + b / X;
            return yx;
        }

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
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
