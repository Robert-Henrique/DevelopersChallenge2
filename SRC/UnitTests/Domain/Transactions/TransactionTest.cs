using System;
using ExpectedObjects;
using NUnit.Framework;
using Domain.Transactions;

namespace UnitTests.Domain.Transactions
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void MustCreateTransaction()
        {
            var datePosted = DateTime.Now;
            var expectedTransaction = new
            {
                Id = 0,
                Type = "DEBIT",
                DatePosted = datePosted,
                Amount = 104.55m,
                Memo = "SAQUE 24H 12725743"
            }.ToExpectedObject();

            var transaction = new Transaction(0, "DEBIT", datePosted, 104.55m, "SAQUE 24H 12725743");

            expectedTransaction.ShouldMatch(transaction);
        }

        [Test]
        public void MustChangeType()
        {
            const string expectedType = "CREDIT";
            var transaction = new Transaction(0, "DEBIT", DateTime.Now, 104.55m, "SAQUE 24H 12725743");

            transaction.AddType(expectedType);

            Assert.AreEqual(expectedType, transaction.Type);
        }

        [Test]
        public void MustChangeDate()
        {
            var expectedDate = DateTime.Now;
            var transaction = new Transaction(0, "DEBIT", DateTime.Now, 104.55m, "SAQUE 24H 12725743");

            transaction.AddDate(expectedDate);

            Assert.AreEqual(expectedDate, transaction.DatePosted);
        }

        [Test]
        public void MustChangeAmount()
        {
            const decimal expectedAmount = 140.0m;
            var transaction = new Transaction(0, "DEBIT", DateTime.Now, 104.55m, "SAQUE 24H 12725743");

            transaction.AddAmount(expectedAmount);

            Assert.AreEqual(expectedAmount, transaction.Amount);
        }

        [Test]
        public void MustChangeDescription()
        {
            const string expectedDescription = "INT APLICACAO SPECIAL RF";
            var transaction = new Transaction(0, "DEBIT", DateTime.Now, 104.55m, "SAQUE 24H 12725743");

            transaction.AddMemo(expectedDescription);

            Assert.AreEqual(expectedDescription, transaction.Memo);
        }
    }
}