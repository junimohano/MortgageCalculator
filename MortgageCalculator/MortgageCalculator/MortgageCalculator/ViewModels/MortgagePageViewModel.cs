using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.VisualBasic;
using MortgageCalculator.Models;
using MortgageCalculator.Utils;

namespace MortgageCalculator.ViewModels
{
    public class MortgagePageViewModel : INotifyPropertyChanged
    {
        private readonly INavigation _navigation;
        private readonly Page _page;

        private string _loanAmount = "410000";
        private string _canadianMortgageRate = "2.4";
        private int _compoundPeriodIndex = 0;
        private int _amortizationPeriod = 25;
        private int _term = 5;
        private DateTime _firstPaymentDate = new DateTime(2011, 8, 1);
        private int _paymentFrequencyIndex;
        private double _payment;

        private string _extraPayment = "0";
        private int _paymentInterval = 1;
        private string _extraAnnualPayment = "0";

        private double _interestRate;
        private double _totalPayments;
        private double _totalInterest;
        private int _numberOfPayments;
        private decimal _numberOfPaymentsYears;
        private DateTime _lastPaymentDate;
        private double _interestSavings;

        private DateTime _dateAtTerm = new DateTime(2011, 8, 1);
        private double _interestPaid;
        private double _principalPaid;
        private double _outstandingBalance;
        private double _totalPaymentsWithNoExtraPayment;
        private double _totalInterestWithNoExtraPayment;

        private bool _isBusy = false;
        private bool _isNotBusy = true;

        private ObservableCollection<Mortgage> _mortgages;

        public MortgagePageViewModel()
        {
        }

        public MortgagePageViewModel(Page page)
        {
            _navigation = page.Navigation;
            _page = page;

            RunCommand = new Command(RunEnter);
        }

        public ICommand RunCommand { get; private set; }


        #region values
        public ObservableCollection<Mortgage> Mortgages
        {
            get { return _mortgages; }
            set
            {
                _mortgages = value;
                OnPropertyChanged(nameof(Mortgages));
            }
        }

        public string LoanAmount
        {
            get { return _loanAmount; }
            set
            {
                double temp;
                if (double.TryParse(value.Replace(".", string.Empty), out temp))
                {
                    _loanAmount = value;
                    OnPropertyChanged(nameof(LoanAmount));
                    Calculator();
                }
            }
        }

        public string CanadianMortgageRate
        {
            get { return _canadianMortgageRate; }
            set
            {
                double temp;
                if (double.TryParse(value.Replace(".", string.Empty), out temp))
                {
                    _canadianMortgageRate = value;
                    OnPropertyChanged(nameof(CanadianMortgageRate));
                    Calculator();
                }
            }
        }

        public List<string> CompoundPeriods => new List<string>
        {
            "Semi-Annually",
            "Monthly",
            "Quarterly",
            "Annually"
        };

        public int CompoundPeriodIndex
        {
            get { return _compoundPeriodIndex; }
            set
            {
                _compoundPeriodIndex = value;
                OnPropertyChanged(nameof(CompoundPeriodIndex));
                Calculator();
            }
        }

        public int CompoundPeriod
        {
            get
            {
                switch (CompoundPeriodIndex)
                {
                    // Semi-Annually
                    case 0:
                        return 2;
                    // Monthly
                    case 1:
                        return 12;
                    // Quarterly
                    case 2:
                        return 4;
                    // Annually
                    case 3:
                        return 1;
                }
                return 0;
            }
        }

        public int AmortizationPeriod
        {
            get { return _amortizationPeriod; }
            set
            {
                _amortizationPeriod = value;
                OnPropertyChanged(nameof(AmortizationPeriod));
                Calculator();
            }
        }

        public int Term
        {
            get { return _term; }
            set
            {
                _term = value;
                OnPropertyChanged(nameof(Term));
                Calculator();
            }
        }

        public DateTime FirstPaymentDate
        {
            get { return _firstPaymentDate; }
            set
            {
                _firstPaymentDate = value;
                OnPropertyChanged(nameof(FirstPaymentDate));
                Calculator();
            }
        }

