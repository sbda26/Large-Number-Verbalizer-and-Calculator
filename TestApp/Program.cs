using java.math;
using System.Globalization;
using System.Numerics;
using System.Reflection;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            double xxx = BigInteger.Log10(BigInteger.Parse("100000000000000000000000000000000000000"));
            Console.WriteLine(xxx.ToString());
            */

            double number = 2983479287.234;

            Console.WriteLine(number);
            Console.Write("E1:\t");
            Console.WriteLine(number.ToString("E1", CultureInfo.InvariantCulture));
            Console.Write("E2:\t");
            Console.WriteLine(number.ToString("E2", CultureInfo.InvariantCulture));
            Console.Write("E3:\t");
            Console.WriteLine(number.ToString("E3", CultureInfo.InvariantCulture));
            Console.Write("E4:\t");
            Console.WriteLine(number.ToString("E4", CultureInfo.InvariantCulture));
            /*
            BigDecimal xx;
            var arg1 = "1000000000000000000000000000000000";
            xx = new BigDecimal(new BigDecimal(arg1));
            Console.WriteLine(xx.pow(-2, MathContext.DECIMAL128));
            */

            for (int x = 1; x <= 30; x++)
                Console.WriteLine(Random.Shared.Next(1, 10));

            Console.WriteLine("------------------");

            Person person = new Person();
            person.Age = 27;
            person.Name = "Fernando Vezzali";

            Type type = typeof(Person);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine("{0} = {1}", property.Name, property.GetValue(person, null));
            }

            Console.Read();
        }
    }
}