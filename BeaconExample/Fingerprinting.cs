using System;
using System.Collections.Generic;

namespace BeaconExample
{
    class Fingerprinting
    {
        //creating a training set
        public static void AppendTrainingSet(string roomname, List<double> values)
        {
            string line = roomname + ";" + values[0] + ";" + values[1] + ";" + values[2] + ";" + values[3] + ";" + values[4] + ";" + values[5] + Environment.NewLine;
            //Console.WriteLine(line);
            System.IO.File.AppendAllText("C:\\Users\\dklomp1\\Pictures\\location test\\training.txt", line);
        }
        public static Dictionary<List<string>, double[][]> LoadTrainingSet() {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\dklomp1\\Pictures\\location test\\training.txt");
            Dictionary<List<string>, double[][]> trainingSet = new Dictionary<List<string>, double[][]>();
            List<string> subStringList = new List<string>();
            double[][] subDoubleList = new double[lines.Length][];
            //Console.WriteLine("size trainingset: " + lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] stringList = lines[i].Split(';');
                List<double> subSubDoubleList = new List<double>();
                subStringList.Add(stringList[0]);
                foreach (string nth in stringList)
                {
                    try
                    {
                        subSubDoubleList.Add(double.Parse(nth));
                    }
                    catch { }
                }
                subDoubleList[i] = subSubDoubleList.ToArray();
            }
            trainingSet.Add(subStringList,subDoubleList);
            return trainingSet;
        }
        public static void WriteLabelMap(Dictionary<int, string> labelMap)
        {
            foreach (KeyValuePair<int, string> label in labelMap) {
                    string line = label.Key.ToString() + ";" + label.Value + Environment.NewLine;
                    System.IO.File.AppendAllText("C:\\Users\\dklomp1\\Pictures\\location test\\labelMap.txt", line);
             
            }
        }
        public static Dictionary<int, string> ReadLabelMap()
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\dklomp1\\Pictures\\location test\\labelMap.txt");
            Dictionary<int, string> map = new Dictionary<int, string>();
            foreach (string line in lines)
            {
                string[] stringList = line.Split(';');
                if (!map.ContainsKey(int.Parse(stringList[0])))
                {
                    map.Add(int.Parse(stringList[0]), stringList[1]);
                }
            }
            return map;
        }
        public static void mapValues(double distanceA, double distanceB, double distanceC, double distanceD, double distanceE, double distanceF, string roomname)
        {
            List<double> values = new List<double>();
                values.Add(distanceA);
                values.Add(distanceB);
                values.Add(distanceC);
                values.Add(distanceD);
                values.Add(distanceE);
                values.Add(distanceF);
                AppendTrainingSet(roomname, values);
            }
        }
    }
