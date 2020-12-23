using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace DataAccessor
{
    public interface IDbInterfacing
    {
         Task<SqlModelRes<T>> GetList<T>(string connString, string commandName, CommandType commandType, DynamicParameters param);
         Task<SqlModelRes<T>> GetOneItem<T>(string connString, string commandName, CommandType commandType, DynamicParameters param);
         Task<SqlModelRes<int>> ModifyDB(string connString, string commandName, CommandType commandType, DynamicParameters param);
    }
}