using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation
{
    class Linear
    {
        public double a;
        public double b;
        private double r;
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Linear(List<double> x, List<double> y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            a = getA();
            b = getB();
            r = getR();
        }

        public PlotModel setModel()
        {
            var model = new PlotModel { Title = "Line approximation", Subtitle = "Graph" };
                            


            Func<double, double> batFn1 = (z) => a * z + b;
            
            model.Series.Add(new FunctionSeries(batFn1, x.Min(), x.Max(), 0.0001));
            model.Series.Add(Dots.getScatter(x, y));
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Bottom, Minimum = x.Min() - 0.5, Maximum = x.Max() + 0.5 });
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Left, Minimum = y.Min() - 0.5, Maximum = y.Max() + 0.5 });
            return model;
        }


        private double getA()
        {
            double part1 = Func.sum(x) * Func.sum(y) - x.Count * Func.sum(x, y);
            double part2 = Math.Pow(Func.sum(x), 2) - x.Count * Func.sumPow(x);
            double a = part1 / part2;
            return a;
        }

        private double getB()
        {             
            double part1 = Func.sum(x) * Func.sum(x, y) - Func.sumPow(x) * Func.sum(y);
            double part2 = Math.Pow(Func.sum(x), 2) - x.Count * Func.sumPow(x);
            double b = part1 / part2; ;
            return b;
        }

        //Коефіцієнт детермінації
        public double getR()
        {
            double part1 = x.Count * Func.sum(x, y) - Func.sum(x) * Func.sum(y);
            double part2 = Math.Sqrt((x.Count * Func.sumPow(x) - Math.Pow(Func.sum(x), 2)) * 
                                     (x.Count * Func.sumPow(y) - Math.Pow(Func.sum(y), 2)));
            double r = part1 / part2; ;
            return r;
        }        

        public double getDet()
        {
            return Math.Pow(r, 2);
        }

        //Середня помилка апроксимації
        public double getRelativeError()
        {
            double part1 = 1f / x.Count;
            return part1 * sumEr() * 100f; 
        }

        //Сума
        private double sumEr()
        {
            double sum=0;

            for(int i = 0; i< x.Count;i++)
            {
                sum += Math.Abs((y[i] - yx(x[i])) / y[i]);
            }
            return sum;
        }

        //y*
        private double yx(double X)
        {
            double yx = a * X + b;            
            return yx;
        }
    }
}