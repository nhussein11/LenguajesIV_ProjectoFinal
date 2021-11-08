using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LenguajesIV_ProjectoFinal.Models
{
    public class Agentes
    {
        [PrimaryKey, AutoIncrement]
        public int cod_agente { get; set; }
        [MaxLength(50)]
        public string  nombre_agente { get; set; }
        [MaxLength(50)]
        public string apellido_agente{ get; set; }
        [MaxLength(50)]
        public string domicilio_agente { get; set; }
        [MaxLength(50)]
        public string user_agente { get; set; }
        [MaxLength(50)]
        public string password_agente { get; set; }
        [MaxLength(50)]
        public string email_agente { get; set; }
        public int dni_agente { get; set; }
        public int telefono_agente { get; set; }
        


    }
}
