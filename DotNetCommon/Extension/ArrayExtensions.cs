using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Extension
{
    /// <summary>
    /// 数组的扩展
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///  To clear an array of type T.
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="arrayToClear">The array to clear.</param>
        /// <returns>Cleared array.</returns>
        /// <example>
        ///     <code>
        ///         int[] myArray = { 1, 2, 3, 4, 5, 6, 7 };
        ///         int[] result = myArray.ClearAll<int>();
        ///     </code>
        /// </example>
        /// Contributed by Mohammad Rahman
        public static T[] ClearAll<T>(this T[] array)
        {

            if (array.IsNotEqualToNull())
                for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); ++i)
                    array[i] = default(T);
            return array;
        }

        /// <summary>
        /// To clear a item at a specified position in the given array.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="arrayToClear">Array to clear</param>
        /// <param name="at">The position of the item which to clear.</param>
        /// <returns>The cleared array.</returns>
        /// <example>
        ///     <code>
        ///         int[] myArray = { 1, 2, 3, 4, 5, 6, 7 };
        ///         int[] result = myArray.ClearAt<int>(3);
        ///     </code>
        /// </example>
        /// Contributed by Mohammad Rahman
        public static T[] ClearAt<T>(this T[] array, int at)
        {
            if (array.IsNotEqualToNull())
            {
                if(at>0 && at<array.Length)
                {
                     array[at] = default(T);
                }
            }
            return array;
        }

 

        /// <summary>
        /// To combine two same type arrray.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="combineWith">The array to combine with.</param>
        /// <param name="arrayToCombine">The array to combine.</param>
        /// <returns>The combined array.</returns>
        /// <example>
        ///     <code>
        ///         int[] myArray1 = { 1, 2, 3, 4, 5, 6, 7 };
        ///         int[] myArray2 = { 1, 2, 3, 4, 5, 6, 7 };
        ///         int[] result = myArray1.CombineArray<int>(myArray2);
        ///     </code>
        /// </example>
        /// Contributed by Mohammad Rahman
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                int initialSize = combineWith.Length;
                Array.Resize<T>(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }

        public static bool IsNotEqualToNull<T>(this T[] array)
        {
            return true;
        }
    }
}
