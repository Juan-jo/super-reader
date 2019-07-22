using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SuperReader.SuperReader
{
    static class SuperReader
    {
        //Variable para saber el tipo de dato
        private static Type convertTo = null;

        public static List<T> VasReader<T>(this SqlDataReader reader) where T :class, new()
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var ResultList = new List<T>();

            while (reader.Read())
            {
                //Crea una instancia del modelo
                var item = Activator.CreateInstance<T>();

                //Recorre propiedades del model
                foreach(var propiedad in typeof(T).GetProperties())
                {
                    convertTo = propiedad.PropertyType;
                    propiedad.SetValue(item, Convert.ChangeType(reader[propiedad.Name], convertTo));
                }
                ResultList.Add(item);
            }
            return ResultList;
        }
    }
}
