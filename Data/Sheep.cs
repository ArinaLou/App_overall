using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Sheep : Animal
    {
        public double amtWool { get; private set; }
        public Sheep(int id,String type, double amtWater, double dailyCost, double weight, int age, string color, double amtWool) :
            base(id, type, amtWater, dailyCost, weight, age, color)
        {
            this.amtWool = amtWool;
        }
/*        public Sheep(System.Data.IDataRecord record) : base(record)
        {
            this.amtWool = Convert.ToDouble(record["amtWool"].ToString());
        }*/
        override public double getProf()
        {
            double gprof = amtWool * Prices.sheepWoolPrice;
            return (gprof);
        }

        override public double getAmountOfWool() 
        { 
            return amtWool / 365; 
        }
    }
}
