using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TransactionFeeCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jsonFilePath = @"C:\Users\JamesMorgan\Desktop\fees.config.json";
            var feeConfig = GetFeeConfig(jsonFilePath);
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
            var transferCharge = CalculateTransactionFee(transaction, feeConfig);

            Console.WriteLine($"You will be charged {transferCharge} for a transfer of {transaction}. \nThank you for banking with us");

            Console.ReadLine();
        }

        public static decimal CalculateTransactionFee(decimal amount, List<Fee> feesConfig)
        {
            decimal charge = 0.0m;
            foreach (var fee in feesConfig)
            {
                if (amount >= fee.MinAmount && amount <= fee.MaxAmount)
                {
                    charge = fee.FeeAmount;
                }
            }
            return charge;
        }

        public static List<Fee> GetFeeConfig(string path)
        {
            string json = File.ReadAllText(path);
            var fees = JsonConvert.DeserializeObject<Root>(json);
            return fees.Fees;
        }
    }

    public class Fee
    {
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public int FeeAmount { get; set; }
    }

    public class Root
    {
        public List<Fee> Fees { get; set; }
    }
}