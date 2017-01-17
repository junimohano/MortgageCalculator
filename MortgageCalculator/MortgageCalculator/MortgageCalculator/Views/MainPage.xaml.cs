using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using MortgageCalculator.ViewModels;
using Xamarin.Forms;

namespace MortgageCalculator.Views
{
    public partial class MainPage : TabbedPage
    {
        private readonly MortgagePageViewModel _mortgagePageViewModel;
        public MainPage()
        {
            InitializeComponent();

            //var navigationPage = new NavigationPage(new MortgageCalculatorPage());
            //navigationPage.Icon = "schedule.png";
            //navigationPage.Title = "Schedule";

            //Children.Add(navigationPage);
            //Children.Add(new HomeOwnershipExpenseCalPage());

            var mortgagePage = new MortgagePage()
            {
                Title = "Mortgage"
            };

            _mortgagePageViewModel = new MortgagePageViewModel(mortgagePage);

            //if (Application.Current.Properties.ContainsKey("LoanAmount"))
            //{
            //    _mortgagePageViewModel.LoanAmount = (string)Application.Current.Properties["LoanAmount"];
            //    _mortgagePageViewModel.CanadianMortgageRate = (string)Application.Current.Properties["CanadianMortgageRate"];
            //    _mortgagePageViewModel.CompoundPeriodIndex = (int)Application.Current.Properties["CompoundPeriodIndex"];
            //    _mortgagePageViewModel.AmortizationPeriod = (int)Application.Current.Properties["AmortizationPeriod"];
            //    _mortgagePageViewModel.Term = (int)Application.Current.Properties["Term"];
            //    _mortgagePageViewModel.FirstPaymentDate = (DateTime)Application.Current.Properties["FirstPaymentDate"];
            //    _mortgagePageViewModel.PaymentFrequencyIndex = (int)Application.Current.Properties["PaymentFrequencyIndex"];
            //    _mortgagePageViewModel.ExtraPayment = (string)Application.Current.Properties["ExtraPayment"];
            //    _mortgagePageViewModel.PaymentInterval = (int)Application.Current.Properties["PaymentInterval"];
            //    _mortgagePageViewModel.ExtraAnnualPayment = (string)Application.Current.Properties["ExtraAnnualPayment"];
            //}

            mortgagePage.BindingContext = _mortgagePageViewModel;

            Children.Add(mortgagePage);
            Children.Add(new MortgageTablePage((MortgagePageViewModel)mortgagePage.BindingContext)
            {
                Title = "Mortgage Table"
            });
            Children.Add(new MortgageGuidePage()
            {
                Title = "Guide"
            });
        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //Application.Current.Properties["LoanAmount"] = _mortgagePageViewModel.LoanAmount;
            //Application.Current.Properties["CanadianMortgageRate"] = _mortgagePageViewModel.CanadianMortgageRate;
            //Application.Current.Properties["CompoundPeriodIndex"] = _mortgagePageViewModel.CompoundPeriodIndex;
            //Application.Current.Properties["AmortizationPeriod"] = _mortgagePageViewModel.AmortizationPeriod;
            //Application.Current.Properties["Term"] = _mortgagePageViewModel.Term;
            //Application.Current.Properties["FirstPaymentDate"] = _mortgagePageViewModel.FirstPaymentDate;
            //Application.Current.Properties["PaymentFrequencyIndex"] = _mortgagePageViewModel.PaymentFrequencyIndex;
            //Application.Current.Properties["ExtraPayment"] = _mortgagePageViewModel.ExtraPayment;
            //Application.Current.Properties["PaymentInterval"] = _mortgagePageViewModel.PaymentInterval;
            //Application.Current.Properties["ExtraAnnualPayment"] = _mortgagePageViewModel.ExtraAnnualPayment;
        }
    }
}
