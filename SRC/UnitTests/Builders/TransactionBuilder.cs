using Domain.Transactions;
using System;

namespace UnitTests.Builders
{
    public class TransactionBuilder
    {
        private int _id = 1;
        private string _type = "DEBIT";
        private DateTime _datePosted = DateTime.Now;
        private decimal _amount = 104.45m;
        private string _description = "TAR DOC INTERNET";

        public static TransactionBuilder ATransaction()
        {
            return new TransactionBuilder();
        }

        public TransactionBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public TransactionBuilder WithType(string type)
        {
            _type = type;
            return this;
        }

        public TransactionBuilder WithDatePosted(DateTime datePosted)
        {
            _datePosted = datePosted;
            return this;
        }

        public TransactionBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public TransactionBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Transaction Build()
        {
            return new Transaction(_id, _type, _datePosted, _amount, _description);
        }
    }
}