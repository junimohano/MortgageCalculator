using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MortgageCalculator.ViewModels;

namespace MortgageCalculator.Services
{
    public class MortageService
    {
        private static readonly MortgagePageViewModel _mortageViewModel = new MortgagePageViewModel();
        public MortgagePageViewModel MortageViewModel => _mortageViewModel;
    }
}
