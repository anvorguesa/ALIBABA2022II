using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionraiz2022postgress.Models

{

   

    [Table("t_descripcion")]

    public class Descripcion

    {    

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]

        public int ID {get; set;}

        [Column("userid")]

        public String UserId {get; set;}

        [Column("tags")]

        public String Tags{get; set;}

        [Column("textconst")]

        public String TextConst { get; set; }

        [Column("description")]

        public String Description {get; set;}




    }

   

}