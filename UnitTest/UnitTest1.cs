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