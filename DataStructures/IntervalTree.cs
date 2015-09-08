using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace DataStructures
{
    public class IntervalTreeNode<T> where T : IComparable
    {
        public T median { get; set; }

        // public Interval<T>[] intervals { get { return intervalsByStart; } }

        public Interval<T>[] intervalsByStart { get; set; }

        public Interval<T>[] intervalsByEnd { get; set; }

        public IntervalTreeNode<T> left { get; set; }

        public IntervalTreeNode<T> right { get; set; }

        public IntervalTreeNode(T median, Interval<T>[] intervals)
        {
            this.median = median;

            intervalsByStart = new Interval<T>[intervals.Length];
            Array.Copy(intervals, intervalsByStart, intervals.Length);
            Array.Sort(intervalsByStart, (a, b) => a.Start.CompareTo(b.Start));

            intervalsByEnd = new Interval<T>[intervals.Length];
            Array.Copy(intervals, intervalsByEnd, intervals.Length);
            Array.Sort(intervalsByStart, (a, b) => b.End.CompareTo(a.End));


        }


        public IEnumerable<Interval<T>> GetIntervals(T point)
        {
            List<Interval<T>> result = new List<Interval<T>>();

            int pointVsMedian = point.CompareTo(median);

            if (pointVsMedian == 0)
                result = this.intervalsByStart.ToList<Interval<T>>();
            else
            {
                if (pointVsMedian < 0)
                {
                    for (int i = 0; i < intervalsByStart.Length; i++)
                    {
                        if (intervalsByStart[i].Contains(point))
                            result.Add(intervalsByStart[i]);
                        else
                            break;
                    }

                    IEnumerable<Interval<T>> leftResult = left.GetIntervals(point);
                    foreach (Interval<T> interval in leftResult)
                        result.Add(interval);
                }
                else
                {
                    for (int i = 0; i < intervalsByStart.Length; i++)
                    {
                        if (intervalsByEnd[i].Contains(point))
                            result.Add(intervalsByEnd[i]);
                        else
                            break;
                    }
                    IEnumerable<Interval<T>> rightResult = right.GetIntervals(point);
                    foreach (Interval<T> interval in rightResult)
                        result.Add(interval);
                }
            }

            return result;
        }

        public IEnumerable<Interval<T>> GetIntervals(Interval<T> interval)
        {
            List<Interval<T>> result = new List<Interval<T>>();

            if (interval.Contains(median))
            {
                foreach (Interval<T> current in intervalsByStart.ToList<Interval<T>>())
                    result.Add(current);

                IEnumerable<Interval<T>> leftResult = left.GetIntervals(interval);
                foreach (Interval<T> current in leftResult)
                    result.Add(current);


                IEnumerable<Interval<T>> rightResult = right.GetIntervals(interval);
                foreach (Interval<T> current in rightResult)
                    result.Add(current);

            }
            else
            {
                if (interval.End.CompareTo(median) < 0)
                {
                    for (int i = 0; i < intervalsByStart.Length; i++)
                    {
                        if (intervalsByStart[i].Overlaps(interval))
                            result.Add(intervalsByStart[i]);
                        else
                            break;
                    }

                    IEnumerable<Interval<T>> leftResults = left.GetIntervals(interval);
                    foreach (Interval<T> leftResult in leftResults)
                        result.Add(leftResult);
                }
                else
                {
                    for (int i = 0; i < intervalsByEnd.Length; i++)
                    {
                        if (intervalsByEnd[i].Overlaps(interval))
                            result.Add(intervalsByEnd[i]);
                        else
                            break;
                    }

                    IEnumerable<Interval<T>> rightResults = right.GetIntervals(interval);
                    foreach (Interval<T> rightResult in rightResults)
                        result.Add(rightResult);
                }
            }

            return result;
        }
    }

    public class IntervalTreeBuilder<T> where T : IComparable
    {
        public IntervalTreeNode<T> GetIntervalTree(IEnumerable<Interval<T>> intervals)
        {
            if (intervals == null)
                return null;

            T[] pointsArray = GetPoints(intervals);
            T median = ArrayUtils.GetMedian<T>(pointsArray);

            IList<Interval<T>> left = new List<Interval<T>>();
            IList<Interval<T>> right = new List<Interval<T>>();

            IList<Interval<T>> current = new List<Interval<T>>();

            foreach (Interval<T> interval in intervals)
            {
                if (interval.Contains(median))
                    current.Add(interval);
                else
                {
                    if (median.CompareTo(interval.Start) < 0)
                        left.Add(interval);
                    else
                        right.Add(interval);
                }
            }

            Interval<T>[] currentIntervals = current.ToArray<Interval<T>>();
            Array.Sort(currentIntervals);

            IntervalTreeNode<T> currentNode = new IntervalTreeNode<T>(median, currentIntervals);

            currentNode.left = GetIntervalTree(left);
            currentNode.right = GetIntervalTree(right);

            return currentNode;
        }

        T[] GetPoints(IEnumerable<Interval<T>> intervals)
        {
            HashSet<T> points = new HashSet<T>();

            foreach (Interval<T> interval in intervals)
            {
                points.Add(interval.Start);
                points.Add(interval.End);
            }

            T[] pointsArray = points.ToArray<T>();
            Array.Sort(pointsArray);
            return pointsArray;
        }


    }
}
