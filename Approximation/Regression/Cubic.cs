using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Approximation.Regression
{
    class Cubic : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Cubic(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();

            Matrix test = new Matrix();

            test.AddLinearEquation(Funcs.sum(y),  sumPow(x,3), sumPow(x,2), Funcs.sum(x), x.Count);
            test.AddLinearEquation(Funcs.sum(x,y), sumPow(x, 4), sumPow(x, 3), sumPow(x, 2), Funcs.sum(x));
            test.AddLinearEquation(sumPow(x,y,2), sumPow(x, 5), sumPow(x, 4), sumPow(x, 3), sumPow(x, 2));
            test.AddLinearEquation(sumPow(x, y, 3), sumPow(x, 6), sumPow(x, 5), sumPow(x, 4), sumPow(x, 3));
            var result = test.Solve();

            string tes="";

            foreach (var asdf in result)
            {
                tes = tes + asdf + "\n";
                Console.Write(asdf);
            }

            a = result[0];
            b = result[1];
            c = result[2];
            d = result[3];

            function = (z) => a * Math.Pow(z,3)+ b*Math.Pow(z,2) + c * z + d;
        }

        private double[,] getMatrix()
        {
            double[,] mat = new double[3, 4];

            mat[0, 0] = sumPow(x, 2);
            mat[0, 1] = Funcs.sum(x);
            mat[0, 2] = x.Count;
            mat[0, 3] = Funcs.sum(y);

            mat[1, 0] = sumPow(x, 3);
            mat[1, 1] = sumPow(x, 2);
            mat[1, 2] = Funcs.sum(x);
            mat[1, 3] = Funcs.sum(x, y);

            mat[2, 0] = sumPow(x, 4);
            mat[2, 1] = sumPow(x, 3);
            mat[2, 2] = sumPow(x, 2);
            mat[2, 3] = sumPow(x, y, 2);

            return mat;
        }

        private double sumPow(List<double> x, int power)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], power);
            }
            return sum;
        }

        private double sumPow(List<double> x, List<double> y, int power)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], power) * y[i];
            }
            return sum;
        }

        
    }
}