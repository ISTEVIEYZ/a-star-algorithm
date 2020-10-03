using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch.Collections
{
    [Serializable]
    public class MinimumHeap<T>
    {
        private Tuple<decimal, T>[] heap;

        public Func<T, T, double> TieBreakFunction { get; set; }

        private int maxSize;
        private int lastIndex;

        public bool IsEmpty { get { return lastIndex < 1; } }

        public decimal MinValue { get { return heap[1] == null ? decimal.MaxValue : heap[1].Item1; } }

        public T Top { get { return heap[1].Item2; } }

        public MinimumHeap(int size)
        {
            heap = new Tuple<decimal, T>[size + 1];
            lastIndex = 0;
            maxSize = size + 1;
        }

        public void Push(T item, decimal priority)
        {
            if (lastIndex + 1 < maxSize)
            {
                heap[++lastIndex] = new Tuple<decimal, T>(priority, item);
                bubbleUp(lastIndex);
            }
            else
                throw new HeapOverflowException(string.Format("Heap is full: Num Items: {0}", lastIndex));
        }

        public T Pop()
        {
            if (lastIndex > 0)
            {
                T ret = heap[1].Item2;
                heap[1] = null;
                swap(1, lastIndex--);
                sinkDown(1);
                return ret;
            }
            else
                throw new HeapUnderflowException("Heap is empty");
        }

        public void Remove(T item)
        {
            int index = -1;
            for (int i = 1; i < maxSize; i++)
                if (heap[i].Item2.Equals(item))
                {
                    index = i;
                    break;
                }

            if (index != -1)
            {
                heap[index] = heap[lastIndex];
                heap[lastIndex--] = null;
                sinkDown(index);
            }
            else
                throw new Exception("Heap item not found");

        }

        private void bubbleUp(int i)
        {
            while (i > 1 && heap[i].Item1 < heap[i / 2].Item1)
                swap(i, i /= 2);
        }

        private void sinkDown(int i)
        {
            int lc = i * 2;
            int rc = i * 2 + 1;

            decimal lcmp = -1;
            decimal rcmp = -1;

            if (lc <= lastIndex)
                lcmp = heap[i].Item1 - heap[lc].Item1;
            if (rc <= lastIndex)
                rcmp = heap[i].Item1 - heap[rc].Item1;

            
            if (rcmp > 0 && rcmp > lcmp)
            {
                swap(rc, i);
                sinkDown(rc);
            }
            else if (lcmp > 0)
            {
                swap(lc, i);
                sinkDown(lc);
            }
            else if (rcmp == 0 || lcmp == 0)
            {
                double tiebreaklc = -1;
                double tiebreakrc = -1;

                if (lc <= lastIndex)
                    tiebreaklc = TieBreakFunction(heap[i].Item2, heap[lc].Item2);
                if (rc <= lastIndex)
                    tiebreakrc = TieBreakFunction(heap[i].Item2, heap[rc].Item2);

                if (rcmp == 0 && lcmp == 0)
                {
                    if (tiebreaklc > 0 && tiebreaklc > tiebreakrc)
                    {
                        swap(i, lc);
                        sinkDown(lc);
                    }
                    else if (tiebreakrc > 0)
                    {
                        swap(i, rc);
                        sinkDown(rc);
                    } 
                }
                else if (rcmp == 0 && tiebreakrc > 0)
                {
                    swap(i, rc);
                    sinkDown(rc);
                }
                else if (lcmp == 0 && tiebreaklc > 0)
                {
                    swap(i, lc);
                    sinkDown(lc);
                }
            }
        }

        private void swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }

    public class HeapOverflowException : System.Exception
    {
        public HeapOverflowException(string message) : base(message) { }
    }

    public class HeapUnderflowException : System.Exception
    {
        public HeapUnderflowException(string message) : base(message) { }
    }
}