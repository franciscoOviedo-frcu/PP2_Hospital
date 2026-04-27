using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class MedicoController : ApiController
    {
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textobusqueda = null)
        {
            return Ok(Logica.BLL.MedicoBLL.LeerTodo(cantidad, pagina, textobusqueda));
        }


        public IHttpActionResult LeerUno(long id)
        {

        }

        public IHttpActionResult Crear(Medico Item)
        {
        }

        public IHttpActionResult Actualizar(long id, MedicoVMR Item)
        {
        }

        public IHttpActionResult Eliminar(List<long> ids)
        {
        }
    }
}
