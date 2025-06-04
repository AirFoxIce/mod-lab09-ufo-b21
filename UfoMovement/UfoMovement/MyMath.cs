using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoMovement
{
    internal class MyMath
    {
        public static double Factorial(int number)
        {
            double result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        public static double MySin(double a, int numMembers)
        {
            double result = 0;
            int sign = 1;

            for (int i = 0; i < numMembers; i++)
            {
                int degree = 2 * i + 1;
                double numerator = Math.Pow(a, degree);
                double denominator = Factorial(degree);

                result += sign * (numerator / denominator);
                sign *= -1;
            }

            return result;
        }

        public static double MyCos(double a, int numMembers)
        {
            double result = 0;
            int sign = 1;

            for (int i = 0; i < numMembers; i++)
            {
                int degree = 2 * i;
                double numerator = Math.Pow(a, degree);
                double denominator = Factorial(degree);

                result += sign * (numerator / denominator);
                sign *= -1;
            }

            return result;
        }
    }
}
