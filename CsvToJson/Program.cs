using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace CsvToJson
{
    class Mydata
    {
        StreamReader sr1 = new StreamReader(new FileStream("Data/India2011.csv", FileMode.Open, FileAccess.Read));
        StreamReader sr2 = new StreamReader(new FileStream("Data/IndiaSC2011.csv", FileMode.Open, FileAccess.Read));
        StreamReader sr3 = new StreamReader(new FileStream("Data/IndiaST2011.csv", FileMode.Open, FileAccess.Read));
        StringBuilder sb = new StringBuilder();
        long[] sum1 = new long[30];
        string[] head = new string[60];
        Dictionary<String, long> literateDictionary = new Dictionary<string, long>();
        Dictionary<String, long> stategraduatefemaleDictionary = new Dictionary<string, long>();
        Dictionary<String, long> stategraduatemaleDictionary = new Dictionary<string, long>();
        Dictionary<String, long> stategraduateDictionary = new Dictionary<string, long>();
        public void Read(StreamReader read)
             {
                var a = read.ReadLine();
                head = a.Split(','); //getting the headings
                while (!read.EndOfStream)
                    {
                        var d = read.ReadLine();
                        string[] data = d.Split(',');    //getting the data
                        for (int i = 0; i < data.Length; i++)
                         {
                            if (head[i] == "Educational level - Graduate & above - Females" && data[i - 37] == "Total" && data[i - 36] == "All ages") //logic for getting graduate population state-wise and gender-wise
                                {
                                    if ((!stategraduatefemaleDictionary.ContainsKey(data[i - 38]))&&(!stategraduatemaleDictionary.ContainsKey(data[i - 38]))&&(!stategraduateDictionary.ContainsKey(data[i - 38]))) //check if key exist
                                        {
                                            stategraduatefemaleDictionary.Add(data[i - 38], long.Parse(data[i]));
                                            stategraduatemaleDictionary.Add(data[i - 38], long.Parse(data[i - 1]));
                                            stategraduateDictionary.Add(data[i - 38], long.Parse(data[i - 2]));
                                         }
                                     else
                                        {
                                            stategraduatefemaleDictionary[data[i - 38]] += long.Parse(data[i]);
                                            stategraduatemaleDictionary[data[i - 38]] += long.Parse(data[i - 1]);
                                            stategraduateDictionary[data[i - 38]] += long.Parse(data[i - 2]);
                                        }
                                 }
                            if (head[i] == "Literate - Persons" && data[i - 8] == "Total" && data[i - 7] != "All ages" && data[i - 7] != "0-6") //logic for getting Age-wise population distribution in terms of literate population
                                 {
                                    if (!literateDictionary.ContainsKey(data[i - 7]))
                                        {
                                            literateDictionary.Add(data[i - 7], long.Parse(data[i]));
                                        }
                                    else
                                        {
                                            literateDictionary[data[i - 7]] += long.Parse(data[i]);
                                        }
                                }
                         }
                        if (data[4] == "Total" && data[5] == "All ages")  //Education Category wise - all India data combined together
                            {
                                for (int z = 15; z < 45; z++)
                                    {
                                        sum1[z - 15] += Convert.ToInt64(data[z]);
                                        z++; z++;  //to skip the male and female columns
                                     }
                            }
                    }
             }
        public void Writedata()
            {
                StreamWriter sw1 = new StreamWriter(new FileStream("JsonFile/graduatepopulation.json", FileMode.OpenOrCreate, FileAccess.Write));
                StreamWriter sw2 = new StreamWriter(new FileStream("JsonFile/age.json", FileMode.OpenOrCreate, FileAccess.Write));
                StreamWriter sw3 = new StreamWriter(new FileStream("JsonFile/education-category.json", FileMode.OpenOrCreate, FileAccess.Write));
                Read(sr1); //read file India2011
                Read(sr2); //read file IndiaSC2011
                Read(sr3); //read file IndiaST2011
                sb.AppendLine("[");    //graduate json
                foreach (KeyValuePair<String, long> entry in stategraduateDictionary)
                    {
                        sb.AppendLine("{\"state\":" + "\"" + entry.Key + "\",");
                sb.AppendLine("\"values\":[");
                sb.AppendLine("{\"value\":" + "\"" + entry.Value + "\","+"\"type\":"+"\"total\"},");
                sb.AppendLine("{\"value\":" + "\"" + stategraduatemaleDictionary[entry.Key] + "\"," + "\"type\":" + "\"male\"},");
                sb.AppendLine("{\"value\":" + "\"" + stategraduatefemaleDictionary[entry.Key] + "\"," + "\"type\":" + "\"female\"}]");
              
                        sb.AppendLine("},");
                    }
                sb.Length = sb.Length - 3;//to remove the last comma from string builder
                sb.AppendLine("]");
                sw1.Write(sb); //writing to graduatepopulation.json
                sw1.Flush();
                sb.Clear();
                sb.AppendLine("[");      //education-category json
                int p;
                for (p = 0; p < 30; p++)
                    {
                     sb.AppendLine("{\"category\":" + "\"" + head[p + 15] + "\",");
                     sb.AppendLine("\"total\":" + sum1[p]);
                     sb.AppendLine("},");
                     p++;p++;
                    }
                sb.Length = sb.Length - 3;//to remove the last comma from string builder
                sb.AppendLine("]");
                sw3.WriteLine(sb); //writing education-category.json
                sw3.Flush();
                sb.Clear();
                sb.AppendLine("["); //age json using 
                foreach (KeyValuePair<String, long> entry in literateDictionary)
                {
                sb.AppendLine("{\"age\":" +"\""+entry.Key + "\",");
                sb.AppendLine("\"literate\":" + entry.Value );
                sb.AppendLine("},");
                 }
                sb.Length = sb.Length - 3;
                sb.AppendLine("]");
                sw2.WriteLine(sb);//write to age.json
                sw2.Flush();
            }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mydata data = new Mydata();
            data.Writedata();  //call writedata function
        }
    }
}


