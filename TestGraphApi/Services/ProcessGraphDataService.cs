using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TestGraphApi.Models;

namespace TestGraphApi.Services
{
    public class ProcessGraphDataService
    {
        public SpeedTime[] SortPoints_Linq(SpeedTime[] points)
        {
            return points.OrderBy(x => x.Time).ToArray();
        }

        public SpeedTime[] SortPoints_MyQuickSort(SpeedTime[] points)
        {
            var pointsSorted = (SpeedTime[])points.Clone();
            QuickSortHelpers.MyQuickSort(pointsSorted, 0, points.Length - 1, (x, y) => x.Time.CompareTo(y.Time));
            //QuickSortHelpers.MyQuickSort(pointsSorted, 0, points.Length - 1);

            return pointsSorted;
        }

        public SpeedTime[] OptimizeSortedPoints(SpeedTime[] points, double optimizeFactor)
        {
            var optimizedPoints = points.ToList();
            int deletedCounter = 0;

            for (int i = 0; i < optimizedPoints.Count - 1; i++)
            {
                double d = Math.Pow((points[i + 1].Speed - points[i].Speed), 2) + Math.Pow((points[i + 1].Time - points[i].Time), 2);
                if (d < optimizeFactor)
                {
                    optimizedPoints.RemoveAt(i);
                    deletedCounter++;
                }

            }
            Debug.Print(deletedCounter.ToString());
            return optimizedPoints.ToArray();
        }
    
    }
}