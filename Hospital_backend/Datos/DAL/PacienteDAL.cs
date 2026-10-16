using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class PacienteDAL
    {
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidad, int pagina, string textobusqueda)
        {
            ListadoPaginadoVMR<PacienteVMR> resultado = new ListadoPaginadoVMR<PacienteVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Paciente.Where(p => !p.borrado).Select(p => new PacienteVMR
                {
                    id = p.id,
                    cedula = p.cedula,
                    nombre = p.nombre + " " + p.apellidoPaterno + (p.apellidoMaterno != null ? (" " + p.apellidoMaterno) : ""),
                    apellidoPaterno = p.apellidoPaterno,
                    apellidoMaterno = p.apellidoMaterno,
                    celular = p.celular,
                    correoElectronico = p.correoElectronico,
                    direccion = p.direccion,
                });

                if (!string.IsNullOrEmpty(textobusqueda))
                {
                    query = query.Where(p => p.nombre.Contains(textobusqueda) || p.cedula.Contains(textobusqueda));
                }

                resultado.cantidadTotal = query.Count();
                resultado.elementos = query
                    .OrderBy(p => p.nombre)
                    .Skip((pagina) * cantidad)
                    .Take(cantidad)
                    .ToList();

            }
            return resultado;
        }

        public static PacienteVMR LeerUno(long id)
        {
            PacienteVMR item = null;
            using (var db = DbConexion.Create())
            {

                item=db.Paciente.Where(p => !p.borrado && p.id == id).Select(p => new PacienteVMR 
                { 
                    id = p.id,
                    cedula = p.cedula,
                    nombre = p.nombre,
                    apellidoPaterno = p.apellidoPaterno,
                    apellidoMaterno = p.apellidoMaterno,
                    celular = p.celular,
                    correoElectronico = p.correoElectronico,
                    direccion = p.direccion,
                }).FirstOrDefault();

            };
            return item;
        }

        public static long Crear(Paciente item)
        {

            using (var db = DbConexion.Create())
            {

                item.borrado = false;
                db.Paciente.Add(item);
                db.SaveChanges();

            }
            return item.id;
        }


        public static void Actualizar(PacienteVMR item)
        {

            using (var db = DbConexion.Create())
            {

                Paciente itemUpdate = db.Paciente.Find(item.id);

                if (itemUpdate == null || itemUpdate.borrado) //Se ignoran los borrados, ya que sino alguien puede manipular un registro que no aparece en la lista, dejandolo en un estado inconsistente
                {
                    throw new Exception("No se encontró el registro a actualizar con el id " + item.id);
                }

                itemUpdate.cedula = item.cedula;
                itemUpdate.nombre = item.nombre;
                itemUpdate.apellidoPaterno = item.apellidoPaterno;
                itemUpdate.apellidoMaterno = item.apellidoMaterno;
                itemUpdate.direccion = item.direccion;
                itemUpdate.celular = item.celular;
                itemUpdate.correoElectronico = item.correoElectronico;

                db.SaveChanges();
            }
        
        }


        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Paciente.Where(x => ids.Contains(x.id) && !x.borrado);
                foreach (var item in items)
                {
                    item.borrado = true;
                }

                db.SaveChanges();
            }
        }
        
    }
}
