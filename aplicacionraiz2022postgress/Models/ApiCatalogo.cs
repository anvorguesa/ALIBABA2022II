using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aplicacionraiz2022postgress.Models
{
    public class ApiCatalogo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Clase { get; set; }
        public string Subclase { get; set; }
        public string Keywords { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public Decimal PorcentajeDescuento { get; set; }
        public String ImageName { get; set; }
        public String Image3D { get; set; }
        public String Status { get; set; }
    }
}