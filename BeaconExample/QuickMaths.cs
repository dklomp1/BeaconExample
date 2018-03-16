using System;
using System.Collections.Generic;
using System.Linq;

namespace BeaconExample
{
    class QuickMaths
    {
        // calculate the mean of the list with rssi values
        public static double Mean(List<double> values)
        {
            int n = values.Count;
            double sum = values.Sum();
            double result = sum / n;
            return result;
        }
        // calculate the standard deviation of the list
        public static double stDev(List<double> values, double mean)
        {

            List<double> devList = new List<double>();
            foreach (int i in values)
            {
                double absoluteVal = Math.Sqrt(i * i);
                double absoluteMean = Math.Sqrt(mean * mean);
                double absoluteRes = Math.Sqrt((absoluteMean - absoluteVal) * (absoluteMean - absoluteVal));
                devList.Add(absoluteRes);
            }
            double result = devList.Sum() / devList.Count;
            return result;
        }
        // filter the values that deviate to much from the mean.
        public static List<double> correctedList(List<double> values, double mean, double std)
        {
            List<double> result = new List<double>();

            foreach (double i in values)
            {
                //Console.WriteLine("std: " + std + ", mean: " + mean + ", value: " + i);
                if ((i > mean - (std * 1.5) && i < mean + (std * 1.5)))
                {
                    result.Add(i);
                }
                else
                {
                    //Console.WriteLine("filter");
                }
            }
            return result;

        }
        public static double getDistance(double rssi, double txCalibrated, double txCalibrated2, int tx2Distance)
        {

            double distance = 0;
            double refDist;
            double rssiPositive = Math.Sqrt(rssi * rssi);
            // distance = (((rssi - A1) / refDist) / 100) * 10
            // the distance calibrated from measurements at 1 and 11 meter.
            // rssi is the current signal strength
            // A1 is the rssi at 1 meter for beacon A
            // A2 is the rssi at 11 meter for beacon A
            // refdist is a value scale that scales the distance from point 1m to 10m
            // the current rssi is mapped according to the refdist.
            // because the distance will be lineair, a filter based on free space path loss values will be applied to get a fitted logarithmic scale.
            double correction = 0.3 + (distanceAlt(rssi) / 35);
            //Console.WriteLine(distanceAlt(rssi));
            //Console.WriteLine(correction);
            refDist = ((txCalibrated2 - txCalibrated) / 100);
            distance = ((((rssiPositive - txCalibrated) / refDist) / 100) * tx2Distance) * correction;
            //Console.WriteLine(distance);
            if(distance < 0){
                distance = 299.00;
            }
            return distance + 1;
        }
        public static double distanceAlt(double rssi)
        {
            double mWTracker = 1 * Math.Pow(10, rssi / 10);
            double mWBeacon = 1;
            double mathlog = 20 * Math.Log10(2400);
            double dB = 10 * Math.Log10(mWBeacon / mWTracker);
            double resultsub = (dB - mathlog + 27.55) / 20;
            double result = Math.Pow(10, resultsub);
            return result;
        }
    }
}
