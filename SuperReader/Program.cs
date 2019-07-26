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
            using(var context = ContextConection.Instancia)
            {
                try
                {
                    context.OpenConection();
                    List<SampleModel> list = context.getReader("SELECT * FROM [MAQE].[Orders]").VasReader<SampleModel>();

                    foreach (var item in list)
                        Console.WriteLine("Order:{0}, Client:{1}", item.CveOrder, item.ClientName);
                }
                finally
                {
                    context.CloseConnection();
                }
            }            
            Console.ReadKey();
        }
    }
}
