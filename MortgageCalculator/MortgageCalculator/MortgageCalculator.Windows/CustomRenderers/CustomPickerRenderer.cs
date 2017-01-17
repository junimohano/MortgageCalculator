using Windows.UI.Xaml;
using MortgageCalculator.CustomRenderers;
using MortgageCalculator.Windows.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace MortgageCalculator.Windows.CustomRenderers
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
                if (customPicker != null)
                {
                    Control.FontSize = (float) customPicker.FontSize;
                    //Control.HorizontalAlignment = HorizontalAlignment.Left;
                }
            }
        }
    }
}