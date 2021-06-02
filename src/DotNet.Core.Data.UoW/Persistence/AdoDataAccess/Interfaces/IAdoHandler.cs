using System.Data;

namespace DotNet.Core.Data.UoW.Persistence.AdoDataAccess.Interfaces
{
  public interface IAdoHandler
  {
    IDbConnection CreateConnection();
    void CloseConnection(IDbConnection connection);
    IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
    IDataAdapter CreateAdapter(IDbCommand command);
    IDbDataParameter CreateParameter(IDbCommand command);
  }
}
