using Dapper;
using Infrastructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ServerService
    {
        //RepList<Servers> lista = new RepList<Servers>();
        RepGen repGen = new RepGen();
        public async Task<string> InsertUpdateTasa(string queryString, string connString)
        {
            
            return await repGen.ExecuteNonQueryStringQueryAsync(queryString, connString);
        }

        public async Task<string> InsertUpdateTasaSP(decimal compra, decimal venta, string fecha, string connString)
        {
            string spName = "ERP.spu_actualizar_tasa_cambio";
            return await repGen.ExecuteStoreProcedureAsync(spName, compra,venta,fecha, connString);
        }

        //public List<Servers> allRecords()
        //{

        //    return lista.returnListClassStringQuery("SELECT [Id], [ServerName], [DatabaseName], [UserName], [Password], [ProviderName], [Status] FROM[dbo].[Servers]");
        //}

        //public List<Servers> FindByName(string name)
        //{
        //    RepList<Servers> lista = new RepList<Servers>();
        //    return lista.returnListClassStringQuery($"SELECT [Id], [ServerName], [DatabaseName], [UserName], [Password], [ProviderName], [Status] FROM[dbo].[Servers] where [ServerName] like '%{name}%'");
        //}

        //public string DeleteBy(string id)
        //{
        //    RepList<Servers> lista = new RepList<Servers>();
        //    DynamicParameters param = new DynamicParameters();
        //    return repGen.executeNonQuery($"DELETE FROM[dbo].[Servers] where [Id] = {id}", param);
        //}

        //public string Update(Servers servers)
        //{
        //    RepList<Servers> lista = new RepList<Servers>();
        //    DynamicParameters param = new DynamicParameters();
        //    return repGen.executeNonQuery($"UPDATE [dbo].[Servers] SET[ServerName] = {servers.ServerName}, [DatabaseName] = {servers.ServerName}, [UserName] " +
        //        $"= {servers.UserName}, [Password] = {servers.Password}, [ProviderName] = {servers.ProviderName}, [Status] = {servers.Status} WHERE([Id] = {servers.Id}", param);
        //}

        //public string Insert(Servers servers)
        //{
        //    RepList<Servers> lista = new RepList<Servers>();
        //    DynamicParameters param = new DynamicParameters();
        //    return repGen.executeNonQuery($"INSERT INTO [dbo].[Servers]([ServerName], [DatabaseName], [UserName], [Password], [ProviderName], [Status]) VALUES(, {servers.ServerName}, {servers.UserName}, {servers.Password},{servers.ProviderName}, {servers.ProviderName})", param);
        //}


    }
}
