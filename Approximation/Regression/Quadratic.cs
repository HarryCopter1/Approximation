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


            function = (z) => a * Math.Pow(z, 2) + b * z + c;
        }

        private double[,] getMatrix()
        {
            double[,] mat = new double[3,4];

            mat[0, 0] = sumPow(x,2);
            mat[0, 1] = Funcs.sum(x);
            mat[0, 2] = x.Count;
            mat[0, 3] = Funcs.sum(y);

            mat[1, 0] = sumPow(x, 3);
            mat[1, 1] = sumPow(x, 2);
            mat[1, 2] = Funcs.sum(x);
            mat[1, 3] = Funcs.sum(x,y);

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
                sum += Math.Pow(x[i], power)*y[i];
            }
            return sum;
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