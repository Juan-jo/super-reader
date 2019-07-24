using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Linq;


namespace SuperReader.SuperReader
{
    static class SuperReader
    {
        private static Type convertTo = null;

        public static List<T> VasReader<T>(this SqlDataReader reader) where T :class, new()
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var ResultList = new List<T>();
            while (reader.Read())
            {
                ResultList.Add(GetT<T>(reader));
            }
            return ResultList;
        }

        private static T GetT<T>(SqlDataReader reader) where T:class, new()
        {
            var t = Activator.CreateInstance<T>();
            
            foreach (var p in typeof(T).GetProperties())
            {
                    convertTo = p.PropertyType;
                    p.SetValue(t, Convert.ChangeType(reader[p.Name], convertTo));
            }
            return t;
        }
        

    }
}