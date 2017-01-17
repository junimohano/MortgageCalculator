using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mobile.DataGrid;
using MortgageCalculator.ViewModels;
using Xamarin.Forms;

namespace MortgageCalculator.Views
{
    public partial class MortgagePage : ContentPage
    {
        public MortgagePage()
        {
            InitializeComponent();
            
            PickerCompoundPeriod.Items.Add("Semi-Annually");
            PickerCompoundPeriod.Items.Add("Monthly");
            PickerCompoundPeriod.Items.Add("Quarterly");
            PickerCompoundPeriod.Items.Add("Annually");
            PickerCompoundPeriod.SelectedIndex = 0;

            PickerPaymentFrequency.Items.Add("Annually");
            PickerPaymentFrequency.Items.Add("semi-Annually");
            PickerPaymentFrequency.Items.Add("Quarterly");
            PickerPaymentFrequency.Items.Add("Bi-Monthly");
            PickerPaymentFrequency.Items.Add("Monthly");
            PickerPaymentFrequency.Items.Add("Semi-Monthly");
            PickerPaymentFrequency.Items.Add("Bi-Weekly");
            PickerPaymentFrequency.Items.Add("Weekly");
            PickerPaymentFrequency.SelectedIndex = 0;
        }
    }
}
