using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public  class Infracciones
    {
        [PrimaryKey,AutoIncrement]
        public int cod_infraccion { get; set; }

        [MaxLength(200)]
        public string descripcion_infraccion { get; set; }

        public int precio_tentativo_infraccion { get; set; }
       
        [MaxLength(20)]
        public string severidad_infraccion { get; set; }
    }
}
