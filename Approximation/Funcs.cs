using System;
using System.Collections.Generic;
using System.Linq;

namespace Approximation
{
    class Funcs
    {
        //Сума масиву
        static public double sum(List<double> arr)
        {
            return arr.Sum();
        }

        //Добуток двох масивів
        static public double sum(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += x[i] * y[i];
            }
            return sum;
        }

        //Сума масива в степені N
        static public double sumPow(List<double> x, int power)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], power);
            }
            return sum;
        }

        //Сума масива y і x в степені N
        static public double sumPow(List<double> x, List<double> y, int power)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], power) * y[i];
            }
            return sum;
        }

        //Сума логарифма натурального від масиву
        static public double sumLn(List<double> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Log(x[i]);
            }
            return sum;
        }

        static public double sumLnX(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Log(x[i]) * y[i];
            }
            return sum;
        }

        //Сума двох масивів від логарифма натурального
        static public double sumLn(List<double> x, List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += (Math.Log(x[i]) * Math.Log(y[i]));
            }
            return sum;
        }

        //Сума логарифма натурального від масиву в степені N
        static public double sumPowLn(List<double> x, int power)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(Math.Log(x[i]), power);
            }
            return sum;
        }
    }
}
