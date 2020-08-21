using System;

namespace WebApp.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime DatePosted { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
    }
}