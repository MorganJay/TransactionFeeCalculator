using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TransactionFeeCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jsonFilePath = "fees.config.json";
            var feeConfig = CalculatorFunctions.GetFeeConfig(jsonFilePath);
            bool pass;
            Console.WriteLine("Welcome, how much would you like to transfer? ");
            pass = decimal.TryParse(Console.ReadLine(), out decimal transaction);

            if (!pass)
            {
                do
                {
                    Console.WriteLine("Please enter a valid amount to transfer");
                    pass = decimal.TryParse(Console.ReadLine(), out transaction);
                } while (!pass);
            }
            var transferCharge = CalculatorFunctions.CalculateTransactionFee(transaction, feeConfig);

            Console.WriteLine($"You will be charged {transferCharge} for a transfer of {transaction}. \nThank you for banking with us");

            Console.ReadLine();
        }
    }
}