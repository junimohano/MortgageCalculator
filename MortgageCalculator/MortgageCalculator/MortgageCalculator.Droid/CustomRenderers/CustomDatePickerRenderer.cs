using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MortgageCalculator.CustomRenderers;
using MortgageCalculator.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;
using TextAlignment = Android.Views.TextAlignment;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace MortgageCalculator.Droid.CustomRenderers
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
                    Control.TextSize = (float)customPicker.FontSize;
                    //Control.TextAlignment = TextAlignment.TextStart;
                }
            }
        }
    }
}