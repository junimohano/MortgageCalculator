using System;
using Foundation;
using MortgageCalculator.CustomRenderers;
using MortgageCalculator.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace MortgageCalculator.iOS.CustomRenderers
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
                    Control.MinimumFontSize = (nfloat)customPicker.FontSize;
                    //Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                }
            }

        }
    }
}