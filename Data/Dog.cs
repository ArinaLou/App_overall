using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Dog : Animal
    {
        public Dog(int id,String type, double amtWater, double dailyCost, double weight, int age, string color)
            : base(id, type, amtWater, dailyCost, weight, age, color)
        {
        }
/*        public Dog(System.Data.IDataRecord record) : base(record)
        {
        }*/
    }
}
