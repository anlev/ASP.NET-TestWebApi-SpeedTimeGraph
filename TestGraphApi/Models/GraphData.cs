using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGraphApi.Models
{
    public class GraphData
    {
        public SpeedTime[] Points { get; set; }

        
        public Range GetSpeedRange()
        {
            return new Range { MinValue = Points.Min(x => x.Speed), MaxValue = Points.Max(x => x.Speed) };
        }

        public Range GetTimeRange()
        {
            return new Range { MinValue = Points.Min(x => x.Time), MaxValue = Points.Max(x => x.Time) };
        }
    }
}