using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTasaCambio.Models
{
    public class BasesDatos
    {
        public int ID { get; set; }
        public string NombreBD { get; set; }
        public string RptDS { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string cadena => $"Data Source={RptDS};database={NombreBD};User={usuario};pwd={password};";
    }
 }