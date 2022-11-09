using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Animal
    {
        public int id;
        public double amtWater;
        public double dailyCost;
        public double weight;
        public int age;
        public string color;
        public String type;

        public Animal(int id, String type, double amtWater, double dailyCost, double weight, int age, string color)
        {
            this.type = type;
            this.id = id;
            this.amtWater = amtWater;
            this.dailyCost = dailyCost;
            this.weight = weight;
            this.age = age;
            this.color = color;
        }

        public int getID() { return this.id; }//get ID
        public double getAmountOfWater() { return this.amtWater; }
        public double getDailyCost() { return this.dailyCost; }//get DailCost
        public String GetType() { return  this.type;}//Gettype
        public double getWeight() { return this.weight; }
        public int getAge() { return this.age; }//get Age
        public String getColor() { return this.color; }//get color
        public virtual double getAmountOfMilk() { return 0; }//get amount of milk
        public virtual Boolean getIsJersy() { return false; }//get is jersy
        public virtual double getAmountOfWool() { return 0; }//get amounlt of wool
        public virtual double getGovTax() { return 0; }//get tax
        virtual public double getProf() { return 0; }//get profility
        
    }
}
