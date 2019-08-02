using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SperReader;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using abstraction;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


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

        [TestMethod]
        public void LoadAssembly()
        {
            List<Type> myListaType = new List<Type>();

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                Assembly.LoadFile(dll);

            var type = typeof(BaseComponent);

            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where type.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract
                        select t;
            
            foreach (var item in types)
                myListaType.Add(item);

            CancellationToken tokenCancellation = new CancellationToken();
            Run(tokenCancellation, 1, myListaType);

            Assert.IsInstanceOfType(myListaType, typeof(List<Type>));
        }

        
        void Run(CancellationToken tokenCancellation, int maxParalelos, List<Type> myListTypes)
        {
            ParallelOptions options = new ParallelOptions()
            {
                CancellationToken = tokenCancellation,
                MaxDegreeOfParallelism = maxParalelos
            };
            Parallel.ForEach(myListTypes, options, type =>
            {
                GetBaseComponent(type).Ejecutar();
            });
        }

        BaseComponent GetBaseComponent(Type type)
        {
            return (BaseComponent)Activator.CreateInstance(type);
        }
    }
}