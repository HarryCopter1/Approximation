using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation
{
    class Dots
    {
        public static ScatterSeries getScatter(List<double> x, List<double> y)
        {
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            for (int i = 0; i < x.Count; i++)
                scatterSeries.Points.Add(new ScatterPoint(x[i], y[i]));


            return scatterSeries;
        }
    }
}
