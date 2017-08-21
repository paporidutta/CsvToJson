using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Dictionary<String, long> statefemaleDictionary = new Dictionary<string, long>();
        Dictionary<String, long> statemaleDictionary = new Dictionary<string, long>();
        Dictionary<String, long> stateDictionary = new Dictionary<string, long>();
        public void Read(StreamReader read)
             {
                var a = read.ReadLine();
                head = a.Split(','); //getting the headings
                while (!read.EndOfStream)
                    {
                        var d = read.ReadLine();
                        string[] data = d.Split(','); 
                        for (int i = 0; i < data.Length; i++)
                         {
                            if (head[i] == "Educational level - Graduate & above - Females" && data[i - 37] == "Total" && data[i - 36] == "All ages")
                                {
                                    if ((!statefemaleDictionary.ContainsKey(data[i - 38]))&&(!statemaleDictionary.ContainsKey(data[i - 38]))&&(!stateDictionary.ContainsKey(data[i - 38])))
                                        {
                                            statefemaleDictionary.Add(data[i - 38], long.Parse(data[i]));
                                            statemaleDictionary.Add(data[i - 38], long.Parse(data[i - 1]));
                                            stateDictionary.Add(data[i - 38], long.Parse(data[i - 2]));
                                         }
                                     else
                                        {
                                            statefemaleDictionary[data[i - 38]] += long.Parse(data[i]);
                                            statemaleDictionary[data[i - 38]] += long.Parse(data[i - 1]);
                                            stateDictionary[data[i - 38]] += long.Parse(data[i - 2]);
                                        }
                                 }
                            if (head[i] == "Literate - Persons" && data[i - 8] == "Total" && data[i - 7] != "All ages" && data[i - 7] != "0-6")
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
                        if (data[4] == "Total" && data[5] == "All ages")
                            {
                                for (int z = 15; z < 45; z++)
                                    {
                                        sum1[z - 15] += Convert.ToInt64(data[z]);
                                    }
                            }
                    }
             }
        public void Writedata()
            {
                StreamWriter sw1 = new StreamWriter(new FileStream("JsonFile/graduatepopulation.json", FileMode.OpenOrCreate, FileAccess.Write));
                StreamWriter sw2 = new StreamWriter(new FileStream("JsonFile/age.json", FileMode.OpenOrCreate, FileAccess.Write));
                StreamWriter sw3 = new StreamWriter(new FileStream("JsonFile/education-category.json", FileMode.OpenOrCreate, FileAccess.Write));
                Read(sr1);
                Read(sr2);
                Read(sr3);
                sb.AppendLine("{\"Graduate-Population-of-India - State-wise & Gender-wise\" : {");    //graduate json
                foreach (KeyValuePair<String, long> entry in stateDictionary)
                    {
                        sb.AppendLine("\"" + entry.Key + "\":" + "[");
                        sb.AppendLine("{\"Total\":" + entry.Value + "}," + "{\"Gender\":["+ "{\"Male\":" + statemaleDictionary[entry.Key] + "},"+ "{\"Female\":" + statefemaleDictionary[entry.Key] + "}]}" );
                        sb.AppendLine("],");
                    }
                sb.Length = sb.Length - 3;
                sb.AppendLine("}}");
                sw1.Write(sb);
                sw1.Flush();
                sb.Clear();
                sb.AppendLine("{\"Education-Category-wise" + "\":" + "[");      //education-category json
                int p;
                for (p = 0; p < 30; p++)
                    {
                        sb.AppendLine("{\"" + head[p+15] + "\": " + sum1[p] + "},"); 
                    }
                sb.Length = sb.Length - 3;
                sb.AppendLine("]}");
                sw3.WriteLine(sb);
                sw3.Flush();
                string json = JsonConvert.SerializeObject(literateDictionary, Formatting.Indented);   //age json using Newtonsoft
                sw2.WriteLine(json);
                sw2.Flush();
            }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mydata data = new Mydata();
            data.Writedata();
        }
    }
}


