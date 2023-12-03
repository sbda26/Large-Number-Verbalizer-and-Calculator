using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;




namespace LNVC_Library
{
    public class SpecialConstantsClass
    {
        public readonly static Dictionary<string, string> Primes = new()
        {
            ["Catalan's"] = "170141183460469231731687303715884105727", // larget prime ever found by hand
            ["Ferrier's"] = "20988936657440586486151264256610222593863921", // 1951 with mechanical desk calculator
            ["Largest found by ECM factorization"] = "4444349792156709907895752551798631908946180608768737946280238078881"
        };

        public readonly static Dictionary<string, string> RubicksCubes = new()
        {
            ["3x3"] = "101097362223624462291180422369532000000",
            ["4x4"] = "7401196841564901869874093974498574336000000000",
            ["5x5"] = "282870942277741856536180333107150328293127731985672134721536000000000000000",
            ["6x6"] = "157152858401024063281013959519483771508510790313968742344694684829502629887168573442107637760000000000000000000000000",
            ["7x7"] = "19500551183731307835329126754019748794904992692043434567152132912323232706135469180065278712755853360682328551719137311299993600000000000000000000000000000000000"
        };

        public readonly static Dictionary<string, string> Miscellaneous = new ()
        {
            ["Smallest '6-perfect' Number"] = "154345556085770649600",
            ["9x9 Sudoku Grids"] = "6670903752021072936960",
            ["Avogadro (approx.)"] = "602214150000000000000000",
            ["Sum of 10th Powers From 1 to 1000"] = "1409924241424243424241924242500",
            ["Largest Perfect Square With All Perfect Square Digits (believed)"] = "419994999149149944149149944191494441",
            ["Largest Armstrong Number (base 10)"] = "115132219018763992565095597973971522401",
            ["Number of IPv6 Addresses (2^128)"] = "340282366920938463463374607431768211456",
            ["Zài"] = PowerOf10(44),
            ["L-Cillion"] = PowerOf10(50),
            ["Eddington Number"] = "31495448272550005155211307922363110936089435829054233418732462850152371262062592",
            ["Googol"] = PowerOf10(100),
            ["Eleventyplex"] = PowerOf10(110),
            ["Asankhyeya"] = PowerOf10(140),
            ["Gargoogol"] = PowerOf10(200),
            ["Mentaggoogol"] = PowerOf10(210),
            ["Bentaggoogol"] = PowerOf10(420),
            ["Tentaggoogol"] = PowerOf10(630),
            ["Megafugafour"] = PowerOf10(1530),
            ["Googood"] = PowerOf10(3000)
        };

        private static string PowerOf10(int power) => BigInteger.Pow(10, power).ToString();


    }
}
