using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTasaCambio.Context;
using WebAppTasaCambio.Models;

namespace WebAppTasaCambio.Controllers
{
    public class HomeController : Controller
    {
        private SqlServerContext db = new SqlServerContext();
        public ActionResult Index()
        {
            bool convert;
            DateTime TiempoActual = DateTime.Now;
            int MinZonaHorariaDif = 0;
            convert = int.TryParse(ConfigurationManager.AppSettings["HostingDiferenciaMinutos"], out MinZonaHorariaDif);
            TiempoActual = TiempoActual.AddMinutes(MinZonaHorariaDif);
            ViewBag.FechaPeru = TiempoActual;
            ViewBag.Intervalo = ConfigurationManager.AppSettings["IntervaloMinutosSunat"];
            ViewBag.HoraIPBS = ConfigurationManager.AppSettings["HoraInicioPrimeraBusquedaSunat"];
            ViewBag.HoraFPBS = ConfigurationManager.AppSettings["HoraInicioPrimeraBusquedaSunat"];
            ViewBag.HoraISBS = ConfigurationManager.AppSettings["HoraInicioSegundaBusquedaSunat"];
            ViewBag.HoraISBS = ConfigurationManager.AppSettings["HoraInicioSegundaBusquedaSunat"];



            List<TasaCambioDiaria> lista = new List<TasaCambioDiaria>();
            lista = db.TasaCambioDiaria.OrderByDescending(x => x.Id).Take(7).ToList();
            return View(lista);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}