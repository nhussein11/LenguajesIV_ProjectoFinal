using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public  class Infractores
    {
        [PrimaryKey,AutoIncrement]
        public int cod_infractores { get; set; }

        public string nombre_infractor { get; set; }
        [MaxLength(50)]
        public string apellido_infractor { get; set; }
        [MaxLength(50)]
        public string domicilio_infractor { get; set; }

        public string  dni_infractor { get; set; }
        public int telefono_infractor { get; set; }
    }
}
