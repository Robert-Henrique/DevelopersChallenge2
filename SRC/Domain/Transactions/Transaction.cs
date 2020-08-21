using System;

namespace Domain.Transactions
{
    public class Transaction
    {
        public int Id { get; protected set; }
        public string Type { get; protected set; }
        public DateTime DatePosted { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Memo { get; protected set; }
        public bool ThisFilled => !string.IsNullOrEmpty(Type) && DatePosted > DateTime.MinValue && !string.IsNullOrEmpty(Memo);

        public Transaction() { }

        public Transaction(int id, string type, DateTime datePosted, decimal amount, string memo)
        {
            Id = id;
            Type = type;
            DatePosted = datePosted;
            Amount = amount;
            Memo = memo;
        }

        public void AddType(string type)
        {
            Type = type;
        }

        public void AddDate(DateTime date)
        {
            DatePosted = date;
        }

        public void AddAmount(decimal amount)
        {
            Amount = amount;
        }

        public void AddMemo(string memo)
        {
            Memo = memo;
        }
    }
}