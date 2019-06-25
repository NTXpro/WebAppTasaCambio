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
    class RepList<T> where T : class
    {

        public SqlConnection con;
        private void connection()
        {
            con = new SqlConnection("data source=localhost;initial catalog=DBcobot;integrated security=True;");
        }
        public List<T> returnListClassStoreProcedure(string query, DynamicParameters param)
        {
            try
            {
                connection();
                con.Open();
                IList<T> Tlista = SqlMapper.Query<T>(con, query, param, null, true, null, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return Tlista.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> returnListClassStringQuery(string query)
        {
            try
            {
                connection();
                con.Open();
                IList<T> Tlista = SqlMapper.Query<T>(con, query, null, commandType: CommandType.Text).ToList();
                con.Close();
                return Tlista.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public T returnClassStoreProcedure(string query, DynamicParameters param)
        {
            try
            {
                connection();
                con.Open();
                //     return this.con.Query( query, param, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                T Tlista = SqlMapper.Query<T>(con, query, param, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return Tlista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T returnClassStringQuery(string query)
        {
            try
            {
                connection();
                con.Open();
                //     return this.con.Query( query, param, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                T Tlista = SqlMapper.Query<T>(con, query,commandType: CommandType.Text).FirstOrDefault();
                con.Close();
                return Tlista;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
