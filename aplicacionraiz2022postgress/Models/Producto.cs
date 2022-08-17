using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aplicacionraiz2022postgress.Models
{
    [Table("t_producto")]
    public class Producto
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int? Id { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("Clase")]
        public string? Clase { get; set; }
        [Column("Subclase")]
        public string? Subclase { get; set; }
        [Column("Keywords")]
        public string? Keywords { get; set; }
        [Column("Estado")]
        public string? Estado { get; set; }
        [Column("Descripcion")]
        public string? Descripcion { get; set; }
        [Column("Precio")]
        public Decimal? Precio { get; set; }
        [Column("PorcentajeDescuento")]
        public Decimal? PorcentajeDescuento { get; set; }
        [Column("ImageName")]
        public String? ImageName { get; set; }
        [Column("Image3D")]
        public String? Image3D { get; set; }
        [Column("Status")]
        public String? Status { get; set; }

    }
}