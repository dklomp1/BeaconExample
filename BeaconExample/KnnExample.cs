using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.IO;
using Accord.MachineLearning;
using Accord.Statistics.Analysis;

namespace BeaconExample
{
    class KnnExample
    {
        public static string basePath = "C:\\Users\\dklomp1\\source\\repos\\BeaconExample";

        public static Dictionary<int, string> KnnCreate(Dictionary<List<string>, double[][]> trainingSet)
        {
            // Create some sample learning data. 
            int labelCounter = -1;
            List<int> classesList = new List<int>();
            Dictionary<int, string> labelMap = new Dictionary<int, string>();
            foreach (string label in trainingSet.First().Key.ToArray())
            {
                if (!labelMap.ContainsValue(label))
                {
                    labelCounter++;
                    classesList.Add(labelCounter);
                    labelMap.Add(labelCounter, label);
                    Console.WriteLine(labelCounter + ":" + label);
                }
                else
                {
                    classesList.Add(labelCounter);
                }
            }

            int[] classes = classesList.ToArray();
            double[][] inputs = trainingSet.First().Value;

            // Now we will create the K-Nearest Neighbors algorithm. 
            // It's possible to swtich around the k: 4 for the possibility of better accuracy
            var knn = new KNearestNeighbors(k:4);

            // We train the algorithm:
            knn.Learn(inputs, classes);
            
            // Let's say we would like to compute the error matrix for the classifier:
            var cm = GeneralConfusionMatrix.Estimate(knn, inputs, classes);

            // We can use it to estimate measures such as 
            double error = cm.Error;  // should be 
            double acc = cm.Accuracy; // should be 
            double kappa = cm.Kappa;  // should be
            Console.WriteLine("error: " + error);
            Console.WriteLine("accuracy: " + acc);
            Console.WriteLine("kappa: " + kappa);
            Console.WriteLine("pearson: " + cm.Pearson);
            for (int i = 0; i < cm.ColumnErrors.Length; i++)
            {
                if (cm.ColumnErrors[i] != 0)
                {
                    
                    double columnerror = double.Parse(cm.ColumnErrors[i].ToString()) / double.Parse(cm.ColumnTotals[i].ToString());
                    Console.WriteLine("Error of " + labelMap[i] + ": " +columnerror);
                }
            }
            SaveKnn(knn);
            Fingerprinting.WriteLabelMap(labelMap);
            return labelMap;
        }
        public static void SaveKnn(KNearestNeighbors knn)
        {
            // After we have created and learned our model, let's say we would 
            // like to save it to disk. For this, we can import the Accord.IO 
            // namespace at the top of our source file namespace, and then use 
            // Serializer's extension method Save:

            // Save to a file called "knn.bin" in the basePath directory:
            knn.Save(Path.Combine(basePath, "knn.bin"));


        }
        public static KNearestNeighbors LoadKnn()
        {
            // To load it back from the disk, we might need to use the Serializer class directly:
            var loaded_knn = Serializer.Load<KNearestNeighbors>(Path.Combine(basePath, "knn.bin"));

            // At this point, knn and loaded_knn should be 
            // two different instances of identical objects.
            return loaded_knn;
        }
        public static string getRoomname(int roomInt)
        {
            Dictionary<int,string> labelMap = Fingerprinting.ReadLabelMap();
            return labelMap[roomInt];

        }
        public static string GetRoom(KNearestNeighbors knn, double[] coordinates)
        {
            // After the algorithm has been created, we can classify a new instance:
            return getRoomname(knn.Decide(coordinates)); 


        }
    }
}