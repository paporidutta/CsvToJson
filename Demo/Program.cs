using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            FileStream file4 = new FileStream("JsonFile/second.json", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream file5 = new FileStream("JsonFile/dem1.json", FileMode.OpenOrCreate, FileAccess.Write);
            FileStream file6 = new FileStream("JsonFile/third.json", FileMode.OpenOrCreate, FileAccess.Write);
            StreamReader sr1 = new StreamReader(file1);
            StreamReader sr2 = new StreamReader(file2);
            StreamReader sr3 = new StreamReader(file3);
            StreamWriter sw = new StreamWriter(file4);
            StreamWriter sw1 = new StreamWriter(file5);
            StreamWriter sw2 = new StreamWriter(file6);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
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

            sb.Append("{");
            while (!sr1.EndOfStream && !sr2.EndOfStream && !sr3.EndOfStream)
            {
                var d = sr1.ReadLine();
                string[] data1 = d.Split(',');
                var e = sr2.ReadLine();
                string[] data2 = e.Split(',');
                var f = sr3.ReadLine();
                string[] data3 = f.Split(',');
                if (data1[4] == "Total" && data1[5] == "All ages" && data2[4] == "Total" && data2[5] == "All ages" && data3[4] == "Total" && data3[5] == "All ages")
                {
                    sb.Append("\"" + data1[3] + "\":");
                    sb.Append("\n[");
                    sb.Append("{\"" + head1[39] + "\"" + ":" + "\"" + data1[39] + "\"},");
                    sb.Append("{\"" + head2[39] + "-SC\"" + ":" + "\"" + data2[39] + "\"},");
                    sb.Append("{\"" + head3[39] + "-ST\"" + ":" + "\"" + data3[39] + "\"},");
                    sb.Append("{\"" + head1[40] + "\"" + ":" + "\"" + data1[40] + "\"},");
                    sb.Append("{\"" + head2[40] + "-SC\"" + ":" + "\"" + data2[40] + "\"},");
                    sb.Append("{\"" + head3[40] + "-ST\"" + ":" + "\"" + data3[40] + "\"},");
                    sb.Append("{\"" + head1[41] + "\"" + ":" + "\"" + data1[41] + "\"},");
                    sb.Append("{\"" + head2[41] + "-SC\"" + ":" + "\"" + data2[41] + "\"},");
                    sb.Append("{\"" + head3[41] + "-ST\"" + ":" + "\"" + data3[41] + "\"}");
                    sb.Append("],\n");
                }
            }
            sb.Append("\"\":\" \"}");
            sw.Write(sb);
            sw.Flush();
            //next json
          
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
                    sw1.Flush();
                }
            }
            sb1.Append("end of file 1");
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
                    sw1.Flush();
                }

            }

            sb1.Append("end of file 2");

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
                    sw1.Flush();
                }

            }
            sb3.AppendLine("{");
            sb3.AppendLine("\"Education-Category-wise"+"\":"+ "[");
            int p;
            for (p = 0; p < 30; p++)
            {
                total[p] = sum1[p] + sum2[p] + sum3[p];
                sb3.AppendLine("{\"" + head1[p + 15] + "\": " + "\"" + total[p] + "\"},");
            }
            sb3.Append("{\"\":\" \"}");
            sb3.AppendLine("]");
            sb3.AppendLine("}");
            sw2.WriteLine(sb3);
            sw2.Flush();
            
           /* for (int x = 0; x < 30; x++)
            {
                Console.WriteLine(total[x]);
            }*/

        }
    }
}  

        
  