using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGraphApi.Models
{
    public struct SpeedTime //: IComparable<SpeedTime>
    {
        public double Speed { get; set; }
        public double Time { get; set; }


       /* public int CompareTo(SpeedTime other)
        {
            if (other == null)
                return 1;
           
            return Time.CompareTo(other.Time);
        }*/
    }
}