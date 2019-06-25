using Dapper;
using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Utilities.Globals;

namespace Infrastructure.Repositorys
{
    class RepGen
    { 
    public SqlConnection con;

    private void connection()
    {
        con = new SqlConnection("data source=localhost;initial catalog=DBcobot;integrated security=True;");
    }
        //public string executeNonQuery(string query, DynamicParameters param)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        con.Execute(query, param, commandType: CommandType.StoredProcedure);
        //        con.Close();
        //        return "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        //public string executeNonQueryStringQuery(string query)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        con.Execute(query, commandType: CommandType.Text);
        //        con.Close();
        //        return "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        //public string returnScalar(string query, DynamicParameters param)
        //{
        //    try
        //    {
        //        string valor = "";
        //        connection();
        //        con.Open();
        //        valor = con.ExecuteScalar<string>(query, param, commandType: CommandType.StoredProcedure);
        //        con.Close();
        //        return valor;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}


        //public string returnScalarStringQuery(string query)
        //{
        //    try
        //    {
        //        string valor = "";
        //        connection();
        //        con.Open();
        //        valor = con.ExecuteScalar<string>(query, commandType: CommandType.Text);
        //        con.Close();
        //        return valor;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}
        //public string returnNumericValue(string query, DynamicParameters param)
        //{
        //    try
        //    {
        //        string valor = "";
        //        param.Add("@output", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        param.Add("@Returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        //        // Getting Return value   
        //        connection();
        //        con.Open();
        //        valor = con.ExecuteScalar<string>(query, param, commandType: CommandType.StoredProcedure);
        //        con.Close();
        //        return valor;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        //public string returnNumericValueStringQuery(string query)
        //{
        //    try
        //    {
        //        string valor = "";
        //         // Getting Return value   
        //        connection();
        //        con.Open();
        //        valor = con.ExecuteScalar<string>(query, commandType: CommandType.Text);
        //        con.Close();
        //        return valor;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        public  Task<string> ExecuteNonQueryStringQueryAsync(string queryString, string connectionString)
        {
            return Task.Run(() =>
            {
                try
                {
                    SqlConnection conex;
                    conex = new SqlConnection(connectionString);
                    conex.Open();
                    conex.Execute(queryString, commandType: CommandType.Text);
                    conex.Close();
                    System.Diagnostics.Debug.Print("xxxxx");
                    return "0";

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                    return ex.Message;
                }
            });

        }

        public Task<string> ExecuteStoreProcedureAsync(string spName, decimal compra, decimal venta, string fecha , string connectionString)
        {
            SqlConnection conex;
            conex = new SqlConnection(connectionString);
            
            DynamicParameters param = new DynamicParameters();
            param.Add("@compra", compra, dbType: DbType.Decimal);
            param.Add("@venta", venta, dbType: DbType.Decimal);
            param.Add("@fecha", fecha, dbType: DbType.DateTime);
           

            return Task.Run(() =>
            {
                try
                {
                    conex.Open();
                    conex.Execute(spName, param, commandType: CommandType.StoredProcedure);
                  
                    conex.Close();
                    return "0";
                }
                catch (Exception ex)
                {
                   
                    return ex.Message;
                }
            });

        }

    }
}
