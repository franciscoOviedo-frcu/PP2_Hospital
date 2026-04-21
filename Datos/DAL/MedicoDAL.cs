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
                item = db.Medico.Where(x => !x.borrado && x.id == id).Select(x => new MedicoVMR
                {
                    id = x.id,
                    cedula = x.cedula,
                    nombre = x.nombre,
                    apellidoMaterno = x.apellidoMaterno,
                    apellidoPaterno = x.apellidoPaterno,
                    habilitado = x.habilitado,
                    esEspecialista = x.esEspecialista
                }).FirstOrDefault();
            }

            return item;
        }

        public static long Crear(Medico Item)
        {
            long id = 0;

            using (var db = DbConexion.Create())
            {
                Item.borrado = false;
                db.Medico.Add(Item);
                db.SaveChanges();
            }

            return id;
        }

        public static void Actualizar(MedicoVMR Item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Medico.Find(Item.id);

                itemUpdate.cedula = Item.cedula;
                itemUpdate.nombre = Item.nombre;
                itemUpdate.apellidoPaterno = Item.apellidoPaterno;
                itemUpdate.apellidoMaterno = Item.apellidoMaterno;
                itemUpdate.esEspecialista = Item.esEspecialista;
                itemUpdate.habilitado = Item.habilitado;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Medico.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
    }
}
