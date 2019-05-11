using System;
using System.Collections.Generic;
using System.Linq;

namespace Approximation.Regression
{
    class Cubic : Graph
    {

        double c;
        double d;


        public Cubic(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();
            name = rm.GetString("Cubic");

            Matrix test = new Matrix();

            test.AddLinearEquation(Funcs.sum(y),            Funcs.sumPow(x,3),      Funcs.sumPow(x,2),      Funcs.sum(x),           x.Count);
            test.AddLinearEquation(Funcs.sum(x,y),          Funcs.sumPow(x, 4),     Funcs.sumPow(x, 3),     Funcs.sumPow(x, 2),     Funcs.sum(x));
            test.AddLinearEquation(Funcs.sumPow(x,y,2),     Funcs.sumPow(x, 5),     Funcs.sumPow(x, 4),     Funcs.sumPow(x, 3),     Funcs.sumPow(x, 2));
            test.AddLinearEquation(Funcs.sumPow(x, y, 3),   Funcs.sumPow(x, 6),     Funcs.sumPow(x, 5),     Funcs.sumPow(x, 4),     Funcs.sumPow(x, 3));
            var result = test.Solve();

         
            
            a = result[0];
            b = result[1];
            c = result[2];
            d = result[3];
            
            funcText = "ax" + (char)0x00B3 + " + " +
                "bx" + (char)0x00B2 + " + " + "cx" + " + " + "d";

            r = getR();
            det = getDet();
            err = getRelativeError();

            function = (z) => a * Math.Pow(z,3)+ b*Math.Pow(z,2) + c * z + d;
        }

        public override double getC()
        {
            return c;
        }

        public override double getD()
        {
            return d;
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
                sum += Math.Pow((y[i] - a * Math.Pow(x[i], 3) - b * Math.Pow(x[i], 2) - c * x[i] - d), 2);
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

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
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
            double yx = a * Math.Pow(X, 3) + b * Math.Pow(X, 2) + c * X + d;
            return yx;
        }



        private double[,] getMatrix()
        {
            double[,] mat = new double[3, 4];

            mat[0, 0] = Funcs.sumPow(x, 2);
            mat[0, 1] = Funcs.sum(x);
            mat[0, 2] = x.Count;
            mat[0, 3] = Funcs.sum(y);

            mat[1, 0] = Funcs.sumPow(x, 3);
            mat[1, 1] = Funcs.sumPow(x, 2);
            mat[1, 2] = Funcs.sum(x);
            mat[1, 3] = Funcs.sum(x, y);

            mat[2, 0] = Funcs.sumPow(x, 4);
            mat[2, 1] = Funcs.sumPow(x, 3);
            mat[2, 2] = Funcs.sumPow(x, 2);
            mat[2, 3] = Funcs.sumPow(x, y, 2);

            return mat;
        }  
    }
}