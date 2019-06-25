using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAppTasaCambio.Models;
using WebAppTasaCambio.Modules;

namespace WebAppTasaCambio.Jobs
{
    public class TasaCambioPrimeraActJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            TasaCambioModule tasaCambioModule = new TasaCambioModule();

            bool convert;
            DateTime TiempoActual = DateTime.Now;
            int MinZonaHorariaDif = 0;
            convert = int.TryParse(ConfigurationManager.AppSettings["HostingDiferenciaMinutos"], out MinZonaHorariaDif);
            TiempoActual = TiempoActual.AddMinutes(MinZonaHorariaDif);
            int HoraActual = TiempoActual.Hour;

            int HoraIPBS = 0;
            convert = int.TryParse(ConfigurationManager.AppSettings["HoraInicioPrimeraBusquedaSunat"], out HoraIPBS);
            int HoraFPBS = 0;
            convert = int.TryParse(ConfigurationManager.AppSettings["HoraFinPrimeraBusquedaSunat"], out HoraFPBS);



            if (HoraActual >= HoraIPBS && HoraActual <= HoraFPBS)
            {
                //verificar si ya se grabo o no
                DateTime xfecha = tasaCambioModule.UltimaFechaGuardada();
                DateTime fechaActual = DateTime.Now;
                int Actualizado = tasaCambioModule.UltimaActualizacion();
                int Mod = 0;
                Proceso proceso = new Proceso();
                if (xfecha.AddDays(1).ToShortDateString() == fechaActual.ToShortDateString() && Actualizado < 1)
                {
                    
                    tasaCambioModule.PrimerValor(xfecha.AddDays(1),1);

                    ActualizarTodasBasesDatos act = new ActualizarTodasBasesDatos();
                    List<BasesDatos> lista = new List<BasesDatos>();
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas4", RptDS = "sql5017.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas4_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas2", RptDS = "sql5020.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas2_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas3", RptDS = "sql5016.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas3_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas", RptDS = "sql5027.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas_admin" });
                  //  act.ActTasaCambio(lista, proceso.TasaCambioDiaria);
                     act.ActTasaCambio(proceso.ListaBaseDatos, proceso.TasaCambioDiaria);
                }





            }



        }
    }
}