using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTasaCambio.Models
{
    public class Proceso
    {
        public string Tipo { get; set; }
        public TasaCambioDiaria TasaCambioDiaria { get; set; }
        public List<BasesDatos> ListaBaseDatos { get; set; }
    }
}