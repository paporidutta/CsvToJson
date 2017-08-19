using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file1 = new FileStream("Data/India2011.csv", FileMode.Open, FileAccess.Read);
            FileStream file2 = new FileStream("Data/IndiaSC2011.csv", FileMode.Open, FileAccess.Read);
            FileStream file3 = new FileStream("Data/IndiaST2011.csv", FileMode.Open, FileAccess.Read);
            FileStream file4 = new FileStream("JsonFile/graduatepopulation.json", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream file5 = new FileStream("JsonFile/age.json", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream file6 = new FileStream("JsonFile/education-category.json", FileMode.OpenOrCreate, FileAccess.Write);
            StreamReader sr1 = new StreamReader(file1);
            StreamReader sr2 = new StreamReader(file2);
            StreamReader sr3 = new StreamReader(file3);
            StreamWriter sw = new StreamWriter(file4);
            StreamWriter sw1 = new StreamWriter(file5);
            StreamWriter sw2 = new StreamWriter(file6);
            StringBuilder state = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder age = new StringBuilder();
            var a = sr1.ReadLine();
            string[] head1 = a.Split(',');
            var b = sr2.ReadLine();
            string[] head2 = b.Split(',');
            var c = sr3.ReadLine();
            string[] head3 = c.Split(',');
            long[] sum1 = new long[30];
            long[] sum2 = new long[30];
            long[] sum3 = new long[30];
            long[] total = new long[30];
            Dictionary<String, long> literateDictionary = new Dictionary<string, long>();
            Dictionary<String, long> statefemaleDictionary = new Dictionary<string, long>();
            Dictionary<String, long> statemaleDictionary = new Dictionary<string, long>();
            Dictionary<String, long> stateDictionary = new Dictionary<string, long>();
            while (!sr1.EndOfStream)
            {
                var d = sr1.ReadLine();
                string[] data1 = d.Split(',');
                for (int i = 0; i < data1.Length; i++)
                {
                    if (head1[i] == "Educational level - Graduate & above - Females" && data1[i -37] == "Total" && data1[i-36] == "All ages")
                    {
                        if (!statefemaleDictionary.ContainsKey(data1[i-38]))
                        {
                            statefemaleDictionary.Add(data1[i - 38], long.Parse(data1[i]));
                        }
                        else
                        {
                            statefemaleDictionary[data1[i -38]] += long.Parse(data1[i]);
                        }
                        if (!statemaleDictionary.ContainsKey(data1[i - 38]))
                        {
                            statemaleDictionary.Add(data1[i - 38], long.Parse(data1[i-1]));
                        }
                        else
                        {
                            statemaleDictionary[data1[i - 38]] += long.Parse(data1[i-1]);
                        }
                        if (!stateDictionary.ContainsKey(data1[i - 38]))
                        {
                            stateDictionary.Add(data1[i - 38], long.Parse(data1[i-2]));
                        }
                        else
                        {
                            stateDictionary[data1[i - 38]] += long.Parse(data1[i-2]);
                        }
                      }
                }
            }
            while (!sr2.EndOfStream)
            {
                var d = sr2.ReadLine();
                string[] data2 = d.Split(',');
                for (int i = 0; i < data2.Length; i++)
                {
                    if (head1[i] == "Educational level - Graduate & above - Females" && data2[i - 37] == "Total" && data2[i - 36] == "All ages")
                    {
                        if (!statefemaleDictionary.ContainsKey(data2[i - 38]))
                        {
                            statefemaleDictionary.Add(data2[i - 38], long.Parse(data2[i]));
                        }
                        else
                        {
                            statefemaleDictionary[data2[i - 38]] += long.Parse(data2[i]);
                        }
                        if (!statemaleDictionary.ContainsKey(data2[i - 38]))
                        {
                            statemaleDictionary.Add(data2[i - 38], long.Parse(data2[i - 1]));
                        }
                        else
                        {
                            statemaleDictionary[data2[i - 38]] += long.Parse(data2[i - 1]);
                        }
                        if (!stateDictionary.ContainsKey(data2[i - 38]))
                        {
                            stateDictionary.Add(data2[i - 38], long.Parse(data2[i - 2]));
                        }
                        else
                        {
                            stateDictionary[data2[i - 38]] += long.Parse(data2[i - 2]);
                        }
                    }
                }
            }
            while (!sr3.EndOfStream)
            {
                var d = sr3.ReadLine();
                string[] data3 = d.Split(',');
                for (int i = 0; i < data3.Length; i++)
                {
                    if (head1[i] == "Educational level - Graduate & above - Females" && data3[i - 37] == "Total" && data3[i - 36] == "All ages")
                    {
                        if (!statefemaleDictionary.ContainsKey(data3[i - 38]))
                        {
                            statefemaleDictionary.Add(data3[i - 38], long.Parse(data3[i]));
                        }
                        else
                        {
                            statefemaleDictionary[data3[i - 38]] += long.Parse(data3[i]);
                        }
                        if (!statemaleDictionary.ContainsKey(data3[i - 38]))
                        {
                            statemaleDictionary.Add(data3[i - 38], long.Parse(data3[i - 1]));
                        }
                        else
                        {
                            statemaleDictionary[data3[i - 38]] += long.Parse(data3[i - 1]);
                        }
                        if (!stateDictionary.ContainsKey(data3[i - 38]))
                        {
                            stateDictionary.Add(data3[i - 38], long.Parse(data3[i - 2]));
                        }
                        else
                        {
                            stateDictionary[data3[i - 38]] += long.Parse(data3[i - 2]);
                        }
                    }
                }
            }
            state.Append("{");
            state.AppendLine("\"Graduate-Population-of-India - State-wise & Gender-wise\" :");
            state.AppendLine("{");
            foreach (KeyValuePair<String, long> entry in stateDictionary)
            {
                state.AppendLine("\""+entry.Key+"\":"+"[");
                state.AppendLine("{\"Total\":"+entry.Value+"},");
                state.AppendLine("{\"Gender\":[");
                state.AppendLine("{\"Male\":" + statemaleDictionary[entry.Key] + "},");
                state.AppendLine("{\"Female\":" + statefemaleDictionary[entry.Key] + "}");
                state.AppendLine("]}");
                state.Append("],");
            }
            state.Length =state.Length-1;
            state.AppendLine("}");
            state.AppendLine("}");
            sw.Write(state);
            sw.Flush();
            //bringing the stream reader to the beginning of file
            sr1.DiscardBufferedData();
            sr1.BaseStream.Seek(0, SeekOrigin.Begin);
            sr1.ReadLine();
            while (!sr1.EndOfStream)
            {
                var d = sr1.ReadLine();
                string[] data1 = d.Split(',');
                if (data1[4] == "Total" && data1[5] == "All ages")
                {
                    sb1.Append("{");
                    for (int z = 15; z < 45; z++)
                    {
                        sb1.Append(head1[z] + " = " + data1[z] + " ");
                        sum1[z - 15] += Convert.ToInt64(data1[z]);
                    }
                    sb1.Append("}");
                }
                sb1.Append("end of file 1");
            }
            sr2.DiscardBufferedData();
            sr2.BaseStream.Seek(0, SeekOrigin.Begin);
            sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                var e = sr2.ReadLine();
                string[] data2 = e.Split(',');
                if (data2[4] == "Total" && data2[5] == "All ages")
                {
                    sb1.Append("{");
                    for (int z = 15; z < 45; z++)
                    {
                        sb1.Append(head1[z] + " = " + data2[z] + " ");
                        sum2[z - 15] += Convert.ToInt64(data2[z]);
                    }
                    sb1.Append("}");
                }
            }
            sr3.DiscardBufferedData();
            sr3.BaseStream.Seek(0, SeekOrigin.Begin);
            sr3.ReadLine();
            while (!sr3.EndOfStream)
            {
                var f = sr3.ReadLine();
                string[] data3 = f.Split(',');
                if (data3[4] == "Total" && data3[5] == "All ages")
                {
                    sb1.Append("{");
                    for (int z = 15; z < 45; z++)
                    {
                        sb1.Append(head1[z] + " = " + data3[z] + " ");
                        sum3[z - 15] += Convert.ToInt64(data3[z]);
                    }
                    sb1.Append("}");
                }
            }
            sb3.AppendLine("{");
            sb3.AppendLine("\"Education-Category-wise" + "\":" + "[");
            int p;
            for (p = 0; p < 30; p++)
            {
                total[p] = sum1[p] + sum2[p] + sum3[p];
                sb3.AppendLine("{\"" + head1[p + 15] + "\": " +  total[p] + "},");
            }
            sb3.Length = sb3.Length - 3;
            sb3.AppendLine("]");
            sb3.AppendLine("}");
            sw2.WriteLine(sb3);
            sw2.Flush();
            sr1.DiscardBufferedData();
            sr1.BaseStream.Seek(0, SeekOrigin.Begin);
            sr1.ReadLine();
            while (!sr1.EndOfStream)
            {
                var d = sr1.ReadLine();
                string[] data1 = d.Split(',');
                for (int j = 0; j < data1.Length; j++)
                {
                    if (head1[j] == "Literate - Persons" && data1[j - 8] == "Total" && data1[j - 7] != "All ages" && data1[j - 7] != "0-6")
                    {
                      if (!literateDictionary.ContainsKey(data1[j - 7]))
                        {
                            literateDictionary.Add(data1[j - 7], long.Parse(data1[j]));
                        }
                        else
                        { 
                            literateDictionary[data1[j - 7]] += long.Parse(data1[j]);
                        }
                    }
                } 
            }
            sr2.DiscardBufferedData();
            sr2.BaseStream.Seek(0,SeekOrigin.Begin);
            sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                var d = sr2.ReadLine();
                string[] data2 = d.Split(',');
                for (int j = 0; j < data2.Length; j++)
                {
                    if (head2[j] == "Literate - Persons" && data2[j - 8] == "Total" && data2[j - 7] != "All ages" && data2[j - 7] != "0-6")
                    {
                        if (!literateDictionary.ContainsKey(data2[j - 7]))
                        {
                            literateDictionary.Add(data2[j - 7], long.Parse(data2[j]));
                        }
                        else
                        {
                            literateDictionary[data2[j - 7]] += long.Parse(data2[j]);
                        }
                    }
                }
            }
            sr3.DiscardBufferedData();
            sr3.BaseStream.Seek(0,SeekOrigin.Begin);
            sr3.ReadLine();
            while (!sr3.EndOfStream)
            {
                var d = sr3.ReadLine();
                string[] data3 = d.Split(',');
                for (int j = 0; j < data3.Length; j++)
                {
                    if (head3[j] == "Literate - Persons" && data3[j - 8] == "Total" && data3[j - 7] != "All ages" && data3[j - 7] != "0-6")
                    {
                        if (!literateDictionary.ContainsKey(data3[j - 7]))
                        {
                            literateDictionary.Add(data3[j - 7], long.Parse(data3[j]));
                        }
                        else
                        {
                            literateDictionary[data3[j - 7]] += long.Parse(data3[j]);
                        }
                    }
                }
            }
            age.AppendLine("{");
            age.AppendLine("\"Agewise-LiteratePoplulation\" :");
            age.AppendLine("[");
            foreach (KeyValuePair<String,long> entry in literateDictionary)
            {
                age.AppendLine("{\"" + entry.Key + "\"" + ":" + entry.Value + "},");
            }
            age.Length = age.Length - 3; //to remove the comma
            age.AppendLine("]");
            age.AppendLine("}");
            sw1.Write(age);
            sw1.Flush();
        }
    }
}  

        
  