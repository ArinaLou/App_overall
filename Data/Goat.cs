using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Goat : Animal
    {
        public double amtMilk { get; private set; }
        public Goat(int id,String type, double amtWater, double dailyCost, double weight, int age, string color, double amtMilk) :
            base(id, type, amtWater, dailyCost, weight, age, color)
        {
            this.amtMilk = amtMilk;
        }
/*        public Goat(System.Data.IDataRecord record) : base(record)
        {
            this.amtMilk = Convert.ToDouble(record["amtMilk"].ToString());
        }*/
        override public double getProf()
        {
            double gprof = amtMilk * Prices.goatMilkPrices;
            return gprof;
        }
        override public double getGovTax()
        {

            return amtMilk * Prices.tax;
        }
        override public double getAmountOfMilk()
        {
            return this.amtMilk;
        }
    }
}
