using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class MedicoBLL
    {
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textobusqueda)
        {
            return MedicoDAL.LeerTodo(cantidad, pagina, textobusqueda);
        }

        public static MedicoVMR LeerUno(long id)
        {
            return MedicoDAL.LeerUno(id);
        }

        public static long Crear(Medico Item)
        {
            return MedicoDAL.Crear(Item);
        }

        public static void Actualizar(MedicoVMR Item)
        {
             MedicoDAL.Actualizar(Item);
        }

        public static void Eliminar(List<long> ids)
        {
            MedicoDAL.Eliminar(ids);
        }
    }
}
