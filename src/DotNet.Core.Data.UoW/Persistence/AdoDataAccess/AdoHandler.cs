using System.Data;
using DotNet.Core.Data.UoW.Persistence.AdoDataAccess.Interfaces;
using Microsoft.Data.SqlClient;

namespace DotNet.Core.Data.UoW.Persistence.AdoDataAccess
{
  internal class AdoHandler : IAdoHandler
  {
    private readonly IDbConnection _connection;

    public AdoHandler(IDbConnection connection)
    {
      _connection = connection;
    }

    public IDbConnection CreateConnection()
    {
      return new SqlConnection(_connection.ConnectionString);
    }
    public void CloseConnection(IDbConnection connection)
    {
      var sqlConnection = (SqlConnection)connection;
      sqlConnection.Close();
      sqlConnection.Dispose();
    }
    public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
    {
      return new SqlCommand
      {
        CommandText = commandText,
        Connection = (SqlConnection)connection,
        CommandType = commandType
      };
    }
    public IDataAdapter CreateAdapter(IDbCommand command)
    {
      return new SqlDataAdapter((SqlCommand)command);
    }

    public IDbDataParameter CreateParameter(IDbCommand command)
    {
      var sqlCommand = (SqlCommand)command;
      return sqlCommand.CreateParameter();
    }
  }
}
