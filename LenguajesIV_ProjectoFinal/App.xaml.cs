﻿
using LenguajesIV_ProjectoFinal.Services;
﻿using System.IO;
using LenguajesIV_ProjectoFinal.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LenguajesIV_ProjectoFinal.Services;

namespace LenguajesIV_ProjectoFinal
{
    public partial class App : Application
    {
        static SQLiteHelper db;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        public static SQLiteHelper SQLiteDB
        {
            get {
                if (db==null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Muncipalidad.db3"));
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
