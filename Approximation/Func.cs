using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation
{
    class Func
    {
        static public double sum(List<double> arr)
        {
            return arr.Sum();
        }

        static public double sum(List<double> arr1, List<double> arr2)
        {
            double sum = 0;
            for (int i = 0; i < arr1.Count; i++)
            {
                sum += arr1[i] * arr2[i];
            }
            return sum;
        }

        static public double sumPow(List<double> arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                sum += Math.Pow(arr[i], 2);
            };
            return sum;
        }
    }
}
