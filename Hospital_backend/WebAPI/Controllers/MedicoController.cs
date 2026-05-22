using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] //se puede especificar el origen, los headers y los métodos permitidos
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

        [HttpGet] 
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<MedicoVMR>();

            try
            {
                respuesta.Datos = MedicoBLL.LeerUno(id);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            if (respuesta.Datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.Codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("No se encontró el médico con el id especificado.");
            }

            return Content(respuesta.Codigo, respuesta);
        }

        [HttpPost]
        public IHttpActionResult Crear(Medico Item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.Datos = MedicoBLL.Crear(Item);
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

        [HttpPut]
        public IHttpActionResult Actualizar(long id, MedicoVMR Item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                Item.id = id;
                MedicoBLL.Actualizar(Item);
                respuesta.Datos = true;
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
               
                MedicoBLL.Eliminar(ids);
                respuesta.Datos = true;
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }
    }
}

