using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IActionResult Index()
        {
            var transactions = _transactionRepository.GetAll();
            var transactionsViewModel = transactions.Select(transaction => new TransactionViewModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                DatePosted = transaction.DatePosted,
                Memo = transaction.Memo,
                Type = transaction.Type
            }).ToList();

            return View(transactionsViewModel);
        }
    }
}
