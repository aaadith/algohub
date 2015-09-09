using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataStructures
{
    public class Heap<T>
    {
        IComparer<T> comparer;

        T[] list;

        int count;

        public Heap()
        {
            count = 0;
            list = new T[10];
        }

        public Heap(IComparer<T> comparer)
            : this()
        {
            this.comparer = comparer;
        }

        private int Compare(T a, T b)
        {
            if (comparer != null)
                return comparer.Compare(a, b);
            //if(typeof(T).IsAssignableFrom(typeof(IComparable)))
            if (a is IComparable)
                return ((IComparable)a).CompareTo(b);
            else
                throw new Exception("no mechanism found for comparison of type");
        }

        public void HeapUp(int position)
        {
            while (position > 0)
            {
                int parentPosition = (position + 1) / 2 - 1;
                T current = list[position];
                T parent = list[parentPosition];

                if (Compare(current, parent) < 0)
                {
                    ArrayUtils.Swap<T>(list, position, parentPosition);
                    position = parentPosition;
                }
                else
                    break;
            }
        }

        public void HeapDown(int position)
        {
            while (position < count)
            {
                T current = list[position];
                int child1Position = (position + 1) * 2 - 1;
                int child2Position = child1Position + 1;

                if (child1Position >= list.Length)
                    break;

                T child1 = list[child1Position];

                if (child2Position >= list.Length)
                {
                    if (Compare(current, child1) < 0)
                    {
                        ArrayUtils.Swap<T>(list, position, child1Position);
                        position = child1Position;
                    }
                }
                else
                {
                    T child2 = list[child2Position];

                    if (Compare(child1, child2) < 0)
                    {
                        if (Compare(current, child1) < 0)
                        {
                            ArrayUtils.Swap<T>(list, position, child1Position);
                            position = child1Position;
                        }
                    }
                    else
                    {
                        if (Compare(current, child2) < 0)
                        {
                            ArrayUtils.Swap<T>(list, position, child2Position);
                            position = child2Position;
                        }
                    }
                }
            }
        }

        public void Add(T newElement)
        {
            if (count == list.Length)
                Array.Resize<T>(ref list, (int)(list.Length * 1.5));

            list[count] = newElement;
            HeapUp(count);
            count++;
        }

        void Delete(int position)
        {
            list[position] = list[count - 1];
            count--;
            HeapDown(position);
        }

        public T Pop()
        {
            T result = list[0];
            Delete(0);
            return result;
        }

    }

}
