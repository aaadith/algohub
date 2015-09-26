using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    /*
    
     * Problem statement:
     * Given N tasks, find the maximal points that can be achieved by finishing them 

    Constraints 
    There are T minutes for completing N tasks 
    Solutions can be submitted at any time, including exactly T minutes after the start 
    i-th task submitted t minutes after the start, will get maxPoints[i] - t * pointsPerMinute[i] points 
    i-th task takes requiredTime[i] minutes to solve 
 
    */


    public class MaxPointsForTaskCompletion
    {
        int[] MaxPoints;
        int[] PointsPerMinute;
        int[] RequiredTime;
        int GetPoints(int t, int i)
        {
            return MaxPoints[i] - t * PointsPerMinute[i];
        }

        //remaining tasks
        Dictionary<Tuple<int, List<int>>, int> lookup = new Dictionary<Tuple<int, List<int>>, int>();
        int GetMaxPoints(int remainingTime, List<int> remainingTasks)
        {
            Tuple<int, List<int>> key = new Tuple<int, List<int>>(remainingTime, new List<int>(remainingTasks));

            if (lookup.ContainsKey(key))
                return lookup[key];

            int maxPoints = 0;
            foreach (int currentTask in remainingTasks.ToArray())
            {
                if (remainingTime < RequiredTime[currentTask])
                    continue;
                int points = GetPoints(remainingTime - RequiredTime[currentTask], currentTask);
                remainingTasks.Remove(currentTask);

                points += GetMaxPoints(remainingTime - RequiredTime[currentTask], remainingTasks);
                remainingTasks.Add(currentTask);
                if (points > maxPoints)
                    maxPoints = points;
            }

            lookup[key] = maxPoints;
            return maxPoints;
        }
    }

    public class Comparer : IEqualityComparer<Tuple<int, List<int>>>
    {

        public bool Equals(Tuple<int, List<int>> x, Tuple<int, List<int>> y)
        {
            return x.Item1 == y.Item1 && x.Item2.SequenceEqual(y.Item2);
        }

        public int GetHashCode(Tuple<int, List<int>> obj)
        {
            int hashcode = obj.Item1;
            foreach (int t in obj.Item2)
            {
                hashcode ^= t.GetHashCode();
            }
            return hashcode;
        }
    }

}
