using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation
{
    class Funcs
    {
        static public double sum(List<double> arr)
        {
            return arr.Sum();
        }

        static public double sum(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += x[i] * y[i];
            }
            return sum;
        }

        static public double sumPow(List<double> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], 2);
            };
            return sum;
        }
    }
}
