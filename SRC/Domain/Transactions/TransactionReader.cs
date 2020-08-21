using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain.Transactions
{
    public class TransactionReader : ITransactionReader
    {
        public IEnumerable<Transaction> Read(string pathOfxFile)
        {
            var allLinesOfTheFile = from line in File.ReadAllLines(pathOfxFile) select line;

            Transaction transaction = null;
            var transactions = new List<Transaction>();

            foreach (var line in allLinesOfTheFile)
            {
                transaction = CreateTransaction(line.Trim(), transaction);

                if (transaction != null && transaction.ThisFilled)
                    transactions.Add(transaction);
            }

            return transactions;
        }

        private static Transaction CreateTransaction(string line, Transaction transaction)
        {
            if (line.Equals("<STMTTRN>") || line.Equals("</STMTTRN>"))
            {
                transaction = new Transaction();
            }

            if (line.StartsWith("<TRNTYPE>"))
            {
                var type = line.Replace("<TRNTYPE>", "");
                transaction.AddType(type);
            }

            if (line.StartsWith("<DTPOSTED>"))
            {
                var datePosted = line.Replace("<DTPOSTED>", "").Substring(0, 8);
                var date = DateTime.ParseExact(datePosted, "yyyyMMdd", null);
                transaction.AddDate(date);
            }

            if (line.StartsWith("<TRNAMT>"))
            {
                var amount = line.Replace("<TRNAMT>", "").Replace(".", ",");
                transaction.AddAmount(Convert.ToDecimal(amount));
            }

            if (!line.StartsWith("<MEMO>")) return transaction;
            var memo = line.Replace("<MEMO>", "");
            transaction.AddMemo(memo);

            return transaction;
        }
    }
}
