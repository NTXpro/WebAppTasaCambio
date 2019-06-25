using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppTasaCambio.Models;

namespace WebAppTasaCambio.Modules
{
    public class ActualizarTodasBasesDatos
    {
        ServerService serverService = new ServerService();

        public async void ActTasaCambio(List<BasesDatos> listaBd, TasaCambioDiaria tasa)
        {
            await Task.Run(async () =>
             {
                
             foreach (BasesDatos item in listaBd)
             {
                 await serverService.InsertUpdateTasaSP(tasa.Compra, tasa.Venta, fechaSql(tasa.Fecha), item.cadena);
                     System.Diagnostics.Debug.WriteLine(item.cadena);
                 }
             });
             
        }

        private string fechaSql(DateTime fecha)
        {

            string Mes = fecha.Month.ToString();
            if (Mes.Length == 1)
            {
                Mes = "0" + Mes;
            }
            string Dia = fecha.Day.ToString();
            if (Dia.Length == 1)
            {
                Dia = "0" + Dia;
            }
            string fechaSQL = fecha.Year.ToString() + "-" + Mes + "-" + Dia;
           
            return fechaSQL;
        }


    }
}