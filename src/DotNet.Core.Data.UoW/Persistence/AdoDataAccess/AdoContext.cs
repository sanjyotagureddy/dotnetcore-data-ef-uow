using System;
using System.Data;
using DotNet.Core.Data.UoW.Persistence.AdoDataAccess.Interfaces;
using Microsoft.Data.SqlClient;

namespace DotNet.Core.Data.UoW.Persistence.AdoDataAccess
{
  public sealed class AdoContext : IAdoContext
  {
    private readonly IAdoHandler _database;

    public AdoContext(IDbConnection connection)
    {
      _database = new AdoHandler(connection);
    }

    public IDbConnection GetDatabaseConnection()
    {
      return _database.CreateConnection();
    }

    public void CloseConnection(IDbConnection connection)
    {
      _database.CloseConnection(connection);
    }

    public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
    {
      return CreateSqlParameter(name, value, dbType, ParameterDirection.Input);
    }

    public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
    {
      return CreateSqlParameter(name, size, value, dbType, ParameterDirection.Input);
    }

    public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
    {
      return CreateSqlParameter(name, size, value, dbType, direction);
    }

    public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var dataSet = new DataSet();
      var dataAdapter = _database.CreateAdapter(command);
      dataAdapter.Fill(dataSet);

      return dataSet.Tables[0];
    }

    public DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var dataSet = new DataSet();
      var dataAdapter = _database.CreateAdapter(command);
      dataAdapter.Fill(dataSet);

      return dataSet;
    }

    public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, out IDbConnection connection)
    {
      connection = _database.CreateConnection();
      connection.Open();

      var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var reader = command.ExecuteReader();

      return reader;
    }

    public int Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      return command.ExecuteNonQuery();
    }

    public int Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Returns the inserted Id
    /// </summary>
    /// <param name="commandText"></param>
    /// <param name="commandType"></param>
    /// <param name="parameters"></param>
    /// <param name="lastId"></param>
    /// <returns></returns>
    public void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out long lastId)
    {
      lastId = 0;
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var newId = command.ExecuteScalar();
      lastId = Convert.ToInt64(newId);
    }

    public int InsertWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      int records;
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        records = command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
        records = -1;
      }
      finally
      {
        connection.Close();
      }

      return records;
    }

    public int InsertWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, IDbDataParameter[] parameters)
    {
      int records;
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction(isolationLevel);

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        records = command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
        records = -1;
      }
      finally
      {
        connection.Close();
      }

      return records;
    }

    public int Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      return command.ExecuteNonQuery();
    }

    public int UpdateWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      int records;
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        records = command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
        records = -1;
      }
      finally
      {
        connection.Close();
      }

      return records;
    }

    public int UpdateWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, IDbDataParameter[] parameters)
    {
      int records;
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction(isolationLevel);

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        records = command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
        records = -1;
      }
      finally
      {
        connection.Close();
      }

      return records;
    }

    public object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters == null) return command.ExecuteScalar();
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }

      return command.ExecuteScalar();
    }


    #region Private Methods

    private static IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction)
    {
      return new SqlParameter
      {
        DbType = dbType,
        ParameterName = name,
        Direction = direction,
        Value = value
      };
    }

    private static IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
    {
      return new SqlParameter
      {
        DbType = dbType,
        Size = size,
        ParameterName = name,
        Direction = direction,
        Value = value
      };
    }

    #endregion
  }
}
