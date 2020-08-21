using Application;
using Domain.Repositories;
using Domain.Transactions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnitTests.Builders;

namespace UnitTests.Application
{
    [TestFixture]
    public class ExtractManagerTest
    {
        private Mock<ITransactionReader> _transactionReader;
        private Mock<ITransactionRepository> _transactionRepository;
        private ExtractManager _extractManager;

        private const string PathOfxFile = "c:\\file\\";

        [SetUp]
        public void SetUp()
        {
            _transactionReader = new Mock<ITransactionReader>();
            _transactionRepository = new Mock<ITransactionRepository>();

            _extractManager = new ExtractManager(_transactionReader.Object, _transactionRepository.Object);
        }

        [Test]
        public void MustReadTransactions()
        {
            _extractManager.ManageExtract(PathOfxFile);

            _transactionReader.Verify(service => service.Read(PathOfxFile), Times.Once);
        }

        [Test]
        public void DuplicateTransactionMustNotBeSaved()
        {
            var transaction = TransactionBuilder.ATransaction().Build();
            _transactionReader.Setup(transactionReader => transactionReader.Read(PathOfxFile)).Returns(new List<Transaction> { transaction });
            _transactionRepository.Setup(repository => repository.TransactionExists(transaction)).Returns(true);

            _extractManager.ManageExtract(PathOfxFile);

            _transactionRepository.Verify(repository => repository.Salve(transaction), Times.Never);
        }

        [Test]
        public void MustSaveTransaction()
        {
            var transaction = TransactionBuilder.ATransaction().Build();
            _transactionReader.Setup(transactionReader => transactionReader.Read(PathOfxFile)).Returns(new List<Transaction> { transaction });
            _transactionRepository.Setup(repository => repository.TransactionExists(transaction)).Returns(false);

            _extractManager.ManageExtract(PathOfxFile);

            _transactionRepository.Verify(repository => repository.Salve(transaction), Times.Once);
        }
    }
}