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
        protected double a;
        protected double b;
        protected double c;
        protected double d;
        protected double r;
        protected double det;
        protected double err;
        protected string name;
        protected string funcText;
        public Func<double, double> function;
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        static public List<Graph> graphList = new List<Graph>();


        public Graph(List<double> x, List<double> y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
        }

        public PlotModel getModel(List<FunctionSeries> functionList)
        {
            var model = new PlotModel { Title = "Approximation", Subtitle = "Graph" };


            foreach (FunctionSeries ser in functionList)
            {
                model.Series.Add(ser);
            }

            model.Series.Add(Dots.getScatter(x, y));
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Bottom, Minimum = x.Min() - 0.5, Maximum = x.Max() + 0.5 });
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Left, Minimum = y.Min() - 0.5, Maximum = y.Max() + 0.5 });
            return model;
        }

        public virtual double getA()
        {
            return a;
        }

        public virtual double getB()
        {
            return b;
        }

        public virtual double getC()
        {
            return c;
        }

        public virtual double getD()
        {
            return d;
        }

        //Коефіцієнт детермінації
        public virtual double getR()
        {
            return r;
        }

        //Коефіцієнт детермінації
        public virtual double getDet()
        {
            return det;
        }

        //Середня помилка апроксимації
        public virtual double getRelativeError()
        {
            return err;
        }

        public virtual string getName()
        {
            return name;
        }

        public virtual string getFuncText()
        {
            return funcText;
        }
    }
}