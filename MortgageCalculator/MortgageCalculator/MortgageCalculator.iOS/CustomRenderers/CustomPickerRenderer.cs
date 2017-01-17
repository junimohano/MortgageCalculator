using System;
using MortgageCalculator.CustomRenderers;
using MortgageCalculator.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace MortgageCalculator.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }

            if (Control != null)
            {
                var customPicker = (CustomPicker)e.NewElement;
                if (customPicker != null) { 
                    Control.MinimumFontSize = (nfloat)customPicker.FontSize;
                    //Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                }
            }
        }

    }
}