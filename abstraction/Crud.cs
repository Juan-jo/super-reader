using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstraction
{
    public interface Crud<T> where T: class, new()
    {
        T Create(T t);

        T Read(object ID);

        T Update(T t);

        bool Delete(object ID);
    }
}
