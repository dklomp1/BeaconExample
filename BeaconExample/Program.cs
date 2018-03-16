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
        private static double maxE = -100;
        private static double minE = 0;
        private static double maxF = -100;
        private static double minF = 0;
        private static DateTime timeStart = new DateTime(2000,01,01);
        private static DateTime timeEnd = new DateTime(2000, 01, 01);
        private static List<double> AList = new List<double>();
        private static List<double> BList = new List<double>();
        private static List<double> CList = new List<double>();
        private static List<double> DList = new List<double>();
        private static List<double> EList = new List<double>();
        private static List<double> FList = new List<double>();

        static void Main(string[] args)
        {
            timeStart = DateTime.Now;
            timeEnd = timeStart.AddSeconds(30);
            var watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += Watcher_Received;
            watcher.Start();
            //Console.WriteLine("Bluetooth LE Advertisement Watcher Started (Press ESC to exit)");
            while (true)
            {
            }
            
        }
        

        private static void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            //calibrated at 0 meter
            double A1 = 36;
            double B1 = 36;
            double C1 = 36;
            double D1 = 36;
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
                if (args.Timestamp.DateTime > timeEnd || (AList.Count != 0 && BList.Count != 0 && CList.Count != 0 && DList.Count != 0 && EList.Count != 0 && FList.Count != 0))
                {
                    // set the time to scan for signals. more is more accurate, but also less responsive.
                    timeStart = args.Timestamp.DateTime;
                    timeEnd = timeStart.AddSeconds(30);
                    // calculate the distance through the filtered list.
                    double rssiA;
                    double rssiB;
                    double rssiC;
                    double rssiD;
                    double rssiE;
                    double rssiF;
                    if (AList.Count == 0)
                    {
                        rssiA = -100;
                    } else if(AList.Count == 1)
                    {
                        rssiA = AList[0];
                    }
                    else
                    {
                        rssiA = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(AList, QuickMaths.Mean(AList), QuickMaths.stDev(AList, QuickMaths.Mean(AList)))));
                    }
                    if (BList.Count == 0)
                    {
                        rssiB = -100;
                    } else if (BList.Count == 1)
                    {
                        rssiB = BList[0];
                    }
                    else
                    {
                        rssiB = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(BList, QuickMaths.Mean(BList), QuickMaths.stDev(BList, QuickMaths.Mean(BList)))));
                    }
                    if (CList.Count == 0)
                    {
                        rssiC = -100;
                    } else if (CList.Count == 1)
                    {
                        rssiC = CList[0];
                    }
                    else
                    {
                        rssiC = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(CList, QuickMaths.Mean(CList), QuickMaths.stDev(CList, QuickMaths.Mean(CList)))));
                    }
                    if (DList.Count == 0)
                    {
                        rssiD = -100;
                    }  else if(DList.Count == 1)
                    {
                        rssiD = DList[0];
                    }
                    else
                    {
                        rssiD = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(DList, QuickMaths.Mean(DList), QuickMaths.stDev(DList, QuickMaths.Mean(DList)))));
                    }
                    if (EList.Count == 0)
                    {
                        rssiE = -100;
                    } else if (EList.Count == 1)
                    {
                        rssiE = EList[0];
                    }
                    else
                    {
                        rssiE = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(EList, QuickMaths.Mean(EList), QuickMaths.stDev(EList, QuickMaths.Mean(EList)))));
                    }
                    if (FList.Count == 0)
                    {
                        rssiF = -100;
                    } else if (FList.Count == 1)
                    {
                        rssiF = FList[0];
                    }
                    else
                    {
                        rssiF = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(FList, QuickMaths.Mean(FList), QuickMaths.stDev(FList, QuickMaths.Mean(FList)))));
                    }


                    if (double.IsNaN(rssiA))
                    {
                        rssiA = QuickMaths.Mean(AList);
                    }
                    if (double.IsNaN(rssiB))
                    {
                        rssiB = QuickMaths.Mean(BList);
                    }
                    if (double.IsNaN(rssiC))
                    {
                        rssiC = QuickMaths.Mean(CList);
                    }
                    if (double.IsNaN(rssiD))
                    {
                        rssiD = QuickMaths.Mean(DList);
                    }
                    if (double.IsNaN(rssiE))
                    {
                        rssiE = QuickMaths.Mean(EList);
                    }
                    if (double.IsNaN(rssiF))
                    {
                        rssiF = QuickMaths.Mean(FList);
                    }
                    if (rssiA > maxA && rssiA != 0)
                    {
                        maxA = rssiA;
                    }
                    if (rssiA < minA)
                    {
                        minA = rssiA;
                    }
                    if (rssiB > maxB && rssiB != 0)
                    {
                        maxB = rssiB;
                    }
                    if (rssiB < minB)
                    {
                        minB = rssiB;
                    }
                    if (rssiC > maxC && rssiC != 0)
                    {
                        maxC = rssiC;
                    }
                    if (rssiC < minC)
                    {
                        minC = rssiC;
                    }
                    if (rssiD > maxD && rssiD != 0)
                    {
                        maxD = rssiD;
                    }
                    if (rssiD < minD)
                    {
                        minD = rssiD;
                    }
                    if (rssiE > maxE && rssiE != 0)
                    {
                        maxE = rssiE;
                    }
                    if (rssiE < minE)
                    {
                        minE = rssiE;
                    }
                    if (rssiF > maxF && rssiF != 0)
                    {
                        maxF = rssiF;
                    }
                    if (rssiF < minF)
                    {
                        minF = rssiF;
                    }
                    double distanceA = QuickMaths.getDistance(rssiA, A1,A2,A2Distance);
                    double distanceB = QuickMaths.getDistance(rssiB, B1,B2,B2Distance);
                    double distanceC = QuickMaths.getDistance(rssiC, C1,C2,C2Distance);
                    double distanceD = QuickMaths.getDistance(rssiD, D1, D2, D2Distance);
                    double distanceE = QuickMaths.getDistance(rssiE, D1, D2, D2Distance);
                    double distanceF = QuickMaths.getDistance(rssiF, D1, D2, D2Distance);

                    Console.WriteLine(" ");
                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "A",
                        rssiA,
                        distanceA,
                        AList.Count,
                        minA,
                        maxA
                        );
                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "B",
                        rssiB,
                        distanceB,
                        BList.Count,
                        minB,
                        maxB);

                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "C",
                        rssiC,
                        distanceC,
                        CList.Count,
                        minC,
                        maxC);
                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "D",
                        rssiD,
                        distanceD,
                        DList.Count,
                        minD,
                        maxD);
                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "E",
                        rssiE,
                        distanceE,
                        EList.Count,
                        minE,
                        maxE);
                    Console.WriteLine(
                        "[{0}], Beacon: {1}, Rssi: {2}, Distance: {3}, Measurements: {4}, min: {5}, max: {6}",
                        timeEnd,
                        "F",
                        rssiF,
                        distanceF,
                        FList.Count,
                        minF,
                        maxF);
                    try
                    {
                        generatePNG.createIMG(distanceA, distanceB, distanceC);
                    }
                    catch
                    {
                    }
                    // uncomment for fingerprinting
                    //Fingerprinting.mapValues(rssiA,rssiB,rssiC,rssiD,rssiE,rssiF,"West28");

                    //uncomment to create a Knn algoritm
                    //KnnExample.KnnCreate(Fingerprinting.LoadTrainingSet());

                    //// uncomment when testing.
                    double[] coordinates = { rssiA * -1, rssiB * -1, rssiC * -1, rssiD * -1, rssiE * -1, rssiF * -1 };
                    foreach (string option in KnnExample.getOptions(coordinates, KnnExample.LoadKnn()))
                    {
                        Console.WriteLine(option);
                    }
                    KnnExample.GetRoom(KnnExample.LoadKnn(), coordinates);
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
        //private static void Watcher_Received2(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        //{
        //    foreach (var adv in args.Advertisement.ManufacturerData.Where(x => x.CompanyId == 0x004C))
        //    {
        //        if (args.RawSignalStrengthInDBm > -75)
        //        {
        //            switch (args.BluetoothAddress)
        //            {
        //                case 119764812684866:
        //                    Console.WriteLine("A");
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    break;
        //                case 146145262751511:
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    Console.WriteLine(args.Advertisement.ServiceUuids);
        //                    break;
        //                case 146145262751546:
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    Console.WriteLine(args.Advertisement.ServiceUuids);
        //                    break;
        //                case 167528103405708:
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    Console.WriteLine(args.Advertisement.ServiceUuids);
        //                    break;
        //                case 167528103407835:
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    Console.WriteLine(args.Advertisement.ServiceUuids);
        //                    break;
        //                case 268687445826578:
        //                    Console.WriteLine(args.RawSignalStrengthInDBm);
        //                    Console.WriteLine(args.Advertisement.ServiceUuids);
        //                    break;
        //            }
        //            Console.WriteLine(args.BluetoothAddress + ", rssi: " + args.RawSignalStrengthInDBm);
        //        }
        //    }
        //}
    //    private static void Watcher_Received3(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
    //    {
    //        //calibrated at 0 meter
    //        double A = 36;
    //        //calibrated at 25
    //        double A2 = 94;

    //        int A2Distance = 25;


    //        foreach (var adv in args.Advertisement.ManufacturerData.Where(x => x.CompanyId == 0x004C))
    //        {
    //            if (args.Timestamp.DateTime > timeEnd)
    //            {
    //                // set the time to scan for signals. more is more accurate, but also less responsive.
    //                timeStart = args.Timestamp.DateTime;
    //                timeEnd = timeStart.AddSeconds(10);
    //                // calculate the distance through the filtered list.

    //                double rssiA = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(AList, QuickMaths.Mean(AList), QuickMaths.stDev(AList, QuickMaths.Mean(AList)))));
    //                double rssiB = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(BList, QuickMaths.Mean(BList), QuickMaths.stDev(BList, QuickMaths.Mean(BList)))));
    //                double rssiC = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(CList, QuickMaths.Mean(CList), QuickMaths.stDev(CList, QuickMaths.Mean(CList)))));
    //                double rssiD = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(DList, QuickMaths.Mean(DList), QuickMaths.stDev(DList, QuickMaths.Mean(DList)))));
    //                double rssiE = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(EList, QuickMaths.Mean(EList), QuickMaths.stDev(EList, QuickMaths.Mean(EList)))));
    //                double rssiF = Math.Round(QuickMaths.Mean(QuickMaths.correctedList(FList, QuickMaths.Mean(FList), QuickMaths.stDev(FList, QuickMaths.Mean(FList)))));
    //                double distanceA = QuickMaths.getDistance(rssiA, A, A2, A2Distance);
    //                double distanceB = QuickMaths.getDistance(rssiB, A, A2, A2Distance);
    //                double distanceC = QuickMaths.getDistance(rssiC, A, A2, A2Distance);
    //                double distanceD = QuickMaths.getDistance(rssiD, A, A2, A2Distance);
    //                double distanceE = QuickMaths.getDistance(rssiE, A, A2, A2Distance);
    //                double distanceF = QuickMaths.getDistance(rssiF, A, A2, A2Distance);

    //                if (double.IsNaN(rssiA))
    //                {
    //                    rssiA = 0;
    //                }
    //                if (double.IsNaN(rssiB))
    //                {
    //                    rssiB = 0;
    //                }
    //                if (double.IsNaN(rssiC))
    //                {
    //                    rssiC = 0;
    //                }
    //                if (double.IsNaN(rssiD))
    //                {
    //                    rssiD = 0;
    //                }
    //                if (double.IsNaN(rssiE))
    //                {
    //                    rssiE = 0;
    //                }
    //                if (double.IsNaN(rssiF))
    //                {
    //                    rssiF = 0;
    //                }
                    
    //                // uncomment for fingerprinting
    //                //Fingerprinting.mapValues(distanceA,distanceB,distanceC,distanceD,distanceE,distanceF,"West53");

    //                //uncomment to create a Knn algoritm
    //                KnnExample.KnnCreate(Fingerprinting.LoadTrainingSet());

    //                // uncomment when testing.
                    
    //                List<double> coordinatesSub = new List<double>();
    //                List<double> coordinates = new List<double>();
    //                List<double> subsub = coordinatesSub;
    //                coordinatesSub.Add(rssiA);
    //                coordinatesSub.Add(rssiB);
    //                coordinatesSub.Add(rssiC);
    //                coordinatesSub.Add(rssiD);
    //                coordinatesSub.Add(rssiE);
    //                coordinatesSub.Add(rssiF);

                    
    //                foreach(double coordinate in coordinatesSub)
    //                {
    //                    Console.WriteLine(coordinate);
    //                }
    //                //foreach (string option in KnnExample.getOptions(coordinatesSub.ToArray(), KnnExample.LoadKnn()))
    //                //{
    //                //    Console.WriteLine(option);
    //                //}
    //                Fingerprinting.mapValues(rssiA, rssiB, rssiC, rssiD, rssiE, rssiF,"West53");
    //                AList.Clear();
    //                BList.Clear();
    //                CList.Clear();
    //                DList.Clear();
    //                EList.Clear();
    //                FList.Clear();

    //            }
    //            else
    //            {
    //                switch (args.BluetoothAddress)
    //                {
    //                    case 119764812684866:
    //                        AList.Add(args.RawSignalStrengthInDBm);
    //                        break;
    //                    case 146145262751511:
    //                        BList.Add(args.RawSignalStrengthInDBm);
    //                        break;
    //                    case 146145262751546:
    //                        CList.Add(args.RawSignalStrengthInDBm);
    //                        break;
    //                    case 167528103405708:
    //                        DList.Add(args.RawSignalStrengthInDBm);
    //                        break;
    //                    case 167528103407835:
    //                        EList.Add(args.RawSignalStrengthInDBm);
    //                        break;
    //                    case 268687445826578:
    //                        FList.Add(args.RawSignalStrengthInDBm);
    //                        break;


    //                }
    //            }
    //        }
    //    }
    }
}