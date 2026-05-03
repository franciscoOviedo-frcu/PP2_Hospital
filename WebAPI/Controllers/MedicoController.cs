using Comun.ViewModels;
using Logica.BLL;
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
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textobusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<MedicoVMR>>();

            try
            {
                respuesta.Datos = MedicoBLL.LeerTodo(cantidad, pagina, textobusqueda);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);

        }


        /*public IHttpActionResult LeerUno(long id)
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
        }*/
    }
}
