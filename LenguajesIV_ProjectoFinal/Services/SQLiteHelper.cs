using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using LenguajesIV_ProjectoFinal.Models;
using System.Threading.Tasks;

namespace LenguajesIV_ProjectoFinal.Services
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Agentes>().Wait();
            db.CreateTableAsync<Detalle_Multa>().Wait();
            db.CreateTableAsync<Infracciones>().Wait();
            db.CreateTableAsync<Infractores>().Wait();
            db.CreateTableAsync<Multas>().Wait();
            db.CreateTableAsync<Ubicaciones>().Wait();
            db.CreateTableAsync<Vehiculos>().Wait();
        }

                                 //CRUD:

                                //INSERT + UPDATE
                                /// <summary>
                                /// Insert or Update del agente dado
                                /// </summary>
                                /// <param name="agen"></param>
                                /// <returns></returns>
        //AGENTES:
        public Task<int> SaveAgentesAsync(Agentes agen)
        {
            if (agen.cod_agente!=0)
            {
                return db.UpdateAsync(agen);
            }else { return db.InsertAsync(agen); }
        }
        //DETALLE_MULTA:
        public Task<int> SaveDetalle_MultaAsync(Detalle_Multa det_multa)
        {
            //Acá debería hacer el insert/update de cod_multa, cod_infraccion
            if (det_multa.cod_detalle_multa == 0)
            {
                return db.UpdateAsync(det_multa);
            }
            else{ return db.InsertAsync(det_multa); }
        }
        //INFRACCIONES:
        public Task<int> SaveInfraccionesAsync(Infracciones infrac)
        {
            if (infrac.cod_infraccion == 0)
            {
                return db.UpdateAsync(infrac);
            }
            else { return db.InsertAsync(infrac); }
        }
        //INFRACTORES:
        public Task<int> SaveInfractoresAsync(Infractores infractores)
        {
            if (infractores.cod_infractores == 0)
            {
                return db.UpdateAsync(infractores);
            }
            else{ return db.InsertAsync(infractores); }
        }
        //MULTAS:
        public Task<int> SaveMultassAsync(Multas multas)
        {
            //Acá debería hacer el insert/update de cod_vehiculo, cod_infractor, cod agente
            if (multas.cod_multa == 0)
            {
                return db.UpdateAsync(multas);
            }
            else{ return db.InsertAsync(multas); }
        }
        //UBICACIONES:
        public Task<int> SaveUbicacionesAsync(Ubicaciones ubi)
        {
            //Acá debería hacer el insert/update de cod_multa
            if (ubi.cod_ubicacion== 0)
            {
                return db.UpdateAsync(ubi);
            }
            else{ return db.InsertAsync(ubi); }
        }
        //VEHICULOS:
        public Task<int> SaveVehiculosAsync(Vehiculos vehiculo)
        {
            if (vehiculo.cod_vehiculo == 0)
            {
                return db.UpdateAsync(vehiculo);
            }
            else{ return db.InsertAsync(vehiculo); }
        }

                                            //READ:
        //AGENTES:
        /// <summary>
        /// Devuelve TODOS los agentes sin condicion
        /// </summary>
        /// <returns>Retorna listado de todos los agentes en la tabla</returns>
        public Task<List<Agentes>> Get_Agentes_Async()
        {
            return db.Table<Agentes>().ToListAsync();
        }
        /// <summary>
        /// SELECT * from agente where user=user_dado and password=pass_dada
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<Agentes> Get_Agentes_byUserandPassword_Async(string user, string password)
        {
            return db.Table<Agentes>().Where(agen => agen.user_agente == user && agen.password_agente == password).FirstOrDefaultAsync();
        }
        //INFRACTORES:
        public Task<Infractores> Get_Infractores_byDNI_Async(string dni)
        {
            return db.Table<Infractores>().Where(infractor => infractor.dni_infractor == Convert.ToInt32(dni)).FirstOrDefaultAsync();
        }
        //VEHICULOS:
        public Task<Vehiculos> Get_VehiculosbyPatente_Async(string patente)
        {
            return db.Table<Vehiculos>().Where(vehiculo => vehiculo.patente_dominio_vehiculo == patente).FirstOrDefaultAsync();
        }


        //DELETE:
        /// <summary>
        /// Eliminar un Agente dado
        /// </summary>
        /// <param name="agente"></param>
        /// <returns>Devuelve...</returns>
        public Task<int> Delete_Agente_async(Agentes agente)
        {
            return db.DeleteAsync(agente);
        }
    }
    
}
