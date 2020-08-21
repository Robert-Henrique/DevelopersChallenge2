using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infra.DataBase
{
    public class ConnectionDb : IConnectionDb
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BankReconciliation;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DataSet GetDataFromSQLServer(string script, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var dataTable = new DataTable();
                var dataSet = new DataSet();
                connection.Open();

                try
                {
                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandTimeout = 60;

                        if (parameters != null)
                            SetParameters(command, parameters);

                        var sqlDataReader = command.ExecuteReader();
                        dataTable.Load(sqlDataReader);
                        dataSet.Tables.Add(dataTable);
                        return dataSet;
                    }
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                    dataTable.Dispose();
                    dataSet.Dispose();
                }
            }
        }

        public void SaveDataSQLServer(string script, Dictionary<string, object> parameters)
        {
            script = "SET DATEFORMAT DMY; " + script;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var command = new SqlCommand(script, connection))
                    {
                        SetParameters(command, parameters);
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        protected void SetParameters(SqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (var item in parameters)
            {
                if (item.Value == null || string.IsNullOrEmpty(item.Value.ToString()))
                {
                    var sqlParameter = new SqlParameter {ParameterName = item.Key, Value = DBNull.Value};
                    command.Parameters.Add(sqlParameter);
                }
                else
                {
                    if (item.Value is string)
                    {
                        var sqlParameter = new SqlParameter
                        {
                            SqlDbType = SqlDbType.VarChar,
                            ParameterName = item.Key, 
                            Value = item.Value
                        };
                        command.Parameters.Add(sqlParameter);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
            }
        }
    }
}
