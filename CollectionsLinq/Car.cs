using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionsLinq
{
    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Pass { get; set; }
        public string Fail { get; set; }

        public Car(string m, string md, string y, string p, string f)
        {
            Make = m;
            Model = md;
            Year = y;
            Pass = p;
            Fail = f;
        }
        public override string ToString()
        {
            return "Car: " + Make + " " + Model + ", year: " + Year + ", no of pass: " + Pass + ", no of fails: " + Fail;
        }
    }
}
