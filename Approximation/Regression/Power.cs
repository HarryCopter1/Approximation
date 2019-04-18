using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Approximation.Properties;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    public class Power : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        public static ResourceManager rm = new ResourceManager("Approximation.Properties." + Settings.Default["Language"], Assembly.GetExecutingAssembly());

        public Power(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            b = getB();
            a = getA();
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = rm.GetString("Power");
            funcText = "a * x" + (char)0X1D47;

            function = (z) => a * Math.Pow(z, b);
        }

        
        public override double getA()
        {
            double part1 = (1f / x.Count) * Funcs.sumLn(y);
            double part2 = (b / x.Count) * Funcs.sumLn(x);
            double a = Math.Exp(part1 - part2);
            return a;
        }

        public override double getB()
        {
            double part1 = (x.Count * Funcs.sumLn(x, y)) - (Funcs.sumLn(x) * Funcs.sumLn(y));
            double part2 = (x.Count * Funcs.sumPowLn(x,2)) - Math.Pow(Funcs.sumLn(x),2);
            double b = (part1 / part2);
            return b;
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
                sum += Math.Pow((y[i] - a * Math.Pow(x[i], b)), 2);
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
            double yx = a * Math.Pow(X, b);
            return yx;
        }

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
        }


    }
}
