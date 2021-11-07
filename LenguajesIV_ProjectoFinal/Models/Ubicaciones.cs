using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public  class Ubicaciones
    {
        [PrimaryKey,AutoIncrement]
        public int cod_ubicacion { get; set; }

        [MaxLength(50)]
        public string latitud_ubicacion { get; set; }

        [MaxLength(50)]
        public string longitud_ubicacion { get; set; }

        //FKs: cod_multa
    }
}
