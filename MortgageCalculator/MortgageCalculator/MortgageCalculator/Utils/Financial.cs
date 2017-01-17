using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Utils
{
    public class Financial
    {
        public enum DueDate
        {
            EndOfPeriod,
            BegOfPeriod,
        }
        public static double Pmt(double Rate, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
        {
            return PMT_Internal(Rate, NPer, PV, FV, Due);
        }

        private static double PMT_Internal(double Rate, double NPer, double PV, double FV = 0.0, DueDate Due = DueDate.EndOfPeriod)
        {
            if (NPer == 0.0)
                return 0;

            double num1;
            if (Rate == 0.0)
            {
                num1 = (-FV - PV) / NPer;
            }
            else
            {
                double num2 = Due == DueDate.EndOfPeriod ? 1.0 : 1.0 + Rate;
                double num3 = Math.Pow(Rate + 1.0, NPer);
                num1 = (-FV - PV * num3) / (num2 * (num3 - 1.0)) * Rate;
            }
            return num1;
        }
    }
}
