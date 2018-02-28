using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windows.Devices.Bluetooth.Advertisement;

namespace BeaconExample
{
    class Program
    {
        private static double maxA = -100;
        private static double minA = 0;
        private static double maxB = -100;
        private static double minB = 0;
        private static double maxC = -100;
        private static double minC = 0;
        private static double maxD = -100;
        private static double minD = 0;
        private static DateTime timeStart = new DateTime(2000,01,01);
        private static DateTime timeEnd = new DateTime(2000, 01, 01);
        private static List<double> AList = new List<double>();
        private static List<double> BList = new List<double>();
        private static List<double> CList = new List<double>();
        private static List<double> DList = new List<double>();
        private static List<double> EList = new List<double>();
        private static List<double> FList = new List<double>();
        private static List<double> AVGList = new List<double>();

        static void Main(string[] args)
        {
            timeStart = DateTime.Now;
            timeEnd = timeStart.AddSeconds(1);
            var watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += Watcher_Received;
            watcher.Start();
            Console.WriteLine("Bluetooth LE Advertisement Watcher Started (Press ESC to exit)");
            while (true)
            {
                Thread.Sleep(1);
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            watcher.Stop();
            Console.WriteLine("Bluetooth LE Advertisement Watcher Stopped");
            
        }
        

        private static void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            //calibrated at 0 meter
            double A1 = 38;
            double B1 = 38;
            double C1 = 38;
            double D1 = 38;
            //calibrated at 25
            double A2 = 94;
            double B2 = 94;
            double C2 = 94;
            double D2 = 94;

            int A2Distance = 25;
            int B2Distance = 25;
            int C2Distance = 25;
            int D2Distance = 25;


            foreach (var adv in args.Advertisement.ManufacturerData.Where(x => x.CompanyId == 0x004C))
            {
                if (args.Timestamp.DateTime > timeEnd)
                {
                    // set the time to scan for signals. more is more accurate, but also less responsive.
                    timeStart = args.Timestamp.DateTime;
                    timeEnd = timeStart.AddSeconds(30);
                    // calculate the distance through the filtered list.
                    double rssiA = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(AList, QuickMaths.Mean(AList), QuickMaths.stDev(AList, QuickMaths.Mean(AList)))));
                    double rssiB = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(BList, QuickMaths.Mean(BList), QuickMaths.stDev(BList, QuickMaths.Mean(BList)))));
                    double rssiC = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(CList, QuickMaths.Mean(CList), QuickMaths.stDev(CList, QuickMaths.Mean(CList)))));
                    double rssiD = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(DList, QuickMaths.Mean(DList), QuickMaths.stDev(DList, QuickMaths.Mean(DList)))));
                    double rssiE = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(EList, QuickMaths.Mean(EList), QuickMaths.stDev(EList, QuickMaths.Mean(EList)))));
                    double rssiF = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(FList, QuickMaths.Mean(FList), QuickMaths.stDev(FList, QuickMaths.Mean(FList)))));

                    if (!double.IsNaN(rssiA))
                    {
                        AVGList.Add(rssiA);
                    }

                    if (rssiA > maxA)
                    {
                        maxA = rssiA;
                    }
                    if (rssiA < minA)
                    {
                        minA = rssiA;
                    }
                    if (rssiB > maxB)
                    {
                        maxB = rssiB;
                    }
                    if (rssiB < minB)
                    {
                        minB = rssiB;
                    }
                    if (rssiC > maxC)
                    {
                        maxC = rssiC;
                    }
                    if (rssiC < minC)
                    {
                        minC = rssiC;
                    }
                    if (rssiD > maxD)
                    {
                        maxD = rssiD;
                    }
                    if (rssiD < minD)
                    {
                        minD = rssiD;
                    }
                    double distanceA = QuickMaths.getDistance(rssiA, A1,A2,A2Distance);
                    double distanceB = QuickMaths.getDistance(rssiB, B1,B2,B2Distance);
                    double distanceC = QuickMaths.getDistance(rssiC, C1,C2,C2Distance);
                    double distanceD = QuickMaths.getDistance(rssiD, D1, D2, D2Distance);
                    double distanceE = QuickMaths.getDistance(rssiE, D1, D2, D2Distance);
                    double distanceF = QuickMaths.getDistance(rssiF, D1, D2, D2Distance);

                    //Console.WriteLine(" ");
                    //Console.WriteLine(
                    //    "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {5}, min: {6}, max: {7}",
                    //    timeEnd,
                    //    "A",
                    //    rssiA,
                    //    distanceA,
                    //    AList.Count,
                    //    minA,
                    //    maxA
                    //    );
                    //Console.WriteLine(
                    //    "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {5}, min: {6}, max: {7}",
                    //    timeEnd,
                    //    "B",
                    //    rssiB,
                    //    distanceB,
                    //    BList.Count,
                    //    minB,
                    //    maxB);

                    //Console.WriteLine(
                    //    "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {5}, min: {6}, max: {7}",
                    //    timeEnd,
                    //    "C",
                    //    rssiC,
                    //    distanceC,
                    //    CList.Count,
                    //    minC,
                    //    maxC);
                    //Console.WriteLine(
                    //    "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Distance Alt: {4}, Measurements: {5}, min: {6}, max: {7}",
                    //    timeEnd,
                    //    "C",
                    //    rssiD,
                    //    distanceD,
                    //    distanceAltD,
                    //    DList.Count,
                    //    minD,
                    //    maxD);
                    try
                    {
                        generatePNG.createIMG(distanceA, distanceB, distanceC);
                    }
                    catch
                    {
                    }
                    // uncomment for fingerprinting
                    //Fingerprinting.mapValues(distanceA,distanceB,distanceC,distanceD,distanceE,distanceF,"West28");

                    //uncomment to create a Knn algoritm
                    KnnExample.KnnCreate(Fingerprinting.LoadTrainingSet());

                    // uncomment when testing.
                    double[] coordinates = { distanceA, distanceB, distanceC, distanceD, distanceE, distanceF };
                    Console.WriteLine("Room: " + KnnExample.GetRoom(KnnExample.LoadKnn(), coordinates));
                    AList.Clear();
                    BList.Clear();
                    CList.Clear();
                    DList.Clear();
                    EList.Clear();
                    FList.Clear();

                }
                else
                {
                    switch (args.BluetoothAddress)
                    {
                        case 119764812684866:
                            AList.Add(args.RawSignalStrengthInDBm);
                            break;
                        case 146145262751511:
                            BList.Add(args.RawSignalStrengthInDBm);
                            break;
                        case 146145262751546:
                            CList.Add(args.RawSignalStrengthInDBm);
                            break;
                        case 167528103405708:
                            DList.Add(args.RawSignalStrengthInDBm);
                            break;
                        case 167528103407835:
                            EList.Add(args.RawSignalStrengthInDBm);
                            break;
                        case 268687445826578:
                            FList.Add(args.RawSignalStrengthInDBm);
                            break;


                    }
                }
            }
        }
        private static void Watcher_Received2(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            foreach (var adv in args.Advertisement.ManufacturerData.Where(x => x.CompanyId == 0x004C))
            {
                if (args.RawSignalStrengthInDBm > -75)
                {
                    Console.WriteLine(args.BluetoothAddress + ", rssi: " + args.RawSignalStrengthInDBm);
                }
            }
        }
    }
}