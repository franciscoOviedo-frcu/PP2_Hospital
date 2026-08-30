using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        [HttpGet("leer-todo")]
        public ActionResult<RespuestaVMR<ListadoPaginadoVMR<MedicoVMR>>> LeerTodo(int cantidad = 10, int pagina = 0, string textobusqueda = null)
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

            return StatusCode((int)respuesta.Codigo, respuesta);
        }

        [HttpGet("{id}")]
        public ActionResult<RespuestaVMR<MedicoVMR>> LeerUno(long id)
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

            return StatusCode((int)respuesta.Codigo, respuesta);
        }

        [HttpPost]
        public ActionResult<RespuestaVMR<long?>> Crear(Medico Item)
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

            return StatusCode((int)respuesta.Codigo, respuesta);
        }

        [HttpPut("{id}")]
        public ActionResult<RespuestaVMR<bool>> Actualizar(long id, MedicoVMR Item)
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

            return StatusCode((int)respuesta.Codigo, respuesta);
        }

        [HttpDelete]
        public ActionResult<RespuestaVMR<bool>> Eliminar(List<long> ids)
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

            return StatusCode((int)respuesta.Codigo, respuesta);
        }
    }
}

