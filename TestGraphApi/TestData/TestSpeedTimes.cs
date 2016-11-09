using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestGraphApi.Models;

namespace TestGraphApi.TestData
{
    public class TestSpeedTimes : AbstractTestData<SpeedTime>
    {
        const double maxSpeed = 100f;
        const double maxTime = 100f;

        protected override IEnumerable<SpeedTime> Generate()
        {
            var random = new Random();

            while (true)
            {
                yield return new SpeedTime
                {
                    Time = random.NextDouble() * maxSpeed,
                    Speed = random.NextDouble() * maxTime
                };
            }
        }
    }
}