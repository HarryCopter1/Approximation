using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approximation.Regression
{
    public class Quadratic : Graph
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Quadratic(List<double> x, List<double> y) : base(x, y)
        {
            this.x = x.ToList();
            this.y = y.ToList();


            findSolution(getMatrix(), ref a, ref b, ref c);
            r = getR();
            det = getDet();
            err = getRelativeError();


            function = (z) => a * Math.Pow(z, 2) + b * z + c;
        }

        //Коефіцієнт Кореляції
        public double getR()
        {
            double r = Math.Sqrt(1f - (sumCor1(y) / sumCor2(y)));
            return r;
        }

        public double sumCor1(List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                //sum += Math.Pow((y[i] - yx(y[i])), 2);
                sum += Math.Pow((y[i] - a * Math.Pow(x[i], 2) - b * x[i] - c), 2);
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
        public double getDet()
        {
            return Math.Pow(r, 2);
        }

        //Середня помилка апроксимації
        public double getRelativeError()
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
            //double yx = a * Math.Pow(X,2) + b * X + c;
            double yx = Math.Pow(X, 2) * a + b * X + c;
            return yx;
        }

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
        }




        private double[,] getMatrix()
        {
            double[,] mat = new double[3,4];

            mat[0, 0] = Funcs.sumPow(x,2);
            mat[0, 1] = Funcs.sum(x);
            mat[0, 2] = x.Count;
            mat[0, 3] = Funcs.sum(y);

            mat[1, 0] = Funcs.sumPow(x, 3);
            mat[1, 1] = Funcs.sumPow(x, 2);
            mat[1, 2] = Funcs.sum(x);
            mat[1, 3] = Funcs.sum(x,y);

            mat[2, 0] = Funcs.sumPow(x, 4);
            mat[2, 1] = Funcs.sumPow(x, 3);
            mat[2, 2] = Funcs.sumPow(x, 2);
            mat[2, 3] = Funcs.sumPow(x, y, 2);
                        
            return mat;
        }



        // This functions finds the determinant of Matrix 
        static double determinantOfMatrix(double[,] mat)
        {
            double ans;
            ans = mat[0, 0] * (mat[1, 1] * mat[2, 2] - mat[2, 1] * mat[1, 2])
                - mat[0, 1] * (mat[1, 0] * mat[2, 2] - mat[1, 2] * mat[2, 0])
                + mat[0, 2] * (mat[1, 0] * mat[2, 1] - mat[1, 1] * mat[2, 0]);
            
            return ans;
        }

        // This function finds the solution of system of 
        // linear equations using cramer's rule 
        static void findSolution(double[,] coeff, ref double a, ref double b, ref double c)
        {
            // Matrix d using coeff as given in cramer's rule 
            double[,] d = {
        { coeff[0,0], coeff[0,1], coeff[0,2] },
        { coeff[1,0], coeff[1,1], coeff[1,2] },
        { coeff[2,0], coeff[2,1], coeff[2,2] },
    };

            // Matrix d1 using coeff as given in cramer's rule 
            double[,] d1 = {
        { coeff[0,3], coeff[0,1], coeff[0,2] },
        { coeff[1,3], coeff[1,1], coeff[1,2] },
        { coeff[2,3], coeff[2,1], coeff[2,2] },
    };

            // Matrix d2 using coeff as given in cramer's rule 
            double[,] d2 = {
        { coeff[0,0], coeff[0,3], coeff[0,2] },
        { coeff[1,0], coeff[1,3], coeff[1,2] },
        { coeff[2,0], coeff[2,3], coeff[2,2] },
    };

            // Matrix d3 using coeff as given in cramer's rule 
            double[,] d3 = {
        { coeff[0,0], coeff[0,1], coeff[0,3] },
        { coeff[1,0], coeff[1,1], coeff[1,3] },
        { coeff[2,0], coeff[2,1], coeff[2,3] },
     };

            // Calculating Determinant of Matrices d, d1, d2, d3 
            double D = determinantOfMatrix(d);
            double D1 = determinantOfMatrix(d1);
            double D2 = determinantOfMatrix(d2);
            double D3 = determinantOfMatrix(d3);
            Console.Write("D is : {0:F6} \n", D);
            Console.Write("D1 is : {0:F6} \n", D1);
            Console.Write("D2 is : {0:F6} \n", D2);
            Console.Write("D3 is : {0:F6} \n", D3);

            // Case 1 
            if (D != 0)
            {
                // Coeff have a unique solution. Apply Cramer's Rule 
                a = D1 / D;
                b = D2 / D;
                c = D3 / D; // calculating z using cramer's rule 
                /*Console.Write("Value of x is : {0:F6}\n", x);
                Console.Write("Value of y is : {0:F6}\n", y);
                Console.Write("Value of z is : {0:F6}\n", z);*/
            }

            // Case 2 
        /*    else
            {
                if (D1 == 0 && D2 == 0 && D3 == 0)
                    Console.Write("Infinite solutions\n");
                else if (D1 != 0 || D2 != 0 || D3 != 0)
                    Console.Write("No solutions\n");
            }*/
        }
    }
}