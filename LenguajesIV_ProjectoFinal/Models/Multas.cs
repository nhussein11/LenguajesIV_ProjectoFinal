﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public class Multas
    {
        [PrimaryKey, AutoIncrement]
        public int cod_multa { get; set; }

        [MaxLength(10)] //Formato: dd/mm/yyyy
        public string fecha_multa { get; set; }
        [MaxLength(10)] //Formato: hh:mm
        public string hora_multa { get; set; }
        [MaxLength(50)]
        public string lugar_multa { get; set; } //Este creo que seria opcional, ya que lo conectamos con la tabla ubicaciones por la FK

        /* extensión: SQLite-Net Extensions 
            [ForeignKey(typeof(Bus))]
            public int BusId { get; set; }
         */
        //FKs: cod_agente, cod_infractor, cod_vehiculo, cod_agente


    }
}