using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;










namespace Algorithms.MachineLearning.DecisionTree
{

    public interface IDecisionTree
    {

    }


    public class ID3Node
    {
        public int DecisionVariableIndex;

        public Dictionary<String, double> TargetClassProbabilities;

        public double Entropy;

        public Dictionary<String, ID3Node> Children;
    }

    public class ID3 : IDecisionTree
    {
        public ID3Node Root;


        public ID3(String TrainingDataFile, char FieldSeparator, bool ContainsHeader, int TargetVariablePositionZeroBased)
        {
            Train(TrainingDataFile, FieldSeparator, ContainsHeader, TargetVariablePositionZeroBased);
        }

        public void Train(String TrainingDataFile, char FieldSeparator, bool ContainsHeader, int TargetVariablePositionZeroBased)
        {
            IEnumerable<Tuple<List<String>, String>> data =
                       from lines in File.ReadAllLines(TrainingDataFile)
                                        .Skip(ContainsHeader ? 1 : 0)
                       let columns = lines.Split(new[] { FieldSeparator }).ToList<String>()
                       let target = columns[TargetVariablePositionZeroBased]
                       let features = GetFeaturesAfterPruning(columns, TargetVariablePositionZeroBased)
                       select new Tuple<List<String>, String>(features, target);




            List<int> UsedFeatures = new List<int>();
            Root = GetID3Node(data, UsedFeatures);
        }


        public static List<String> GetFeaturesAfterPruning(List<String> Features, int TargetVariablePositionZeroBased)
        {
            Features.RemoveAt(TargetVariablePositionZeroBased);
            return Features;
        }

        public ID3Node GetID3Node(IEnumerable<Tuple<List<String>, String>> data, List<int> UsedFeatures)
        {
            ID3Node node = new ID3Node();
            node.TargetClassProbabilities = TargetClassProbabilities(data);
            node.Entropy = Entropy(node.TargetClassProbabilities);

            if (node.Entropy > 0)
            {
                node.DecisionVariableIndex = GetDecisionVariable(node, data, UsedFeatures);

                if (node.DecisionVariableIndex != -1)
                {
                    List<String> DecisionVariableValues = ((from tuple in data
                                                  select tuple.Item1[node.DecisionVariableIndex]).Distinct()).ToList<String>();
                    
                    if (DecisionVariableValues.Count > 1)
                    {
                        node.Children = new Dictionary<string, ID3Node>();

                        UsedFeatures.Add(node.DecisionVariableIndex);
                        foreach (String DecisionVariableValue in DecisionVariableValues)
                        {
                            var subset = from tuple in data
                                         where tuple.Item1[node.DecisionVariableIndex] == DecisionVariableValue
                                         select tuple;
                            node.Children[DecisionVariableValue] =  GetID3Node(subset,UsedFeatures);
                        }
                        UsedFeatures.Remove(node.DecisionVariableIndex);
                    }
                }
            }

            return node;
        }


        public int GetDecisionVariable(ID3Node node, IEnumerable<Tuple<List<String>, String>> data, List<int> UsedFeatures)
        {
            int NumFeatures = data.First().Item1.Count;

            if (UsedFeatures.Count < NumFeatures)
            {
                Dictionary<int, double> Gain = new Dictionary<int, double>();

                for (int i = 0; i < NumFeatures; i++)
                {
                    if (UsedFeatures.Contains(i))
                        continue;

                    Gain[i] = InformationGain(node.Entropy, data, i);
                }

                double MaxGain = Gain.Values.Max();
                foreach (int i in Gain.Keys)
                {
                    if (Gain[i] == MaxGain)
                        return i;
                }
            }
            return -1;
        }

        public  Dictionary<String, Double> TargetClassProbabilities(IEnumerable<Tuple<List<String>, String>> data)
        {
            var TargetClassFrequencies = from tuple in data
                                         group tuple by tuple.Item2 into grp
                                         select new
                                         {
                                             TargetClass = grp.Key,
                                             NumRecords = grp.Count()
                                         };

            Dictionary<String, Double> Probabilities = new Dictionary<string, double>();

            foreach (var TargetClassFrequency in TargetClassFrequencies)
            {
                Probabilities[TargetClassFrequency.TargetClass] = TargetClassFrequency.NumRecords;
            }

            double Total = Probabilities.Values.Sum();

            foreach (String TargeteClass in Probabilities.Keys)
            {
                Probabilities[TargeteClass] = Probabilities[TargeteClass] / Total;
            }

            return Probabilities;

        }


        public  double Entropy(Dictionary<String, Double> Probabilities)
        {
            double Entropy = 0;

            foreach (double Probability in Probabilities.Values)
            {
                Entropy += (Probability * Math.Log(Probability, 2));
            }
            return -Entropy;
        }


        public  double InformationGain(double GlobalEntropy, IEnumerable<Tuple<List<String>, String>> data, int CandidateFeatureIndex)
        {
            double InformationGain = GlobalEntropy;

            List<String> FeatureValues = (from tuple in data
                                          select tuple.Item1[CandidateFeatureIndex])
                                         .Distinct().ToList<String>();

            int UniversalSetCount = data.Count();

            foreach (String FeatureValue in FeatureValues)
            {
                var subset = from tuple in data
                             where tuple.Item1[CandidateFeatureIndex] == FeatureValue
                             select tuple;

                Dictionary<String, double> Probabilities = TargetClassProbabilities(subset);


                double FeatureValueEntropy = Entropy(Probabilities);
                int SubsetCount = subset.Count();

                InformationGain = InformationGain - ((SubsetCount * 1.0 / UniversalSetCount) * (FeatureValueEntropy / GlobalEntropy));
            }

            return InformationGain;

        }






    }

 
    public class ID3Client
    {

        public static void Run()
        {
            //IDecisionTree dt = new ID3(@"C:\Users\shravanr\labs\Algorithms\Algorithms\MachineLearning\ID3SampleTrainingData.txt",',',true,3);
            //List<String> s = new List<String>() { "a", "s","r","t","y"};

            /*
            IEnumerable<Tuple<List<String>, String>> data =
                       from lines in File.ReadAllLines(@"C:\Users\shravanr\labs\Algorithms\Algorithms\MachineLearning\ID3SampleTrainingData.txt")
                                        //.Skip(1)
                       let features = lines.Split(new[] { ',' }).ToList<String>()
                       let target = features[3]                       
                       select new Tuple<List<String>, String>(features, target);


            Console.WriteLine(data.First().Item1.Count);

            foreach (var r in data)
            {
                Console.WriteLine(r.Item1[0]);
            }*/

        }
    }
}
