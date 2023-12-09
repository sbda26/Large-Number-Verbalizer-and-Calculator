//using com.sun.xml.@internal.fastinfoset.sax;
//using java.math;
//using javax.sound.midi;
//using java.util;
using Microsoft.Extensions.Configuration;
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

            Console.WriteLine("------------------");

            BuildConfig();

            Console.Read();
        }

        private static void BuildConfig()
        {
            // Build a config object, using env vars and JSON providers.
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            // Get values from the config given their key and their target type.
            Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

            // Write the values to the console.
            Console.WriteLine($"KeyOne = {settings?.KeyOne}");
            Console.WriteLine($"KeyTwo = {settings?.KeyTwo}");
            Console.WriteLine($"KeyThree:Message = {settings?.KeyThree?.Message}");

            // Application code which might rely on the config could start here.

            // This will output the following:
            //   KeyOne = 1
            //   KeyTwo = True
            //   KeyThree:Message = Oh, that's nice...

        }
    }
}