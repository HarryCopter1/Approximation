using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    public class Graph
    {
        public double a;
        public double b;
        public double c;
        public double d;
        public double r;
        public double err;
        public Func<double, double> function;
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Graph(List<double> x, List<double> y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
        }

        public PlotModel getModel()
        {
            var model = new PlotModel { Title = "Line approximation", Subtitle = "Graph" };

            Linear linear = new Linear(x, y);
            Power power = new Power(x, y);
            Quadratic quadratic = new Quadratic(x, y);
            abExponential expo = new abExponential(x, y);
            Cubic cubic = new Cubic(x, y);

                model.Series.Add(new FunctionSeries(expo.function, x.Min(), x.Max(), 0.0001, "Expo"));
                model.Series.Add(new FunctionSeries(linear.function, x.Min(), x.Max(), 0.0001, "linear"));
                model.Series.Add(new FunctionSeries(power.function, x.Min(), x.Max(), 0.0001, "power"));
                model.Series.Add(new FunctionSeries(quadratic.function, x.Min(), x.Max(), 0.0001, "quadratic"));
            model.Series.Add(new FunctionSeries(cubic.function, x.Min(), x.Max(), 0.0001, "cubic"));
            model.Series.Add(Dots.getScatter(x, y));
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Bottom, Minimum = x.Min() - 0.5, Maximum = x.Max() + 0.5 });
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Left, Minimum = y.Min() - 0.5, Maximum = y.Max() + 0.5 });
            return model;
        }

    }
}
