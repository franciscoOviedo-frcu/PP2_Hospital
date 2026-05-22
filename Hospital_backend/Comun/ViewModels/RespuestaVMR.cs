using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class RespuestaVMR<T>
    {
        public HttpStatusCode Codigo { get; set; }
        public T Datos { get; set; }
        public List<string> mensajes { get; set; }
        public RespuestaVMR() 
        {
            Codigo = HttpStatusCode.OK;
            Datos = default(T);
            mensajes = new List<string>();
        }
    }
}
