using HtmlAgilityPack;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using WebAppTasaCambio.Context;
using WebAppTasaCambio.Models;

namespace WebAppTasaCambio.Modules
{
    public class TasaCambioModule
    {
       // const string URL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes={0}&anho={1}";
        const string URL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias";
        private string html = string.Empty;
        private string mes = "";
        private string anio = "";
        private string diaNumero = "";
        private double compra = 0.0;
        private double venta = 0.0;
        private SqlServerContext db = new SqlServerContext();
        

        private DataTable obtenerDatos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dia", typeof(String));
            dt.Columns.Add("Compra", typeof(String));
            dt.Columns.Add("Venta", typeof(String));
            mes = ObtenerFechaFormato(DateTime.Now, "mes");
            anio = ObtenerFechaFormato(DateTime.Now, "anio");
            //string urlcomplete = string.Format(URL,mes, anio);
            string urlcomplete = string.Format(URL);
            this.html = new WebClient().DownloadString(urlcomplete);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(this.html);

            HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
            if (NodesTr != null)
            {

                int iNumFila = 0;
                foreach (HtmlNode Node in NodesTr)
                {
                    if (iNumFila > 0)
                    {
                        int iNumColumna = 0;
                        DataRow dr = dt.NewRow();
                        foreach (HtmlNode subNode in Node.Elements("td"))
                        {

                            if (iNumColumna == 0) dr = dt.NewRow();

                            string sValue = subNode.InnerHtml.ToString().Trim();
                            sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
                            dr[iNumColumna] = sValue.Trim();

                            iNumColumna++;

                            if (iNumColumna == 3)
                            {
                                dt.Rows.Add(dr);
                                iNumColumna = 0;
                            }
                        }
                    }
                    iNumFila++;
                }

                dt.AcceptChanges();
            }

            return dt;
        }

