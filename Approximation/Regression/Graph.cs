using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Approximation.Regression
{
    public class Graph
    {
        protected static ResourceManager rm;
        protected double a;
        protected double b;
        protected double r;
        protected double det;
        protected double err;
        protected string name;
        protected string funcText;
        public Func<double, double> function;
        protected List<double> x = new List<double>();
        protected List<double> y = new List<double>();

        static public List<Graph> graphList = new List<Graph>();


        public Graph(List<double> x, List<double> y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            rm = MainForm.rm;
        }

        public PlotModel getModel(List<FunctionSeries> functionList)
        {
            var model = new PlotModel { Title = "Approximation", Subtitle = "Graph" };


            foreach (FunctionSeries ser in functionList)
            {
                model.Series.Add(ser);
            }

            model.Series.Add(Dots.getScatter(x, y));
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Bottom});
            model.Axes.Add(new LinearAxis { IsPanEnabled = false, IsZoomEnabled = false, Position = AxisPosition.Left });
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

        //
        public virtual double getC()
        {
            return 1 / 0F;
        }

        public virtual double getD()
        {
            return 1 / 0F;
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