        public List<string> PaymentFrequencies => new List<string>
        {
            "Annually",
            "Asemi-Annually",
            "Quarterly",
            "Bi-Monthly",
            "Monthly",
            "Semi-Monthly",
            "Bi-Weekly",
            "Weekly"
        };

        public int PaymentFrequencyIndex
        {
            get { return _paymentFrequencyIndex; }
            set
            {
                _paymentFrequencyIndex = value;
                OnPropertyChanged(nameof(PaymentFrequencyIndex));
                Calculator();
            }
        }

        public int PeriodsPerYear
        {
            get
            {
                switch (PaymentFrequencyIndex)
                {
                    // Annually
                    case 0:
                        return 1;
                    // Asemi-Annually
                    case 1:
                        return 2;
                    // Quarterly
                    case 2:
                        return 4;
                    // Bi-Monthly
                    case 3:
                        return 6;
                    // Monthly
                    case 4:
                        return 12;
                    // Semi-Monthly
                    case 5:
                        return 24;
                    // Bi-Weekly
                    case 6:
                        return 26;
                    // Weekly
                    case 7:
                        return 52;
                }
                return 0;
            }
        }

        public double MonthsPerPeriod
        {
            get
            {
                switch (PaymentFrequencyIndex)
                {
                    // Annually
                    case 0:
                        return 12;
                    // Asemi-Annually
                    case 1:
                        return 6;
                    // Quarterly
                    case 2:
                        return 3;
                    // Bi-Monthly
                    case 3:
                        return 2;
                    // Monthly
                    case 4:
                        return 1;
                    // Semi-Monthly
                    case 5:
                        return 0.5;
                    // Bi-Weekly
                    case 6:
                        return 0.5;
                    // Weekly
                    case 7:
                        return 0.25;
                }
                return 0;
            }
        }

        public double Payment
        {
            get { return _payment; }
            set
            {
                _payment = value;
                OnPropertyChanged(nameof(Payment));
            }
        }

        public string ExtraPayment
        {
            get { return _extraPayment; }
            set
            {
                double temp;
                if (double.TryParse(value.Replace(".", string.Empty), out temp))
                {
                    _extraPayment = value;
                    OnPropertyChanged(nameof(ExtraPayment));
                    Calculator();
                }
            }
        }

        public int PaymentInterval
        {
            get { return _paymentInterval; }
            set
            {
                _paymentInterval = value;
                OnPropertyChanged(nameof(PaymentInterval));
                Calculator();
            }
        }

        public string ExtraAnnualPayment
        {
            get { return _extraAnnualPayment; }
            set
            {
                double temp;
                if (double.TryParse(value.Replace(".", string.Empty), out temp))
                {
                    _extraAnnualPayment = value;
                    OnPropertyChanged(nameof(ExtraAnnualPayment));
                    Calculator();
                }
            }
        }

        public double InterestRate
        {
            get { return _interestRate; }
            set
            {
                _interestRate = value;
                OnPropertyChanged(nameof(InterestRate));
            }
        }

        public double TotalPayments
        {
            get { return _totalPayments; }
            set
            {
                _totalPayments = value;
                OnPropertyChanged(nameof(TotalPayments));
            }
        }

        public double TotalInterest
        {
            get { return _totalInterest; }
            set
            {
                _totalInterest = value;
                OnPropertyChanged(nameof(TotalInterest));
            }
        }

        public int NumberOfPayments
        {
            get { return _numberOfPayments; }
            set
            {
                _numberOfPayments = value;
                OnPropertyChanged(nameof(NumberOfPayments));
            }
        }

        public decimal NumberOfPaymentsYears
        {
            get { return _numberOfPaymentsYears; }
            set
            {
                _numberOfPaymentsYears = value;
                OnPropertyChanged(nameof(NumberOfPaymentsYears));
            }
        }