        public List<TipoCambio> ListadoPorMes()
        {
            try
            {

                List<TipoCambio> lstTc = new List<TipoCambio>();

                DataTable dt = obtenerDatos();
                foreach (DataRow dr in dt.Rows)
                {
                     diaNumero = int.Parse(dr["Dia"].ToString()).ToString("00");
                    TipoCambio objTc = new TipoCambio()
                    {
                        Dia = diaNumero,
                        Mes = this.mes,
                        Anio = this.anio,
                        Compra = decimal.Parse(dr["Compra"].ToString()),
                        Venta = decimal.Parse(dr["Venta"].ToString())
                    };
                    lstTc.Add(objTc);
                }

                return lstTc;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<BasesDatos> ListadoBaseDato()
        {
            List<BasesDatos> listaDb = new List<BasesDatos>();
            listaDb = db.BasesDatos.ToList();  
            return listaDb;
        }

        public DateTime UltimaFechaGuardada()
        {
            List<TasaCambioDiaria> lista = new List<TasaCambioDiaria>();
            lista = db.TasaCambioDiaria.ToList();
            DateTime ultima = lista.Last().Fecha;
            return ultima;
        }
        public int UltimaActualizacion()
        {
            List<TasaCambioDiaria> lista = new List<TasaCambioDiaria>();
            lista = db.TasaCambioDiaria.ToList();
            int ultima = lista.Last().Actualizado;
            return ultima;
        }


        public Proceso PrimerValor(DateTime fechaEvaluar,int paso)
        {
            Proceso proceso = new Proceso();
            proceso.Tipo = "No Insertar";
            TasaCambioDiaria tasaCambioDiaria = new TasaCambioDiaria();
            List<TasaCambioDiaria> listaTCD = new List<TasaCambioDiaria>();
            TipoCambio tipoCambio = new TipoCambio();
            List<TipoCambio> lstTc = new List<TipoCambio>();

            string Mes =fechaEvaluar.Month.ToString();
            if (Mes.Length == 1)
            {
                Mes = "0" + Mes;
            }
            string Dia = fechaEvaluar.Day.ToString();
            if (Dia.Length == 1)
            {
                Dia = "0" + Dia;
            }
            string Anio = fechaEvaluar.Year.ToString();

           
            lstTc = ListadoPorMes();
            // listado de sunat
            int existe = lstTc.Where(c => c.Anio == Anio && c.Dia == Dia && c.Mes == Mes).Count();
            
            if (existe > 0) //si existe el dia atual en el sunat
            {
                tipoCambio = lstTc.Where(c => c.Anio == Anio && c.Dia == Dia && c.Mes == Mes).FirstOrDefault();
                tasaCambioDiaria.Fecha = new DateTime(int.Parse(tipoCambio.Anio), int.Parse(tipoCambio.Mes), int.Parse(tipoCambio.Dia));
                tasaCambioDiaria.Compra = tipoCambio.Compra;
                tasaCambioDiaria.Venta = tipoCambio.Venta;
                tasaCambioDiaria.Proceso = "Insert-Sunat";
                proceso.Tipo = "Insertar";
                proceso.ListaBaseDatos = ListadoBaseDato();
                proceso.TasaCambioDiaria = tasaCambioDiaria;
            }
            else   // se busca el valor anterior en la tabla de ntx
            {
                listaTCD = db.TasaCambioDiaria.ToList();
                tasaCambioDiaria = listaTCD.Last();
                    tipoCambio.Anio = Anio;
                    tipoCambio.Mes = Mes;
                    tipoCambio.Dia = Dia;
                    tipoCambio.Compra = tasaCambioDiaria.Compra;
                    tipoCambio.Venta = tasaCambioDiaria.Venta;
                tasaCambioDiaria.Fecha = new DateTime(int.Parse(tipoCambio.Anio), int.Parse(tipoCambio.Mes), int.Parse(tipoCambio.Dia));
                tasaCambioDiaria.Proceso = "Insert-No act Sunat";
                proceso.Tipo = "Insertar";
                proceso.ListaBaseDatos = ListadoBaseDato();
                proceso.TasaCambioDiaria = tasaCambioDiaria;
            }
            GrabarValor(tasaCambioDiaria, paso);
            return proceso;
        }
    
        public void GrabarValor(TasaCambioDiaria tasaCambioDiaria, int paso)
        {
            
            int result = db.TasaCambioDiaria.Where(c => c.Fecha == tasaCambioDiaria.Fecha).Count();
            if (result==0)
            {
                tasaCambioDiaria.Proceso = tasaCambioDiaria.Proceso;
                tasaCambioDiaria.Actualizado = paso;
                db.TasaCambioDiaria.Add(tasaCambioDiaria);
                db.SaveChanges();
                ActualizarTodasBasesDatos act = new ActualizarTodasBasesDatos();
                List<BasesDatos> lista = new List<BasesDatos>();
                lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas4", RptDS = "sql5017.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas4_admin" });
                lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas2", RptDS = "sql5020.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas2_admin" });
                lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas3", RptDS = "sql5016.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas3_admin" });
                lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas", RptDS = "sql5027.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas_admin" });
                act.ActTasaCambio(lista, tasaCambioDiaria);
            }
            else
            { 
               
                TasaCambioDiaria obj = db.TasaCambioDiaria.First(c => c.Fecha == tasaCambioDiaria.Fecha);
                obj.Compra = tasaCambioDiaria.Compra;
                obj.Venta = tasaCambioDiaria.Venta;
                obj.Actualizado = paso;
                if (obj.Compra != tasaCambioDiaria.Compra || obj.Venta != tasaCambioDiaria.Venta)
                {
                    obj.Fecha = tasaCambioDiaria.Fecha;
                    obj.Proceso = "Update-Automatico";
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    ActualizarTodasBasesDatos act = new ActualizarTodasBasesDatos();
                    List<BasesDatos> lista = new List<BasesDatos>();
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas4", RptDS = "sql5017.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas4_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas2", RptDS = "sql5020.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas2_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas3", RptDS = "sql5016.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas3_admin" });
                    lista.Add(new BasesDatos { ID = 1, NombreBD = "DB_A42167_Pruebas", RptDS = "sql5027.site4now.net", password = "Quipu2016", usuario = "DB_A42167_Pruebas_admin" });
                    act.ActTasaCambio(lista, tasaCambioDiaria);
                }
                
            }

        }

      
        private string ObtenerFechaFormato(DateTime fecha,string tipo)
        {
            string retorno = "";
            if (tipo == "mes")
            {
                retorno = fecha.Month.ToString();
                if (retorno.Length == 1)
                {
                    retorno = "0" + retorno;
                }

            }
            if (tipo == "anio")
            {
                retorno = fecha.Year.ToString();
             

            }

            return retorno;
        }
    }
}