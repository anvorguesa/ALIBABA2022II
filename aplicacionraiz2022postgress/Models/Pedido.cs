using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aplicacionraiz2022postgress.Models
{
    [Table("t_order")]
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}
        [Column("UserID")]
        public String UserID{ get; set; }
        [Column("Total")]
        public Decimal Total { get; set; }
        [Column("pago")]
        public Pago pago { get; set; }
        [Column("Status")]
        public String Status { get; set; }

    }
}