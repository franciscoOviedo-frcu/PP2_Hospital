using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class MedicoDAL
    {
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textobusqueda)  //cantidad: cantidad de elementos por pagina
        {
            ListadoPaginadoVMR<MedicoVMR> resultado = new ListadoPaginadoVMR<MedicoVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Medico.Where(x => !x.borrado).Select(x => new MedicoVMR   //Se usan expresiones Lambda, por eso se usa x
                {
                    id= x.id,
                    cedula = x.cedula,
                    nombre = x.nombre + " " + x.apellidoPaterno + (x.apellidoMaterno != null ? (" "+ x.apellidoMaterno) : ""),
                    esEspecialista = x.esEspecialista
                });

                if (!string.IsNullOrEmpty(textobusqueda))
                {
                    query = query.Where(x => x.cedula.Contains(textobusqueda) || x.nombre.Contains(textobusqueda));
                }

                resultado.cantidadTotal = query.Count();

                resultado.elementos = query
                    .OrderBy(x => x.id) //Ordenar por id, se puede cambiar por otro campo)
                    .Skip(pagina * cantidad) //Saltar los elementos de las paginas anteriores
                    .Take(cantidad)
                    .ToList();
            }
            
                
           return resultado;

        }

        public static MedicoVMR LeerUno(long id)
        {
            MedicoVMR item = null;

            using (var db = DbConexion.Create())
            {

            }

            return item;
        }

        public static long Crear(Medico Item)
        {
            long id = 0;

            using (var db = DbConexion.Create())
            {

            }

            return id;
        }

        public static void Actualizar(Medico Item)
        {
            using (var db = DbConexion.Create())
            {
            }
        }
        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
            }
        }
    }
}
