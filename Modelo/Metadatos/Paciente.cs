using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Libreria que contiene los modificadores (Required, StringLength, etc)

namespace Modelo.Modelos
{
    [MetadataType(typeof(PacienteMetadato))]
    public partial class Paciente
    {

    }

    public class PacienteMetadato
    {
        [Required]
        [StringLength(10)]
        public string cedula { get; set; }
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string apellidoPaterno { get; set; }
        [StringLength(50)]
        public string apellidoMaterno { get; set; }
        [Required]
        [StringLength(150)]
        public string direccion { get; set; }
        [Required]
        [StringLength(10)]
        public string celular { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string correoElectronico { get; set; }
    }
}
