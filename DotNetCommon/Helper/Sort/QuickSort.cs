using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper.Sort
{

    /// <summary>
    /// 快速排序
    /// </summary>
    public class QuickSort<T> : ISort<T>
    {
        #region ISort Members

        public void Sort(T[] array)
        {
            sort(array, 0, array.Length - 1, Comparer<T>.Default);
        }

        public void Sort(T[] array, IComparer<T> comparer)
        {
            sort(array, 0, array.Length - 1, comparer);
        }

        private int sort(T[] A, int first_index, int end_index, IComparer<T> comparer)
        {
            if (first_index < end_index)
            {
                int part_index = partition(A, first_index, end_index, comparer);
                sort(A, first_index, part_index - 1, comparer);
                sort(A, part_index + 1, end_index, comparer);
            }
            return 0;
        }


        private int partition(T[] array, int first_index, int end_index, IComparer<T> comparer)
        {
            int i = first_index;
            int j = first_index;
            for (; j < end_index; j++)
            {
                if (comparer.Compare(array[j - 1], array[end_index - 1]) > 0)
                {
                    Exchange(array, i, j);
                    i++;
                }
            }
            Exchange(array, i, end_index);
            return i;
        }

        private void Exchange(T[] array, int a, int b)
        {
            T temp;
            temp = array[a - 1];
            array[a - 1] = array[b - 1];
            array[b - 1] = temp;
        }

        #endregion
    }
}
