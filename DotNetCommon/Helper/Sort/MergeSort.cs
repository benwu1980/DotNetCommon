using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper.Sort
{
    /// <summary>
    /// 归并排序
    /// </summary>
    public class MergeSort<T> : ISort<T>
    {

        public void Sort(T[] array, IComparer<T> comparer)
        {
            Guard.ArgumentNotNull(comparer, "comparer");
            MergeSortCore(array, comparer, 0, array.Length - 1);
        }

        public void Sort(T[] array)
        {
            MergeSortCore(array, Comparer<T>.Default, 0, array.Length - 1);
        }

        private static void MergeSortCore<T>(T[] array, IComparer<T> comparer, int fromPos, int toPos)
        {
            Guard.ArgumentNotNull(array, "list");
            Guard.ArgumentNotNull(comparer, "comparer");

            if (fromPos < toPos)
            {
                int mid = (fromPos + toPos) / 2;

                MergeSortCore(array, comparer, fromPos, mid);
                MergeSortCore(array, comparer, mid + 1, toPos);

                int endLow = mid;
                int startHigh = mid + 1;

                while (fromPos <= endLow & startHigh <= toPos)
                {
                    if (comparer.Compare(array[fromPos], array[startHigh]) < 0)
                    {
                        fromPos++;
                    }
                    else
                    {
                        T temp = array[startHigh];
                        for (int index = startHigh - 1; index >= fromPos; index--)
                        {
                            array[index + 1] = array[index];
                        }

                        array[fromPos] = temp;
                        fromPos++;
                        endLow++;
                        startHigh++;
                    }
                }
            }
        }


      
    }
}
