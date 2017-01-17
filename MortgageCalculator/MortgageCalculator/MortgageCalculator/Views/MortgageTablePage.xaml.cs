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
    public partial class MortgageTablePage : ContentPage
    {
        private readonly MortgagePageViewModel _mortgagePageViewModel;

        public MortgageTablePage(MortgagePageViewModel mortgagePageViewModel)
        {
            _mortgagePageViewModel = mortgagePageViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _mortgagePageViewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BindingContext = null;
        }

        private void Grid_OnEndRowEdit(object sender, RowEditingEventArgs e)
        {
            ((MortgagePageViewModel)BindingContext).Calculator();
        }
    }
}
