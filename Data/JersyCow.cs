using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class JersyCow : Cow
    {
        public JersyCow(int id,String type, double amtWater, double dailyCost, double weight, int age, string color, double amtMilk, bool isJersy) :
            base(id, type, amtWater, dailyCost, weight, age, color, amtMilk, isJersy)
        {

        }

        override public double getProf()
        {
            double gprof = (amtMilk * Prices.cowMilkPrices)
                - (amtWater * Prices.waterPrice) - dailyCost;
            return gprof;
        }
        override public double getGovTax()
        {

            return amtMilk * Prices.jersyCowTax;
        }
        override public double getAmountOfMilk()
        {
            return this.amtMilk;
        }
    }
}
