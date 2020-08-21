using System.Collections.Generic;

namespace Domain.Transactions
{
    public interface ITransactionReader
    {
        IEnumerable<Transaction> Read(string pathOfxFile);
    }
}