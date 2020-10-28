using System;
using System.Collections.Generic;
using System.Text;

namespace SearchCars
{
    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Pass { get; set; }
        public int Fail { get; set; }

        public Car(string m, string md, int y, int p, int f)
        {
            Make = m;
            Model = md;
            Year = y;
            Pass = p;
            Fail = f;
        }
        public Car() { }
        public override string ToString()
        {
            return Make + " " + Model + ", year: " + Year + ", no of pass: " + Pass + ", no of fails: " + Fail;
        }
        public string toCSV()
        {
            int total = Pass + Fail;
            return Make + "," + Model + "," + Year + "," + total;
        }
    }
}
