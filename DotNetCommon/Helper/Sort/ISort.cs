using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper.Sort
{
    interface ISort<T>
    {
        void Sort(T[] array);
        void Sort(T[] array, IComparer<T> comparer);
    }
}
