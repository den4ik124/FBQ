using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ_app
{
    internal class Program
    {
        public ref struct Transaction
        {
            public readonly int Amount;
            public readonly string To;

            public Transaction(in int amount, in string to)
            {
                Amount = amount;
                To = to;
            }
        }

        private abstract class TransactionBuilder<Self> where Self : TransactionBuilder<Self>
        {
            protected int amount;
            protected string to;

            public Self WithAmount(int amount)
            {
                this.amount = amount;
                return This();
            }

            public Self To(string to)
            {
                this.to = to;
                return This();
            }

            protected abstract Self This();

            //public TransactionBuilder To(string to)
            //{
            //    this.to = to;
            //    return this;
            //}
        }

        private class TransactionGetter : TransactionBuilder<TransactionGetter>
        {
            public TransactionGetter(string to)
            {
                this.to = to;
            }

            public Transaction GetTransaction() => new Transaction(amount, to);

            protected override TransactionGetter This() => this;
        }

        private static void Main(string[] args)
        {
            //TransactionBuilder tb = new TransactionBuilder();
            //Transaction transaction = tb.To("Denys")
            //    .WithAmount(100)
            //    .GetTransaction();

            //TransactionBuilder tb = new TransactionBuilder();
            //Transaction transaction = tb    //.To("Denys")
            //    .WithAmount(100)
            //    .GetTransaction();

            Transaction transaction = new TransactionGetter("Denys")
                .WithAmount(100)
                .GetTransaction();

            Transaction newTransaction = new TransactionGetter("Boryhin").WithAmount(1000).GetTransaction();

            Console.WriteLine(transaction.To);
            Console.WriteLine(transaction.Amount);
        }
    }
}