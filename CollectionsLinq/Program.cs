using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CollectionsLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\klaud\source\repos\CollectionsLinq\motfailures.csv"));
            String header = reader.ReadLine();
            List<Car> cars = new List<Car>();

            while (!reader.EndOfStream)
            {
                String line = reader.ReadLine();
                Console.WriteLine(line);
                var values = line.Split(',');
                Car car = new Car(values[0], values[1], values[2], values[3], values[4]);
                cars.Add(car);    
            }

            var queryMake = from car in cars where car.Make == "\"CITROEN\"" select car;
            var queryModel = from car in cars where car.Model == "\"GT\"" select car;
            var queryYear = from car in cars where car.Year == "\"1964\"" select car;

            


        }
    }
}
