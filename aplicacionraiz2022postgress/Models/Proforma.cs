using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aplicacionraiz2022postgress.Models
{
    [Table("t_proforma")]
    public class Proforma
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }   
        [Column("UserID")]
        public String UserID { get; set; }
        [Column("Producto")]
        public Producto Producto { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }
        [Column("Precio")]
        public Decimal Precio { get; set; }
        [Column("Status")]
        public String Status { get; set; } = "PENDIENTE";
    }
}