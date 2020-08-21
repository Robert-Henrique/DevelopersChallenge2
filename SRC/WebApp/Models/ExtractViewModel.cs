using System.Collections.Generic;
using System.Transactions;

namespace WebApp.Models
{
    public class ExtractViewModel
    {
        public List<Transaction> Transactions { get; set; }
    }
}