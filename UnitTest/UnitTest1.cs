using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SperReader;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetList()
        {
            List<ModelInfo> list;
            using (var context = dbContext.Instancia)
            {
                try
                {
                    context.OpenConnection();
                    list = context.GetReaderBySQL("SELECT * FROM [MAQE].[Orders]").VasReader<ModelInfo>();
                }
                finally
                {
                    context.CloseConnection();
                }
            }
            Assert.IsInstanceOfType(list, typeof(List<ModelInfo>));
        }
    }
}