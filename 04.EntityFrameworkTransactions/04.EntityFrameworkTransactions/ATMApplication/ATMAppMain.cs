using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATMApplication
{
    class ATMAppMain
    {
        static void Main()
        {
            var context = new ATMEntities();
            string cardNumber = String.Empty;
            decimal amountToWithdraw = 0;

            var accountToWithdraw = context.CardAccounts.Find(1);
            var threwException = false;

            // Unfinished!

            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter the card number: ");
                    cardNumber = Console.ReadLine();
                    Console.WriteLine("Please enter the card PIN: ");
                    string cardPin = Console.ReadLine();
                    Console.WriteLine("Please enter the amount to withdraw: ");
                    amountToWithdraw = decimal.Parse(Console.ReadLine());

                    if (amountToWithdraw > accountToWithdraw.CardCash || accountToWithdraw.CardCash == null)
                    {
                        throw new ArgumentOutOfRangeException(
                            "You cannot withdraw more cash than what you currently have: " + accountToWithdraw.CardCash);
                    }

                
                    using (var transaction = context.Database.BeginTransaction())
                    {
                      
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    threwException = true;
                }

                if (!threwException)
                {
                    context.TransactionHistories.Add(new TransactionHistory()
                    {
                        CardNumber = cardNumber,
                        Amount = amountToWithdraw,
                        TransactioDate = DateTime.Now
                    });
                }
            }
        }
    }
}
