extern alias MySqlConnectorAlias;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;


namespace DataAccessor
{
    public class DbInterfacing : IDbInterfacing
    {
        private ILogger<DbInterfacing> _logger;
        public DbInterfacing(ILogger<DbInterfacing> logger)
        {
            _logger = logger;
        }
        public async Task<SqlModelRes<T>> GetList<T>(string connString, string commandName, CommandType commandType, DynamicParameters param)
        {
            var res = new SqlModelRes<T>();
            string classMeth = "GetList";
            try
            {
                using (var connection = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<T>(commandName, param, null, null, commandType);
                    if (result != null)
                    {
                        res.Success = true;
                    }
                    res.ResultList = result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(classMeth + "Error occured while executing " + commandName + " " + ex.Message + " " + ex.StackTrace);
                res.Success = false;
                res.ErrorMessage = ex.ToString();
            }
            return res;
        }

        public async Task<SqlModelRes<T>> GetOneItem<T>(string connString, string commandName, CommandType commandType, DynamicParameters param)
        {
            var res = new SqlModelRes<T>();
            string classMeth = "GetOneItem";
            try
            {
                using (var connection = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    var result = await connection.QuerySingleOrDefaultAsync<T>(commandName, param, null, null, commandType);
                    if (result != null)
                    {
                        res.Success = true;
                    }
                    res.Result = result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(classMeth + "Error occured while executing " + commandName + " " + ex.Message + " " + ex.StackTrace);
                res.Success = false;
                res.ErrorMessage = ex.ToString();
            }
            return res;
        }
        public async Task<SqlModelRes<int>> ModifyDB(string connString, string commandName, CommandType commandType, DynamicParameters param)
        {
            var res = new SqlModelRes<int>();
            string classMeth = "ModifyDB";
            try
            {
                using (var connection = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection(connString))
                {

                    var result = await connection.ExecuteAsync(commandName, param, null, null, commandType);
                    res.Success = true;
                    res.ResultInt = result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(classMeth + "Error occured while executing " + commandName + " " + ex.Message + " " + ex.StackTrace);
                res.Success = false;
                res.ErrorMessage = ex.ToString();
            }
            return res;
        }
    }
}
