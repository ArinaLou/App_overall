using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Cow : Animal
    {
        public double amtMilk;
        public bool isJersy;
        public Cow(int id, String type, double amtWater, double dailyCost, double weight, int age, string color, double amtMilk, Boolean isjersy) :
            base(id, type, amtWater, dailyCost, weight, age, color)
        {
            this.amtMilk = amtMilk;
            this.isJersy = isjersy;
        }

/*        public Cow(System.Data.IDataRecord record) : base(record)
        {
            this.amtMilk = Convert.ToDouble(record["amtMilk"].ToString());
            this.isJersy = Convert.ToBoolean(record["isJersy"].ToString());
        }*/

        override public double getProf()
        {
            double gprof = (amtMilk * Prices.cowMilkPrices)
                - (amtWater * Prices.waterPrice) - dailyCost;
            return gprof;
        }
        override public double getGovTax()
        {
            return amtMilk * Prices.tax;
        }
        override public  double getAmountOfMilk() 
        { 
            return this.amtMilk; 
        }
    }
}

