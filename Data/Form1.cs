using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Collections;
using System.IO;
using static System.Windows.Forms.LinkLabel;

namespace Data
{
    public partial class Form1 : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=H:\Data\data.accdb;");
        Dictionary<int, Animal> My_dic = new Dictionary<int, Animal>();
        private Animal animal;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Funcation1();
        }

      /*The user enter an ID and the program displays the information associated 
        with this animal farm. In addition to the basic information, a string will be
        added to state the type of the animal (Dog, Cow, Jersey Cow, Sheep or Goat)*/
        public void Funcation1()
        {
            try
            {
                conn.Open();
                if (string.IsNullOrEmpty(SearchBox.Text))
                {
                    InforBox.Text = "No Id find";
                }
                //get the id form textbox and use sql qusert search in data
                OleDbCommand cmd1 = new OleDbCommand
                    ("SELECT amtWater, dailyCost, weight, age, color, amtMilk, isJersy FROM Cows WHERE ID  = " + int.Parse(SearchBox.Text), conn);
                OleDbCommand cmd2 = new OleDbCommand
                    ("SELECT amtWater, dailyCost, weight, age, color, amtMilk ,isJersy FROM Goats WHERE ID  = " + int.Parse(SearchBox.Text), conn);
                OleDbCommand cmd3 = new OleDbCommand
                    ("SELECT amtWater, dailyCost, weight, age, color, amtWool FROM Sheeps WHERE ID  = " + int.Parse(SearchBox.Text), conn);
                OleDbCommand cmd4 = new OleDbCommand
                    ("SELECT amtWater, weight, age, color, dailyCost FROM Dogs WHERE ID  = " + int.Parse(SearchBox.Text), conn);
                //create datareader for each table
                OleDbDataReader reader1 = cmd1.ExecuteReader();// the cows reader
                OleDbDataReader reader2 = cmd2.ExecuteReader();// the goats reader
                OleDbDataReader reader3 = cmd3.ExecuteReader();// the sheeps reader
                OleDbDataReader reader4 = cmd4.ExecuteReader();// the dogs reader

                if (reader1.Read())//Cows reader output
                {
                    //display the type in cow table for differente type: cow and jersycow
                    TypeBox.Clear();
                    if (reader1.GetValue(6).Equals(true))
                        TypeBox.Text = "Cow-Jersy";
                    else
                        TypeBox.Text = "Cow";
                    //display the information to textbox, each value for each line
                    InforBox.Text = "ID: " + int.Parse(SearchBox.Text) + "\r\n" + "amtWater: "
                        + reader1.GetValue(0).ToString() + "\r\n" + "dailyCost:" + reader1.GetValue(1).ToString() + "\r\n" + "Weight: "
                        + reader1.GetValue(2).ToString() + "\r\n" + "Age: " + reader1.GetValue(3).ToString() + "\r\n" + "Color: "
                        + reader1.GetValue(4).ToString() + "\r\n" + "amtMilk: " + reader1.GetValue(5).ToString();
                }
                else if (reader2.Read())//Goats reader output
                {
                    //display the Goat type
                    TypeBox.Text = "Goat";
                    //display the information to textbox, each value for each line
                    InforBox.Text = "ID: " + int.Parse(SearchBox.Text) + "\r\n" + "amtWater: "
                        + reader2.GetValue(0).ToString() + "\r\n" + "dailyCost:" + reader2.GetValue(1).ToString() + "\r\n" + "Weight: "
                        + reader2.GetValue(2).ToString() + "\r\n" + "Age: " + reader2.GetValue(3).ToString() + "\r\n" + "Color: "
                        + reader2.GetValue(4).ToString() + "\r\n" + "amtMilk: " + reader2.GetValue(5).ToString();
                }
                else if (reader3.Read())//Sheeps reader output
                {
                    //display the Sheep type
                    TypeBox.Text = "Sheep";
                    //display the information to textbox, each value for each line
                    InforBox.Text = "ID: " + int.Parse(SearchBox.Text) + "\r\n" + "amtWater: "
                        + reader3.GetValue(0).ToString() + "\r\n" + "dailyCost:" + reader3.GetValue(1).ToString() + "\r\n" + "Weight: "
                        + reader3.GetValue(2).ToString() + "\r\n" + "Age: " + reader3.GetValue(3).ToString() + "\r\n" + "Color: "
                        + reader3.GetValue(4).ToString() + "\r\n" + "amtWool: " + reader3.GetValue(5).ToString();
                }
                else if (reader4.Read())//Dogs reader output
                {
                    //display the Dog type
                    TypeBox.Text = "Dog";
                    //display the information to textbox, each value for each line
                    InforBox.Text = "ID: " + int.Parse(SearchBox.Text) + "\r\n" + "amtWater: "
                        + reader4.GetValue(0).ToString() + "\r\n" + "dailyCost:" + reader4.GetValue(1).ToString() + "\r\n" + "Weight: "
                        + reader4.GetValue(2).ToString() + "\r\n" + "Age: " + reader4.GetValue(3).ToString() + "\r\n" + "Color: "
                        + reader4.GetValue(4).ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void readData()
        {
            try
            {
                //create a string array for all tablename
                string[] animalTables = new string[] { "CommodityPrices", "Dogs", "Cows", "Goats", "Sheeps" };
                String q = "";
                conn.Open();
                //for read every table and wirte to a string value.
                for (int k = 0; k < animalTables.Length; k++)
                {
                    string str = "";
                    string tablename = animalTables[k].ToString();
                    //sql read table form tablename
                    q = $"SELECT * FROM {animalTables[k]}";
                    OleDbCommand cmd = new OleDbCommand(q, conn);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                str += reader[i].ToString();//write the each value in select table
                                if (i < reader.FieldCount - 1)
                                {
                                    str += ",";//add , easily for split
                                }
                            }
                            str += " ";
                        }
                        
                        switch (tablename)
                        {
                            //in price table
                            case "CommodityPrices":
                                //Remove strange symbols
                                string temp = Regex.Replace(str, "[^0-9.,]", "").Trim(',');
                                //Use commas to split into symbolic arrays
                                string[] count5 = temp.Split(',');
                                //Each item corresponds to a defined value
                                Prices.goatMilkPrices = double.Parse(count5[0]);
                                Prices.sheepWoolPrice = double.Parse(count5[1]);
                                Prices.waterPrice = double.Parse(count5[2]);
                                Prices.tax = double.Parse(count5[3]);
                                Prices.jersyCowTax = double.Parse(count5[4]);
                                Prices.cowMilkPrices = double.Parse(count5[5]);
                                break;
                            //in dog table
                            case "Dogs":
                                //Use commas to split into symbolic arrays
                                string[] cont4 = str.Trim().Split(' ');
                                //read all line int string array that already split
                                foreach (String line in cont4)
                                {
                                    //Each column is split to a different value
                                    string[] trimmed = line.Trim().Split(',');
                                    int id = int.Parse(trimmed[0]);
                                    double amtWater = double.Parse(trimmed[1].Trim());
                                    double weight = double.Parse(trimmed[2].Trim());
                                    int age = int.Parse(trimmed[3].Trim());
                                    string color = trimmed[4].Trim();
                                    double dailyCost = double.Parse(trimmed[5].Trim());
                                    String type = animalTables[k].ToString();

                                    //Create the animal object and add the values that have been read into the object
                                    Animal farmAnimal;
                                    farmAnimal = new Dog(id, type, amtWater, dailyCost, weight, age, color);
                                    My_dic.Add(id, farmAnimal);
                                    //Convenient for later calculations
                                    Prices.totDailyCost += dailyCost;
                                }
                                break;
                            //in sheep table
                            case "Sheeps":
                                //Use commas to split into symbolic arrays
                                string[] cont3 = str.Trim().Split(' ');
                                //read all line int string array that already split
                                foreach (String line in cont3)
                                {
                                    //Each column is split to a different value
                                    string[] trimmed = line.Trim().Split(',');
                                    int id = int.Parse(trimmed[0].Trim());
                                    double amtWater = double.Parse(trimmed[1].Trim());
                                    double dailyCost = double.Parse(trimmed[2].Trim());
                                    double weight = double.Parse(trimmed[3].Trim());
                                    int age = int.Parse(trimmed[4].Trim());
                                    string color = trimmed[5].Trim();
                                    double amtWool = double.Parse(trimmed[6].Trim());
                                    String type = animalTables[k].ToString();

                                    //Create the animal object and add the values that have been read into the object
                                    Animal farmAnimal;
                                    farmAnimal = new Sheep(id, type, amtWater, dailyCost, weight, age, color, amtWool);
                                    My_dic.Add(id, farmAnimal);
                                    //Convenient for later calculations
                                    Prices.SheepCount++;
                                    Prices.totamtWool += amtWool;
                                    Prices.totDailyCost += dailyCost;

                                }
                                break;
                            //in goat table
                            case "Goats":
                                //Use commas to split into symbolic arrays
                                string[] cont2 = str.Trim().Split(' ');
                                //read all line int string array that already split
                                foreach (string line in cont2)
                                {
                                    //Each column is split to a different value
                                    string[] trimmed = line.Trim().Split(',');
                                    int id = int.Parse(trimmed[0].Trim());
                                    double amtWater = double.Parse(trimmed[1].Trim());
                                    double dailyCost = double.Parse(trimmed[2].Trim());
                                    double weight = double.Parse(trimmed[3].Trim());
                                    int age = int.Parse(trimmed[4].Trim());
                                    string color = trimmed[5].Trim();
                                    double amtMilk = double.Parse(trimmed[6].Trim());
                                    bool isJersy = bool.Parse(trimmed[7].Trim());
                                    String type = animalTables[k].ToString();

                                    //Create the animal object and add the values that have been read into the object
                                    Animal farmAnimal;
                                    farmAnimal = new Goat(id, type, amtWater, dailyCost, weight, age, color, amtMilk);
                                    My_dic.Add(id, farmAnimal);
                                    //Convenient for later calculations
                                    Prices.GoatCount++;
                                    Prices.totamtMIlk += amtMilk;
                                    Prices.totDailyCost += dailyCost;

                                }
                                break;
                            //in cow table
                            case "Cows":
                                //Use commas to split into symbolic arrays
                                string[] cont1 = str.Trim().Split(' ');
                                //read all line int string array that already split
                                foreach (String line in cont1)
                                {
                                    //Each column is split to a different value
                                    string[] trimmed = line.Trim().Split(',');
                                    int id = int.Parse(trimmed[0].Trim());
                                    double amtWater = double.Parse(trimmed[1].Trim());
                                    double dailyCost = double.Parse(trimmed[2].Trim());
                                    double weight = double.Parse(trimmed[3].Trim());
                                    int age = int.Parse(trimmed[4].Trim());
                                    string color = trimmed[5].Trim();
                                    double amtMilk = double.Parse(trimmed[6].Trim());
                                    bool isJersy = bool.Parse(trimmed[7].Trim());
                                    String type = animalTables[k].ToString();
                                    String type1 = "JersyCow";
                                    //Determine if it is jersycow
                                    if (isJersy == true)
                                    {
                                        //Create the animal object and add the values that have been read into the object
                                        Animal FarmAnimal = new JersyCow(id, type1, amtWater, dailyCost, weight, age, color, amtMilk, isJersy);
                                        My_dic.Add(id, FarmAnimal);
                                        Prices.totamtMIlk += amtMilk;
                                        Prices.totDailyCost += dailyCost;
                                    }
                                    else
                                    {
                                    //Create the animal object and add the values that have been read into the object
                                        Animal farmAnimal = new Cow(id,type, amtWater, dailyCost, weight, age, color, amtMilk, isJersy);
                                        My_dic.Add(id, farmAnimal); 
                                        Prices.totamtMIlk += amtMilk;
                                        Prices.CowCount++;
                                        Prices.totDailyCost += dailyCost;
                                    }

                                }
                                break;
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readData();  
            try
            {
                //Display the total profitability/loose of the farm per day 
                //Display the average age of all animal farms (dog excluded)
                GetTotalProfication();
                //Display the total tax paid to the government per month
                GetTotalTax();
                //Display the total amount of milk per day for goats and cows
                totAmtMilk.Text = Prices.totamtMIlk.ToString() + "kg";
                //Display the total tax paid for Jersey Cows
                //Display the total profitability of all Jersey Cows
                GetJersyCow();
                //Display the ratio of Dogs’ cost compared to the total cost
                GetRatio();
                //Display the ratio of livestock with the color red
                GetRedRatio();
                //Display the average age of all animal farms (dog excluded)
                GetAvgAge();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //The user enter a threshold (number of years), and the program displays the ratio of
                //the number of animal farm where the age is above this threshold.
                GetAgeRatio();
                /*Generate a file that contains the ID of all animal ordered by their
                profitability(You are not allowed to use built-in sorting algorithm – Your
                code must do the sorting). Dogs are excluded.*/
                InsertionSort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Display the total profitability/loose of the farm per day 
        //Display the average age of all animal farms (dog excluded)
        public void GetTotalProfication()
        {
            double totP = 0;
            double CowProf = 0;
            double GoatProf = 0;
            double SheepProf = 0;
            foreach(var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    totP += key.Value.getProf();
                    switch (key.Value.GetType())
                    {
                        case "Cows":
                            CowProf += key.Value.getProf();
                            break;
                        case "Goats":
                            GoatProf += key.Value.getProf();
                            break;
                        case "Sheeps":
                            SheepProf += key.Value.getProf();
                            break;
                    }
                }
            }
            totProf.Text = "$" + totP.ToString();
            ACG_Box.Text = "$" + Math.Round(((CowProf / Prices.CowCount) + (GoatProf / Prices.GoatCount)),2).ToString();
            ASP_Box.Text = "$" + Math.Round((SheepProf / Prices.SheepCount),2).ToString();
            //Math.Round(x, 2)
        }
        //Display the total tax paid to the government per month
        public void GetTotalTax()
        {
            double tottax = 0;
            foreach (KeyValuePair<int, Animal> ele in My_dic)
            {
                tottax += ele.Value.getGovTax();
            }
            totTax.Text = "$"+Math.Round((tottax * 30),2).ToString();
        }
        //Display the average age of all animal farms (dog excluded)
        public void GetAvgAge()
        {
            double totage = 0;
            //int seachage = Convert.ToInt32(textBox3.Text);
           // double count = 0;
           // double totcount = 0;
            foreach (var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    if(key.Value.GetType() != "Dogs")
                    {
                        totage += key.Value.getAge();
                        
                    }
                    //if (key.Value.getAge() >= seachage)
                    //{
                        //count++;
                   // }
                    //totcount++;

                }
            }
            //double ratio = Math.Round(count / totcount);
           // textBox5.Text = (ratio * 100).ToString();
            double avgage = totage / (Prices.CowCount + Prices.GoatCount + Prices.SheepCount);
            AvgAgeBox.Text = Math.Round(avgage).ToString();
        }
        //Display the total tax paid for Jersey Cows
        //Display the total profitability of all Jersey Cows
        public void GetJersyCow()
        {
            double totProf = 0;
            double totTax = 0;
            foreach (var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    if (key.Value.GetType() == "JersyCow")
                    {
                        totProf += key.Value.getProf();
                        totTax += key.Value.getGovTax();
                    }
                }
            }
            textBox4.Text = totProf.ToString() + "kg";
            Top_J_Box.Text = "$" + totTax.ToString();
        }
        //Display the ratio of Dogs’ cost compared to the total cost
        public void GetRatio()
        {
            double DogCost = 0;
            foreach (var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    if (key.Value.GetType() == "Dogs")
                    {
                        DogCost += key.Value.getDailyCost();
                    }
                }
            }
            textBox1.Text = (100 * Math.Round((DogCost/Prices.totDailyCost),2)).ToString() + "%";
        }
        //Display the ratio of livestock with the color red
        public void GetRedRatio()
        {
            double redcount = 0;
            double nocount = 0;
            foreach (var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    if (key.Value.getColor() == "Red")
                    {
                        redcount++;
                    }
                    else
                    {
                        nocount++;
                    }
                }
            }
            textBox2.Text = redcount.ToString() + " : " + nocount.ToString();
        }
        //The user enter a threshold (number of years), and the program displays the ratio of the number of animal farm where the age is above this threshold.
        public void GetAgeRatio()
        {
            int seachage = Convert.ToInt32(textBox3.Text);
            double count = 0;
            foreach (var key in My_dic)
            {
                if (My_dic.TryGetValue(key.Key, out animal))
                {
                    if (key.Value.getAge() >= seachage)
                    {
                        count++;
                    }
                }
            }
            double ratio = Math.Round((count / My_dic.Count),2);
            textBox5.Text = (ratio * 100).ToString() + "%";
        }
/*        Generate a file that contains the ID of all animal ordered by their
        profitability(You are not allowed to use built-in sorting algorithm – Your
        code must do the sorting). Dogs are excluded.*/
        public void InsertionSort()
        {
            //Create list to accept values from animals
            List<double> termsList1 = new List<double>();
            List<double> termsList2 = new List<double>();
            double prof = 0;
            int id = 0;
            foreach (var key in My_dic)
            {
                prof = key.Value.getProf();
                id = key.Key;
                //Add the required values to the list
                termsList1.Add(id);
                termsList2.Add(prof);
            }
            double[] terms2 = termsList2.ToArray();
            double[] terms1 = termsList1.ToArray();
            double temp1;
            double temp2;
            for (int i = 0; i < terms2.Length - 1; i++)

                // traverse i+1 to array length
                for (int j = i + 1; j < terms2.Length; j++)

                    // compare array element with
                    // all next element
                    if (terms2[i] < terms2[j])
                    {
                        temp2 = terms2[i];
                        temp1 = terms1[i];
                        terms2[i] = terms2[j];
                        terms1[i] = terms1[j];
                        terms2[j] = temp2;
                        terms1[j] = temp1;
                    }
            textBox6.Text = "ID     Prof" + "\r\n";
            string path = @"H:\Data\Testing.txt";
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            for (int v=0;v < terms2.Length;v++)
            {
                textBox6.Text += (terms1[v] + "  " + Math.Round(terms2[v],2) + "\r\n");
                double text2write = terms1[v];
                writer.WriteLine(text2write);
                //await File.WriteAllLinesAsync("WriteLines.txt", terms1[v]);
            }
            writer.Close(); 

        }






        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void totporf_TextChanged(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
