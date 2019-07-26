namespace SuperReader.SuperReader
{
    public class Singleton<T> where T: class, new()
    {
        #region declare properties
        private static volatile T NInstance = null;

        // Objeto para verificar si la instacia es ejecutado por hilos y evitar que se instancié varias veces
        private static readonly object Sync = new object();
        #endregion declare properties

        public static T Instancia
        {
            get
            {
                if(NInstance == null)
                {
                    // Valida si no esta bloqueda la instancia
                    lock (Sync)
                    {
                        if(NInstance == null)
                        {
                            NInstance = new T();
                        }
                    }
                }
                return NInstance;
            }
        }
    }
}
