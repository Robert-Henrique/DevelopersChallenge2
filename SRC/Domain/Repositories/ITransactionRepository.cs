using System.Collections.Generic;
using Domain.Transactions;

namespace Domain.Repositories
{
    public interface ITransactionRepository
    {
        void Salve(Transaction transaction);

        bool TransactionExists(Transaction transaction);

        IEnumerable<Transaction> GetAll();
    } 
}