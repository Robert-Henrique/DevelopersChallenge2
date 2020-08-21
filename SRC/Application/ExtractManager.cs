using Domain.Repositories;
using Domain.Transactions;

namespace Application
{
    public class ExtractManager : IExtractManager
    {
        private readonly ITransactionReader _transactionReader;
        private readonly ITransactionRepository _transactionRepository;

        public ExtractManager(ITransactionReader transactionReader, 
            ITransactionRepository transactionRepository)
        {
            _transactionReader = transactionReader;
            _transactionRepository = transactionRepository;
        }

        public void ManageExtract(string pathOfxFile)
        {
            var transactions = _transactionReader.Read(pathOfxFile);

            foreach (var transaction in transactions)
            {
                var transactionExists = _transactionRepository.TransactionExists(transaction);

                if (transactionExists)
                    continue;

                _transactionRepository.Salve(transaction);
            }
        }
    }
}