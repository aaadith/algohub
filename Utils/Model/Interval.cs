using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Model
{
    public enum IntervalBoundaryType
    {
        Open, // Boundary point not included in interval
        Closed // Boundary point included in interval
    }

    public class Interval<T> : IComparable<Interval<T>> where T : IComparable
    {
        public T Start { get; set; }

        public T End { get; set; }

        public IntervalBoundaryType StartType { get; set; }

        public IntervalBoundaryType EndType { get; set; }

        public Interval(T Start, T End, IntervalBoundaryType StartType = IntervalBoundaryType.Open, IntervalBoundaryType EndType = IntervalBoundaryType.Open)
        {
            if(Start.CompareTo(End)>0)
                throw new ArgumentException("start greater than end");

            this.Start = Start;
            this.End = End;
            this.StartType = StartType;
            this.EndType = EndType;

        }

        

        public bool Contains(T Point)
        {
            int StartComparisonWithPoint = Start.CompareTo(Point);
            int EndComparisonWithPoint = End.CompareTo(Point);

            if(StartComparisonWithPoint > 0 && EndComparisonWithPoint < 0)
                return true;

            if(StartComparisonWithPoint == 0 && StartType == IntervalBoundaryType.Closed)
                return true;

            if(EndComparisonWithPoint == 0 && EndType == IntervalBoundaryType.Closed)
                return true;
            
            return false;
        }

        public bool Overlaps(Interval<T> Other)
        {
            return Overlaps(this, Other) || Overlaps(Other, this);
        }

        bool Overlaps(Interval<T> a, Interval<T> b)
        {

            int bStartWithAStart = b.Start.CompareTo(a.Start);
            int bStartWithAEnd = b.Start.CompareTo(a.End);
            int bEndWithAStart = b.End.CompareTo(a.Start);
            int bEndWithAEnd = b.End.CompareTo(a.End);

            //if start of b is in a's interval
            if (bStartWithAStart > 0 && bStartWithAEnd < 0)
                return true;

            //if end of b is in a's interval
            if (bEndWithAStart > 0 && bEndWithAEnd < 0)
                return true;

            //check if both intervals claim the boundary point

            //if start of b coincides with start of a
            if (bStartWithAStart==0)
            {
                return (a.StartType == IntervalBoundaryType.Closed && b.StartType == IntervalBoundaryType.Closed);
            }

            //if start of b coincides with end of a
            if (bStartWithAEnd==0)
            {
                return (b.StartType == IntervalBoundaryType.Closed && a.EndType == IntervalBoundaryType.Closed);
            }

            //if end of b coincides with start of a
            if (bEndWithAStart==0)
            {
                return (b.EndType == IntervalBoundaryType.Closed && a.StartType == IntervalBoundaryType.Closed);
            }

            //if end of b coincides with end of a
            if (bEndWithAEnd==0)
            {
                return (b.EndType == IntervalBoundaryType.Closed && b.EndType == IntervalBoundaryType.Closed);
            }

            return false;
        }

        public int CompareTo(Interval<T> other)
        {
            return this.End.CompareTo(other.End);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}, {2}{3}", StartType == IntervalBoundaryType.Open ? '(' : '[', Start, End, (EndType == IntervalBoundaryType.Open) ? ')' : ']');
        }
    }

}
