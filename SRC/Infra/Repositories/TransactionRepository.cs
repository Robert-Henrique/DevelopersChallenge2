using System;
using Domain.Repositories;
using Infra.DataBase;
using System.Collections.Generic;
using System.Data;
using Domain.Transactions;

namespace Infra.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ConnectionDb _connectionDb;

        public TransactionRepository()
        {
            _connectionDb = new ConnectionDb();
        }

        public void Salve(Transaction transaction)
        {
            var parameters = ConfigureParameters(transaction);

            const string script = @"INSERT INTO [dbo].[Transaction] (Type, DatePosted, Amount, Memo) VALUES (@Type, @DatePosted, @Amount, @Memo)";
            _connectionDb.GetDataFromSQLServer(script, parameters);
        }

        public bool TransactionExists(Transaction transaction)
        {
            var parameters = ConfigureParameters(transaction);

            const string script = @"SELECT Id 
                                    FROM [dbo].[Transaction]
                                    WHERE Type = @Type AND
                                          DatePosted = @DatePosted AND
                                          Amount = @Amount AND
                                          Memo = @Memo";

            var dataSet = _connectionDb.GetDataFromSQLServer(script, parameters);
            return dataSet != null && dataSet.Tables[0].Rows.Count > 0;
        }

        public IEnumerable<Transaction> GetAll()
        {
            var transactions = new List<Transaction>();
            const string script = @"SELECT Id, Type, DatePosted, Amount, Memo
                                    FROM [dbo].[Transaction]
                                    Order BY DatePosted";

            var dataSet = _connectionDb.GetDataFromSQLServer(script);
            var recordsExist = dataSet != null && dataSet.Tables[0].Rows.Count > 0;

            if (!recordsExist)
                return transactions;

            var dataTable = dataSet.Tables[0];

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var id = Convert.ToInt32(dataRow["Id"]);
                var type = dataRow["Type"].ToString();
                var datePosted = Convert.ToDateTime(dataRow["DatePosted"]);
                var amount = Convert.ToDecimal(dataRow["Amount"]);
                var memo = dataRow["Memo"].ToString();
                transactions.Add(new Transaction(id, type, datePosted, amount, memo));
            }

            return transactions;
        }

        private static Dictionary<string, object> ConfigureParameters(Transaction transaction)
        {
            return new Dictionary<string, object>
            {
                ["Type"] = transaction.Type,
                ["DatePosted"] = transaction.DatePosted,
                ["Amount"] = transaction.Amount,
                ["Memo"] = transaction.Memo
            };
        }
    }
}