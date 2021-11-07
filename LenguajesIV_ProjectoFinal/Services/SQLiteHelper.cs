using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using LenguajesIV_ProjectoFinal.Models;
namespace LenguajesIV_ProjectoFinal.Services
{
    //Escribir metodos de CRUD
    public class SQLiteHelper
    {

        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbpath) {
            db = new SQLiteAsyncConnection(dbpath);

            db.CreateTableAsync<Agente>().Wait();
            //crear demas tablas
        }
    }
    
}
