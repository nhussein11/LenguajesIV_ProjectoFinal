using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public class Detalle_Multa
    {
        [PrimaryKey, AutoIncrement]
        public int cod_detalle_multa { get; set; }

        [MaxLength(200)]
        public string testimonio_agente { get; set; }
        [MaxLength(50)]
        public string observacion_detalle_multa{ get; set; }

        public int subtotal_detalle_multa { get; set; }

        //FKs: cod_multa, cod_infraccion
        public int cod_multa { get; set; }
        public int cod_infracion { get; set; }

    }
}
