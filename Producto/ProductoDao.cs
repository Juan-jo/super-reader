using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abstraction;
namespace Producto
{
    public class ProductoDao : BaseComponent, Crud<ProductoInfo>
    {
        public ProductoInfo Create(ProductoInfo t)
        {
            return null;
        }

        public bool Delete(object ID)
        {
            var i = (int)ID;
            return false;
        }

        public override void Ejecutar()
        {
            Console.WriteLine("Holis");
        }

        public ProductoInfo Read(object ID)
        {
            return null;
        }

        public ProductoInfo Update(ProductoInfo t)
        {
            return null;
        }
    }
}
