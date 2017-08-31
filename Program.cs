[assembly: System.Reflection.AssemblyTitleAttribute("CsvToJson")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]
namespace CsvToJson
  {
    static class Program
    {
        static void Main(string[] args)
        {
            MyData data = new MyData();
            data.Writedata(); //call the write data method which further call the method having all implementation

        }
    }
  }


