using System;
namespace BeaconExample
{
    class KnnDistance
    {
        
        /// <returns>Returns the Euclidean Distance Measure Between Points X and Points Y</returns>
        public static double EuclideanDistance(double[] X, double[] Y)
        {
            int count = 0;

            double distance = 0.0;

            double sum = 0.0;


            if (X.GetUpperBound(0) != Y.GetUpperBound(0))
            {
                throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
            }
            else
            {
                count = X.Length;
            }

            for (int i = 0; i < count; i++)
            {
                sum = sum + Math.Pow(Math.Abs(X[i] - Y[i]), 2);
            }

            distance = Math.Sqrt(sum);

            return distance;
        }
        
        /// <returns>Returns the Minkowski Distance Measure Between Points X and Points Y</returns>
        public static double ChebyshevDistance(double[] X, double[] Y)
        {
            int count = 0;

            if (X.GetUpperBound(0) != Y.GetUpperBound(0))
            {
                throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
            }
            else
            {
                count = X.Length;
            }
            double[] sum = new double[count];

            for (int i = 0; i < count; i++)
            {
                sum[i] = Math.Abs(X[i] - Y[i]);
            }
            double max = double.MinValue;
            foreach (double num in sum)
            {
                if (num > max)
                {
                    max = num;
                }
            }
            return max;
        }
        
        /// <returns>Returns the Manhattan Distance Measure Between Points X and Points Y</returns>
        public static double ManhattanDistance(double[] X, double[] Y)
        {
            int count = 0;

            double distance = 0.0;

            double sum = 0.0;


            if (X.GetUpperBound(0) != Y.GetUpperBound(0))
            {
                throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
            }
            else
            {
                count = X.Length;
            }

            for (int i = 0; i < count; i++)
            {
                sum = sum + Math.Abs(X[i] - Y[i]);
            }

            distance = sum;

            return distance;
        }

    }
}