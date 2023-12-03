using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LNVC_Library
{
    // Recently added a thousand times smaller than yocto is ronto and a million times smaller than yocto is quecto. Use the prefix “nano” and you make any unit a billion times smaller. A nanometre? That's a billionth of a metre.Mar 27, 2019
    // A zeptosecond is a trillionth of a billionth of a second. That's a decimal point followed by 20 zeroes and a 1, and it looks like this: 0.000 000 000 000 000 000 001. The only unit of time shorter than a zeptosecond is a yoctosecond, and Planck time. A yoctosecond (ys) is a septillionth of a second.Oct 22, 2020

    public class TimeUnitsClass
    {
        public BigInteger Year { get; set; }
        public BigInteger Day { get; set; }
        public BigInteger Hour { get; set; }
        public BigInteger Minute { get; set; }
        public BigInteger Second { get; set; }
        /*
        public BigInteger MilliSecond { get; set; }
        public BigInteger MicroSecond { get; set; }
        public BigInteger NanoSecond { get; set; }
        public BigInteger PicoSecond { get; set; }
        public BigInteger FemtoSecond { get; set; }
        public BigInteger AttoSecond { get; set; }
        public BigInteger ZeptoSecond { get; set; }
        public BigInteger YoctoSecond { get; set; }
        public BigInteger RontoSecond { get; set; }
        public BigInteger QuectoSecond { get; set; }
        */
    }
 }
