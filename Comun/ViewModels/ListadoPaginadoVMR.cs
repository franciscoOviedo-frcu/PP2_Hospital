using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class ListadoPaginadoVMR<T>
    {
        public int cantidadTotal { get; set; }
        public IEnumerable<T> elementos { get; set; }

        public ListadoPaginadoVMR()
        {
            cantidadTotal = 0;
            elementos = new List<T>();
        }
    }

}
