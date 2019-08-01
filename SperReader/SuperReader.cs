using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SperReader
{
    public static class SuperReader
    {
        private static Type convertTo;

        public static List<T> VasReader<T>(this SqlDataReader sdr) where T: class, new()
        {
            if (sdr == null) throw new ArgumentNullException(nameof(sdr));

            var list = new List<T>();
            while (sdr.Read())
                list.Add(GetT<T>(sdr));

            return list;
        }

        private static T GetT<T>(SqlDataReader sdr) where T: class, new()
        {
            var t = Activator.CreateInstance<T>();
            foreach(var p in typeof(T).GetProperties())
            {
                convertTo = p.PropertyType;
                p.SetValue(t, Convert.ChangeType(sdr[p.Name], convertTo));
            }
            return t;
        }
    }
}
