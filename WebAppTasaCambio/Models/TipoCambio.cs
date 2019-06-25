using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppTasaCambio.Models
{
    public class TipoCambio
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }
}