        public DateTime LastPaymentDate
        {
            get { return _lastPaymentDate; }
            set
            {
                _lastPaymentDate = value;
                OnPropertyChanged(nameof(LastPaymentDate));
            }
        }

        public double InterestSavings
        {
            get { return _interestSavings; }
            set
            {
                _interestSavings = value;
                OnPropertyChanged(nameof(InterestSavings));
            }
        }

        public DateTime DateAtTerm
        {
            get { return _dateAtTerm; }
            set
            {
                _dateAtTerm = value;
                OnPropertyChanged(nameof(DateAtTerm));
            }
        }

        public double InterestPaid
        {
            get { return _interestPaid; }
            set
            {
                _interestPaid = value;
                OnPropertyChanged(nameof(InterestPaid));
            }
        }

        public double PrincipalPaid
        {
            get { return _principalPaid; }
            set
            {
                _principalPaid = value;
                OnPropertyChanged(nameof(PrincipalPaid));
            }
        }

        public double OutstandingBalance
        {
            get { return _outstandingBalance; }
            set
            {
                _outstandingBalance = value;
                OnPropertyChanged(nameof(OutstandingBalance));
            }
        }

        public double TotalPaymentsWithNoExtraPayment
        {
            get { return _totalPaymentsWithNoExtraPayment; }
            set
            {
                _totalPaymentsWithNoExtraPayment = value;
                OnPropertyChanged(nameof(TotalPaymentsWithNoExtraPayment));
            }
        }

