using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace CsvToJson
  {
     class MyData
       {
          long[] sum = new long[30];
          string[] head = new string[60];
          Dictionary<String, long> literateDictionary = new Dictionary<string, long>();
          Dictionary<String, long> stategraduatefemaleDictionary = new Dictionary<string, long>();
          Dictionary<String, long> stategraduatemaleDictionary = new Dictionary<string, long>();
          Dictionary<String, long> stategraduateDictionary = new Dictionary<string, long>();
          public void Read(StreamReader read)
            {
                var a = read.ReadLine();
                head = a.Split(','); //getting the headings
                while (!read.EndOfStream) //read the file till it does not reach the end of the file
                 {
                    var d = read.ReadLine();
                    string[] data = d.Split(',');    //getting the data
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (head[i] == "Educational level - Graduate & above - Females" && data[i - 37] == "Total" && data[i - 36] == "All ages") //logic for getting graduate population state-wise and gender-wise
                        {
                            if ((!stategraduatefemaleDictionary.ContainsKey(data[i - 38])) && (!stategraduatemaleDictionary.ContainsKey(data[i - 38])) && (!stategraduateDictionary.ContainsKey(data[i - 38]))) //check if key exist
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
                        GetDataByAge(data, i);    //refracting
                    }
                    GetDataByEducationCatergory(data); //call the logic for getting all education catergories of India
                }
            read.Dispose();  //dispose the file reader resource
          }
            private void GetDataByAge(string[] data, int i) //extracted from Read Method
            {
                if (head[i] == "Literate - Persons" && data[i - 8] == "Total" && data[i - 7] != "All ages" && data[i - 7] != "0-6") //logic for getting Age-wise population distribution in terms of literate population
                {
                    if (!literateDictionary.ContainsKey(data[i - 7]))
                        literateDictionary.Add(data[i - 7], long.Parse(data[i]));
                    else
                        literateDictionary[data[i - 7]] += long.Parse(data[i]);
                }
            }
            private void GetDataByEducationCatergory(string[] data)  //extracted from Read Method
            {
                if (data[4] == "Total" && data[5] == "All ages")  //Education Category wise - all India data combined together
                {
                    for (int z = 15; z < 45; z++)
                    {
                        sum[z - 15] += Convert.ToInt64(data[z]);
                        z++; z++;  //to skip the male and female columns
                    }
                }
            }
           public  void Writedata()
               {
                try        //exception handling for reading the files
                 {
                    StreamReader sr1 = new StreamReader(new FileStream("Data/India2011.csv", FileMode.Open, FileAccess.Read));
                    Read(sr1); //read file India2011
                    StreamReader sr2 = new StreamReader(new FileStream("Data/IndiaSC2011.csv", FileMode.Open, FileAccess.Read));
                    Read(sr2); //read file IndiaSC2011
                    StreamReader sr3 = new StreamReader(new FileStream("Data/IndiaST2011.csv", FileMode.Open, FileAccess.Read));
                    Read(sr3); //read file IndiaST2011
                }
                catch (FileNotFoundException ex) { Console.Write("The file does not exist in the current context: {0}", ex.Message); } //handle if file is not found
                catch (IOException e) { Console.WriteLine("the file may be used by another resource", e.Message); } 
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[");    //graduate json
                foreach (KeyValuePair<String, long> entry in stategraduateDictionary)
                {
                    sb.AppendLine("{\"state\":" + "\"" + entry.Key + "\",");
                    sb.AppendLine("\"values\":[");
                    sb.AppendLine("{\"value\":" + "\"" + entry.Value + "\"," + "\"type\":" + "\"total\"},");
                    sb.AppendLine("{\"value\":" + "\"" + stategraduatemaleDictionary[entry.Key] + "\"," + "\"type\":" + "\"male\"},");
                    sb.AppendLine("{\"value\":" + "\"" + stategraduatefemaleDictionary[entry.Key] + "\"," + "\"type\":" + "\"female\"}]");
                    sb.AppendLine("},");
                }
                sb.Length = sb.Length - 3;//to remove the last comma from string builder
                sb.AppendLine("]");
                StreamWriter sw1 = new StreamWriter(new FileStream("graduatepopulation.json", FileMode.OpenOrCreate, FileAccess.Write));
                sw1.Write(sb); //writing to graduatepopulation.json
                sw1.Flush();
                sw1.Dispose();
                sb.Clear();
                sb.AppendLine("[");      //education-category json
                int p;
                for (p = 0; p < 30; p++)
                {
                    sb.AppendLine("{\"category\":" + "\"" + head[p + 15] + "\",");
                    sb.AppendLine("\"total\":" + sum[p]);
                    sb.AppendLine("},");
                    p++; p++;
                }
                sb.Length = sb.Length - 3;//to remove the last comma from string builder
                sb.AppendLine("]");
                StreamWriter sw3 = new StreamWriter(new FileStream("education-category.json", FileMode.OpenOrCreate, FileAccess.Write));
                sw3.WriteLine(sb); //writing education-category.json
                sw3.Flush(); sw3.Dispose();
                sb.Clear();
                sb.AppendLine("["); //age json using  streambuilder
                foreach (KeyValuePair<String, long> entry in literateDictionary)
                {
                    sb.AppendLine("{\"age\":" + "\"" + entry.Key + "\",");
                    sb.AppendLine("\"literate\":" + entry.Value);
                    sb.AppendLine("},");
                }
                sb.Length = sb.Length - 3;
                sb.AppendLine("]");
                StreamWriter sw2 = new StreamWriter(new FileStream("age.json", FileMode.OpenOrCreate, FileAccess.Write));
                sw2.WriteLine(sb);//write to age.json
                sw2.Flush(); sw2.Dispose();
            }
        }
    }


