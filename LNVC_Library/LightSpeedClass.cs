using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LNVC_Library
{
    public class LightSpeedClass
    {
        public readonly static Dictionary<string, TimeUnitsClass> LightSpeedValues_Metric = GetLightSpeedValues_Metric();
        public readonly static Dictionary<string, TimeUnitsClass> LightSpeedValues_Imperial = GetLightSpeedValues_Imperial();

        private static Dictionary<string, TimeUnitsClass> GetLightSpeedValues_Metric()
        {
            BigInteger kilometersSecond_Rounded = 299792;
            BigInteger value = 299792458;   // meters per second
            Dictionary<string, TimeUnitsClass> values = new();

            values.Add("Kilometers", DistancesInTimes(kilometersSecond_Rounded));
            values.Add("Meters", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Centimeters", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Millimeters", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Micrometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Nanometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Picometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Femtometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Attometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Zeptometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Yoctometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("Rontometers", DistancesInTimes(value));
            MultiplyValue1000(ref value);
            values.Add("QuectoMeters", DistancesInTimes(value));

            return values;
        }

        private static Dictionary<string, TimeUnitsClass> GetLightSpeedValues_Imperial()
        {
            BigInteger value = 186282;  // miles per second
            Dictionary<string, TimeUnitsClass> values = new();

            values.Add("Miles", DistancesInTimes(value));
            value *= 5280;
            values.Add("Feet", DistancesInTimes(value));
            value *= 12;
            values.Add("Inches", DistancesInTimes(value));

            return values;
        }

        private static TimeUnitsClass DistancesInTimes(BigInteger secondValue)
        {
            TimeUnitsClass times = new();

            times.Second = secondValue;
            secondValue *= 60;
            times.Minute = secondValue;
            secondValue *= 60;
            times.Hour = secondValue;
            secondValue *= 24;
            times.Day = secondValue;
            secondValue *= 365;
            times.Year = secondValue;

            return times;
        }

        private static void MultiplyValue1000(ref BigInteger value)
        {
            value *= 1000;
        }
    }
}
