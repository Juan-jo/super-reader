using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperReader;
using SuperReader.SuperReader;
using System.Data.SqlClient;

namespace SuperReader
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SampleModel> list = ContextConection.getReader("SELECT * FROM [MAQE].[Orders]").VasReader<SampleModel>();
            string TS = "";
            Console.ReadKey();
        }
    }
}
