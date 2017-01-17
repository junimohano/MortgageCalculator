using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MortgageCalculator.ViewModels;

namespace MortgageCalculator.Models
{
    public class Mortgage
    {
        public int No { get; set; }
        public DateTime DueDate { get; set; }
        public double Payment { get; set; }
        public double ExtraPayments { get; set; }
        public double AdditionalPayment { get; set; }
        public double Interest { get; set; }
        public double Principal { get; set; }
        public double Balance { get; set; }
    }
}
