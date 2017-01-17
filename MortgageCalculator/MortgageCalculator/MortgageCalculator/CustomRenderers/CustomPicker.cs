using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MortgageCalculator.CustomRenderers
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create<CustomPicker, double>(p => p.FontSize, Font.Default.FontSize);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
    }
}
