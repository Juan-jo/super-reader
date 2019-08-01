namespace SperReader
{
    public sealed class Singleton<T> where T: class, new()
    {
        private static volatile T ObjT = null;
        private static readonly object Sync = new object(); // Objeto para verificar si la instacia es ejecutado por hilos y evitar que se instancié varias veces

        public static T GetInstancia
        {
            get
            {
                if (ObjT == null)
                    lock (Sync) // Valida si no esta bloqueda la instancia
                        if (ObjT == null)
                            ObjT = new T();
                return ObjT;
            }
        }
    }
}