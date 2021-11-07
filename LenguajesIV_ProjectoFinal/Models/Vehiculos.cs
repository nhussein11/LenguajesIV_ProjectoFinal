using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace LenguajesIV_ProjectoFinal.Models
{
    public  class Vehiculos
    {
        [PrimaryKey,AutoIncrement]
        public int cod_vehiculo { get; set; }

        [MaxLength(50)]
        public string patente_dominio_vehiculo { get; set; }

        [MaxLength(50)]
        public string modelo_vehiculo { get; set; }

        [MaxLength(100)]
        public string caracteristicas_vehiculo { get; set; }

    }
}
