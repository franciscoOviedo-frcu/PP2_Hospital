using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos //El namespace debe ser el mismo que el namespace del modelo para que entityframeworkcore entienda como una clase que extiende a la clase de contexto roginal
{
    public partial class DbConexion : DbContext
    {
        private DbConexion(string stringConexion)
            : base(stringConexion)
        {
            this.Configuration.LazyLoadingEnabled = false; //Deshabilitamos el LazyLoading para evitar problemas de serialización
            this.Configuration.ProxyCreationEnabled = false; //Deshabilitamos la creación de proxies para evitar problemas de serialización
            this.Database.CommandTimeout = 900;
        }

        public static DbConexion Create(/*string stringConexion*/)
        {
            return new DbConexion("name=DbConexion");
        }
    }
}
