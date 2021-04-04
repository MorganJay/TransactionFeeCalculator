using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TransactionFeeCalculator
{
    public static class CalculatorFunctions
    {
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
}