using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton_s_DD
{
    class Program
    {
        static void Main(string[] args)
        {
            int problem;

            double IntervalA = 0;
            double IntervalB = 0;
            double xValue = 0;

            Console.WriteLine(String.Format("Select which problem you would like to solve for: \n1 \n2 \n3"));
            Console.WriteLine("---------------------------------------");

            problem = Convert.ToInt16(Console.ReadLine());
            problem--;

            Console.WriteLine("Interval - A");
            IntervalA = double.Parse(Console.ReadLine());

            Console.WriteLine("Interval - B");
            IntervalB = double.Parse(Console.ReadLine());

            Console.WriteLine("X value");
            xValue = double.Parse(Console.ReadLine());
            switch (problem)
            {
                case 0:
                    ProblemA probA = new ProblemA();
                    double[,] data = probA.CreateDataPoints(probA.CalculatePoints(IntervalA, IntervalB), IntervalA);
                    Console.WriteLine(probA.Interpolate(data, xValue));
                    break;


                case 1:
                    ProblemB probB = new ProblemB();
                    data = probB.CreateDataPoints(probB.CalculatePoints(IntervalA, IntervalB), IntervalA);
                    Console.WriteLine(probB.Interpolate(data, xValue));
                    break;
            }
            




            Console.ReadLine();
        }
    }

    class ProblemA
    {
        public double CalculatePoints(double a, double b)
        {
            double range = Math.Abs(b - a);
            return (range / 9.0f);
        }

        public double[,] CreateDataPoints(double range, double a)
        {
            double[,] data = new double[10, 2];
            //Tuple<double, double> data;

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    data[i, 0] = a;
                }
                else
                {
                    //data = new Tuple<double, double>(, 0);
                    data[i, 0] = data[i-1, 0] + range;
                }

                data[i, 1] = Function1(data[i,0]);
                Console.WriteLine($"Data point {i + 1}: [x: {data[i, 0]}, y: {data[i, 1]}");
            }

            return data;
        }

        //to Interpolate for f(x), given x and a set of n data points
        //(x(i), f(i)) i = 1, 2, ..., n
        public double Interpolate(double[,] dataPoints, double x)
        {
            //set sum = 0
            double Sum = 0;

            //do for I = 1 to N
            for (int i = 0; i < 10; i++)
            {
                //set P = 1
                double P = 1;

                //do for J = 1 to N
                for (int j = 0; j < 10; j++)
                {

                    //if J != I
                    if (j != i)
                    {
                        //set P = P * (x - x(j))/(x(I) - x(J))
                        P = P * ((x - dataPoints[j,0]) / (dataPoints[i, 0] - dataPoints[j, 0]));
                    }

                    //end do (J)
                }

                //set sum = sum + P * f(I)
                Sum = Sum + P * Function1(dataPoints[i, 0]);

                //end do(I)
            }
            //sum is the interpolated value
            return Sum;
        }

        private double Function1(double x)
        {
            double answer = (2 * Math.Sin(x)) + (3 * Math.Cos(x));

            return answer;
        }
    }

    class ProblemB
    {
        public double CalculatePoints(double a, double b)
        {
            double range = Math.Abs(b - a);
            return (range / 9.0f);
        }

        public double[,] CreateDataPoints(double range, double a)
        {
            double[,] data = new double[10, 2];

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    data[i, 0] = a;
                }
                else
                {
                    //data = new Tuple<double, double>(, 0);
                    data[i, 0] = data[i - 1, 0] + range;
                }

                data[i, 1] = Function1(data[i, 0]);
                Console.WriteLine($"Data point {i+1}: [x: {data[i, 0]}, y: {data[i, 1]}");
            }



            return data;
        }

        //to Interpolate for f(x), given x and a set of n data points
        //(x(i), f(i)) i = 1, 2, ..., n
        public double Interpolate(double[,] dataPoints, double x)
        {
            //set sum = 0
            double Sum = 0;

            //do for I = 1 to N
            for (int i = 0; i < 10; i++)
            {
                //set P = 1
                double P = 1;

                //do for J = 1 to N
                for (int j = 0; j < 10; j++)
                {

                    //if J != I
                    if (j != i)
                    {
                        //set P = P * (x - x(j))/(x(I) - x(J))
                        P = P * ((x - dataPoints[j, 0]) / (dataPoints[i, 0] - dataPoints[j, 0]));
                    }

                    //end do (J)
                }

                //set sum = sum + P * f(x(I))
                Sum = Sum + P * Function1(dataPoints[i, 0]);

                //end do(I)
            }

            //sum is the interpolated value
            return Sum;
        }

        private double Function1(double x)
        {
            //g(x) = e^−x^2
            double answer = Math.Pow(Math.E, -Math.Pow(x, 2));

            return answer;
        }
    }
}
