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
            FileStream file4 = new FileStream("JsonFile/dem.json", FileMode.OpenOrCreate, FileAccess.Write);
            
            StreamReader sr1 = new StreamReader(file1);
            StreamReader sr2 = new StreamReader(file2);
            StreamReader sr3 = new StreamReader(file3);
            StreamWriter sw = new StreamWriter(file4);
            StringBuilder sb = new StringBuilder();
            var a = sr1.ReadLine();
            string[] head1 = a.Split(',');
            var b = sr2.ReadLine();
            string[] head2 = b.Split(',');
            var c = sr3.ReadLine();
            string[] head3 = c.Split(',');
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
            

        }
    }
}
