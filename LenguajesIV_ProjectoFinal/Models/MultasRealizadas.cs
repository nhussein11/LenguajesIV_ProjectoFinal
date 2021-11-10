using System;
using System.Collections.Generic;
using System.Text;

namespace LenguajesIV_ProjectoFinal.Models
{
    //Esta no es otra tabla 
    //solo me sirve para tener todos los datos juntos en un obj a la hora de mostrar las multas realizadas
    public class MultasRealizadas
    {
        public string fecha_multa { get; set; }
 
        public string lugar_multa { get; set; }

        public string nombre_agente { get; set; }
       
        public string apellido_agente { get; set; }

        public string dni_infractor { get; set; }

        public string patente_dominio_vehiculo { get; set; }

        public string latitud_ubicacion { get; set; }

        public string longitud_ubicacion { get; set; }

    }
}
