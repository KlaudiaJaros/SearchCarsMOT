using System;
using System.Collections.Generic;
using System.Text;

namespace SearchCars
{
    /// <summary>
    /// A class to store information about car's make, model, year and the number of pass/fail MOTs.
    /// </summary>
    class Car
    {
        // properties:
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Pass { get; set; }
        public int Fail { get; set; }

        public Car() { }
        public override string ToString()
        {
            return Make + " " + Model + ", year: " + Year + ", no of pass: " + Pass + ", no of fails: " + Fail;
        }
        public string ToCSV()
        {
            int total = Pass + Fail;
            return Make + "," + Model + "," + Year + "," + total;
        }
    }
}
