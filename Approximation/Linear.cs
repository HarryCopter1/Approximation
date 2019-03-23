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
        public static PlotModel setModel(List<int> x, List<int> y, double a, double b)
        {
            var model = new PlotModel { Title = "Line approximation", Subtitle = "Graph" };

            
            Func<double, double> batFn1 = (z) => a * z + b;
            
            model.Series.Add(new FunctionSeries(batFn1, x.Min(), x.Max(), 0.0001));
            model.Series.Add(Dots.getScatter(x, y));
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Bottom, Minimum = x.Min() - 0.5, Maximum = x.Max() + 0.5 });
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Left, Minimum = y.Min() - 0.5, Maximum = y.Max() + 0.5 });
            return model;
        }

    }
}