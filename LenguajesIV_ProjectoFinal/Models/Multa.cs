using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace LenguajesIV_ProjectoFinal.Models
{
    public class Multa
    {

        [PrimaryKey, AutoIncrement]
        public int cod_multa { get; set; }
        
        public int cod_agente { get; set; }
    }
}
