using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace SuperReader.SuperReader
{
    public static class SuperReader
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
            var o = Activator.CreateInstance<T>();
            foreach (var p in o.GetType().GetProperties())//typeof(T).GetProperties())
            {
                    convertTo = p.PropertyType;
                    p.SetValue(o, Convert.ChangeType(reader[p.Name], convertTo));
            }
            return o;
        }        
    }
}