        public double TotalInterestWithNoExtraPayment
        {
            get { return _totalInterestWithNoExtraPayment; }
            set
            {
                _totalInterestWithNoExtraPayment = value;
                OnPropertyChanged(nameof(TotalInterestWithNoExtraPayment));
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                IsNotBusy = !_isBusy;
            }
        }

        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set
            {
                _isNotBusy = value;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
        #endregion

        public void Calculator()
        {
            IsBusy = true;
            try
            {
                InterestRate = Math.Pow((1 + (double.Parse(CanadianMortgageRate) / 100) / CompoundPeriod), (double)CompoundPeriod / PeriodsPerYear) - 1;
                Payment = Math.Round(-Financial.Pmt(InterestRate, AmortizationPeriod * PeriodsPerYear, double.Parse(LoanAmount)), 2);
                var nPer = AmortizationPeriod * PeriodsPerYear;

                var additionalPaymentList = Mortgages?.Where(x => x.AdditionalPayment > 0).ToList();

                if (Mortgages == null)
                    Mortgages = new ObservableCollection<Mortgage>();
                else
                    Mortgages.Clear();

                Mortgage previousMortgage = new Mortgage()
                {
                    No = 0,
                    Balance = double.Parse(LoanAmount)
                };

                while (previousMortgage.Balance != 0)
                {
                    var mortgage = new Mortgage();

                    if (previousMortgage.No >= nPer || Math.Round(previousMortgage.Balance, 2) <= 0)
                        mortgage.No = 0;
                    else
                        mortgage.No = previousMortgage.No + 1;

                    if (mortgage.No != 0)
                    {
                        // Due date
                        if (PeriodsPerYear == 26)
                        {
                            if (mortgage.No == 1)
                                mortgage.DueDate = FirstPaymentDate;
                            else
                                mortgage.DueDate = mortgage.DueDate.AddDays(14);
                        }
                        else if (PeriodsPerYear == 52)
                        {
                            if (mortgage.No == 1)
                                mortgage.DueDate = FirstPaymentDate;
                            else
                                mortgage.DueDate = mortgage.DueDate.AddDays(7);
                        }
                        else
                        {
                            mortgage.DueDate = new DateTime(FirstPaymentDate.Year, FirstPaymentDate.Month, FirstPaymentDate.Day);
                            mortgage.DueDate = mortgage.DueDate.AddMonths((int)((mortgage.No - 1) * MonthsPerPeriod));

                            if (PeriodsPerYear == 24)
                            {
                                if (1 - (mortgage.No % 2) == 1)
                                    mortgage.DueDate = mortgage.DueDate.AddDays(14);
                                else
                                    mortgage.DueDate = new DateTime(mortgage.DueDate.Year, mortgage.DueDate.Month, FirstPaymentDate.Day);
                            }
                            else
                                mortgage.DueDate = new DateTime(mortgage.DueDate.Year, mortgage.DueDate.Month, FirstPaymentDate.Day);
                        }

                        // Payment
                        if (mortgage.No == nPer || Payment > Math.Round((1 + InterestRate) * previousMortgage.Balance, 2))
                            mortgage.Payment = Math.Round((1 + InterestRate) * previousMortgage.Balance, 2);
                        else
                            mortgage.Payment = Payment;

                        // Extra Payment
                        if (previousMortgage.Balance <= Payment || mortgage.No == 0)
                            mortgage.ExtraPayments = 0;
                        else
                        {
                            if (double.Parse(ExtraAnnualPayment) > 0)
                            {
                                if (mortgage.No % PeriodsPerYear == 0)
                                    mortgage.ExtraPayments = double.Parse(ExtraAnnualPayment);
                                else
                                    mortgage.ExtraPayments = 0;
                            }
                            else
                                mortgage.ExtraPayments = 0;

                            if (PaymentInterval == 0)
                                mortgage.ExtraPayments += 0;
                            else
                            {
                                if (mortgage.No % PaymentInterval == 0)
                                    mortgage.ExtraPayments += double.Parse(ExtraPayment);
                                else
                                    mortgage.ExtraPayments += 0;
                            }
                        }
                        // Additional Payment
                        mortgage.AdditionalPayment = additionalPaymentList?.FirstOrDefault(x => x.No == mortgage.No)?.AdditionalPayment ?? 0;

                        //  Interest
                        mortgage.Interest = Math.Round(InterestRate * previousMortgage.Balance, 2);

                        // Principal
                        mortgage.Principal = mortgage.Payment - mortgage.Interest + mortgage.AdditionalPayment +
                                             mortgage.ExtraPayments;

                        // Balance
                        mortgage.Balance = previousMortgage.Balance - mortgage.Principal;

                        Mortgages.Add(mortgage);
                        previousMortgage = mortgage;
                    }
                    else
                    {
                        break;
                    }
                }

                // Fully Amortized
                TotalPayments = Mortgages.Sum(x => x.Interest) + Mortgages.Sum(x => x.Principal);
                TotalInterest = Mortgages.Sum(x => x.Interest);
                NumberOfPayments = Mortgages.Max(x => x.No);
                LastPaymentDate = Mortgages.Max(x => x.DueDate);
                NumberOfPaymentsYears = Math.Round((decimal)(NumberOfPayments / PeriodsPerYear), 2);

                // Balance at Term
                DateAtTerm = Mortgages.FirstOrDefault(x => x.No == (Term * PeriodsPerYear)).DueDate;
                InterestPaid = Mortgages.Where(x => x.No <= (Term * PeriodsPerYear)).Sum(x => x.Interest);
                PrincipalPaid = Mortgages.Where(x => x.No <= (Term * PeriodsPerYear)).Sum(x => x.Principal);
                OutstandingBalance = Mortgages.FirstOrDefault(x => x.No == (Term * PeriodsPerYear)).Balance;

                // Totals Assuming No Extra Payments
                if (TotalInterestWithNoExtraPayment - TotalInterest < 0)
                    InterestSavings = 0;
                else
                    InterestSavings = TotalInterestWithNoExtraPayment - TotalInterest;

                TotalPaymentsWithNoExtraPayment = TotalInterest + InterestSavings + double.Parse(LoanAmount);
                TotalInterestWithNoExtraPayment = TotalPaymentsWithNoExtraPayment - double.Parse(LoanAmount);

                //await Task.Delay(200);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            IsBusy = false;
        }

        private void RunEnter(object obj)
        {
            //Calculator();
            //await _navigation.PushModalAsync(new MenuPage());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}