using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TestGraphApi.Models;

namespace TestGraphApi.Services
{
    public static class QuickSortHelpers
    {
        public static void MyQuickSort<T>(T[] m, int a, int b, Comparison<T> comparison)
        {
            if (a >= b) return;
            int c = Partition(m, a, b, comparison);
            MyQuickSort(m, a, c - 1, comparison);
            MyQuickSort(m, c + 1, b, comparison);
        }

        private static int Partition<T>(T[] m, int a, int b, Comparison<T> comparison)
        {
            int i = a;
            for (int j = a; j <= b; j++)         
            {
                if (comparison(m[j], m[b]) <= 0)  
                {
                    T t = m[i];                  
                    m[i] = m[j];                
                    m[j] = t;                    
                    i++;                         
                }
            }
            return i - 1;                       
        }

        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> collection, Comparison<T> comparison)
        {
            if (!collection.Any())
            {
                return Enumerable.Empty<T>();
            }
            else
            {
                var array = collection.ToArray();
                Array.Sort(array, comparison);
                return array;
            }
        }



        // Use if model IComparable in model is allowed

        /*
        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (!collection.Any())
            {
                return Enumerable.Empty<T>();
            }
            else
            {
                var array = collection.ToArray();
                Array.Sort(array, (x, y) => x.CompareTo(y));
                return array;
            }
        }


        public static void MyQuickSort<T>(T[] m, int a, int b) where T : IComparable<T>
        {
            if (a >= b) return;
            int c = Partition(m, a, b);
            MyQuickSort(m, a, c - 1);
            MyQuickSort(m, c + 1, b);
        }

        private static int Partition<T>(T[] m, int a, int b) where T : IComparable<T>
        {
            int i = a;
            for (int j = a; j <= b; j++)
            {
                if (m[j].CompareTo(m[b]) <= 0)
                {
                    T t = m[i];
                    m[i] = m[j];
                    m[j] = t;
                    i++;
                }
            }
            return i - 1;
        }
                */
    }
}