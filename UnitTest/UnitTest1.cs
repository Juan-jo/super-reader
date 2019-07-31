using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperReader.SuperReader;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetList()
        {
            List<SampleModel> list;
            using (var context = ContextConection.Instancia)
            {
                try
                {
                    context.OpenConection();
                    list = context.getReader("SELECT * FROM [MAQE].[Orders]").VasReader<SampleModel>();

                    foreach (var item in list)
                        Console.WriteLine("Order:{0}, Client:{1}", item.CveOrder, item.ClientName);
                }
                finally
                {
                    context.CloseConnection();
                }
            }
            Assert.IsInstanceOfType(list, typeof(List<SampleModel>));
        }
        
    }
}
