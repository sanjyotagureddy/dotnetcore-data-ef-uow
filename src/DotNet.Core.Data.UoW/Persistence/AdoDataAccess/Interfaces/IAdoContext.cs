using System.Data;

namespace DotNet.Core.Data.UoW.Persistence.AdoDataAccess.Interfaces
{
  public interface IAdoContext
  {
    IDbConnection GetDatabaseConnection();
    void CloseConnection(IDbConnection connection);
    IDbDataParameter CreateParameter(string name, object value, DbType dbType);
    IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType);
    IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction);
    DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);
    DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);

    IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters,
      out IDbConnection connection);

    int Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);
    int Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters);
    void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out long lastId);
    int InsertWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters);

    int InsertWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel,
      IDbDataParameter[] parameters);
    int Update(string commandText, CommandType commandType, IDbDataParameter[] parameters);
    int UpdateWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters);

    int UpdateWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel,
      IDbDataParameter[] parameters);

    object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);

  }
}
