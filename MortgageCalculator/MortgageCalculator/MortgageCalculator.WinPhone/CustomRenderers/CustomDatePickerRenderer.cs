using Windows.UI.Xaml;
using MortgageCalculator.CustomRenderers;
using MortgageCalculator.WinPhone.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace MortgageCalculator.WinPhone.CustomRenderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            if (Control != null)
            {
                var customPicker = (CustomDatePicker)e.NewElement;
                if (customPicker != null)
                {
                    Control.FontSize = (float)customPicker.FontSize;
                    //Control.HorizontalAlignment = HorizontalAlignment.Left;
                }
            }
        }
    }
}